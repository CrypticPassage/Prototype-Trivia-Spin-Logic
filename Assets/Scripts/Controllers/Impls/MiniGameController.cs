using Databases.Impls;
using Items;
using Models;
using Services.Impls;
using UnityEngine;
using Utils;
using Views;

namespace Controllers.Impls
{
    public class MiniGameController : MonoBehaviour, IMiniGameController
    {
        [SerializeField] private MiniGameView _miniGameView;
        [SerializeField] private LoadingService _loadingService;
        [SerializeField] private MiniGameDatabase _miniGameDatabase;
        
        private MiniGameVo _lastItemVo;
        private int _correctAnswersAmountPerLevel;
        
        private void Start()
        {
            BindButtons();
            SetCommonData();
            SetQuizData();
        }

        private void BindButtons()
        {
            foreach (var item in _miniGameView.MiniGameItems)
                item.ClickButton.onClick.AddListener(() => OnItemButtonClick(item));
            
            _miniGameView.CloseButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick() 
            => _loadingService.LoadScene(SceneNames.MenuScene);

        private void SetCommonData()
        {
            var answersCount = PlayerPrefs.GetInt(PlayerPrefsKeys.AnswersCount);
            var level = PlayerPrefs.GetInt(PlayerPrefsKeys.MiniGameLevel);
            
            _miniGameView.SetCommonData(level.ToString(), answersCount.ToString(), _correctAnswersAmountPerLevel.ToString());
        }

        private void SetQuizData()
        {
            while (true)
            {
                var index = Random.Range(0, _miniGameDatabase.MiniGameVos.Length);

                if ( _lastItemVo == _miniGameDatabase.MiniGameVos[index])
                    continue;

                var data = _miniGameDatabase.MiniGameVos[index];
                
                _lastItemVo = data;
                _miniGameView.SetQuizData(data);
                break;
            }
        }

        private void OnItemButtonClick(MiniGameItem item)
        {
            if (!item.IsCorrectAnswer)
            {
                SetQuizData();
                return;
            }

            var answersCount = PlayerPrefs.GetInt(PlayerPrefsKeys.AnswersCount);
            _correctAnswersAmountPerLevel += 1;
            
            if (_correctAnswersAmountPerLevel >= answersCount)
                UpdateLevelData();
                
            _miniGameView.UpdateCorrectAnswersAmountPerLevel(_correctAnswersAmountPerLevel.ToString());
            SetQuizData();
        }

        private void UpdateLevelData()
        {
            var oldLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MiniGameLevel);
            var newLevel = oldLevel + 1;
            
            PlayerPrefs.SetInt(PlayerPrefsKeys.MiniGameLevel, newLevel);
            _miniGameView.UpdateLevel(newLevel.ToString());
            _correctAnswersAmountPerLevel = 0;
        }
    }
}