using Databases.Impls;
using Items;
using UnityEngine;
using Utils;
using Views;

namespace Controllers.Impls
{
    public class MiniGameController : MonoBehaviour, IMiniGameController
    {
        [SerializeField] private MiniGameView _miniGameView;
        [SerializeField] private MiniGameDatabase _miniGameDatabase;
        
        private int _correctAnswersAmountPerLevel;
        private int _lastQuizIndex;
        private bool _isFirstQuestion;
        
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
        }

        private void SetCommonData()
        {
            var answersCount = PlayerPrefs.GetInt(PlayerPrefsKeys.AnswersCount);
            var level = PlayerPrefs.GetInt(PlayerPrefsKeys.MiniGameLevel);
            
            _miniGameView.SetCommonData(level.ToString(), answersCount.ToString());
        }

        private void SetQuizData()
        {
            while (true)
            {
                var index = Random.Range(0, _miniGameDatabase.MiniGameVos.Length);

                if (!_isFirstQuestion && index == _lastQuizIndex)
                    continue;

                var data = _miniGameDatabase.MiniGameVos[index];

                _isFirstQuestion = true;
                _lastQuizIndex = index;
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
                
            SetQuizData();
        }

        private void UpdateLevelData()
        {
            _correctAnswersAmountPerLevel = 0;
            _miniGameView.UpdateLevel();
        }
    }
}