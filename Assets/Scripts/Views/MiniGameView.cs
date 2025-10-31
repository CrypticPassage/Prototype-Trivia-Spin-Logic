using Items;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MiniGameView : MonoBehaviour
    {
        [SerializeField] private MiniGameItem[] miniGameItems;
        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_Text quizDescriptionText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text answersAmountToReachLevelText;
        [SerializeField] private TMP_Text correctAnswersAmountPerLevelText;

        public MiniGameItem[] MiniGameItems => miniGameItems;
        public Button CloseButton => closeButton;

        public void SetQuizData(MiniGameVo data)
        {
            quizDescriptionText.text = data.Description;

            for (var i = 0; i < data.MiniGameItemVos.Length; i++)
                miniGameItems[i].SetItemData(data.MiniGameItemVos[i]);
        }

        public void SetCommonData(string level, string answersCount, string correctAnswersPerLevel)
        {
            levelText.text = level;
            answersAmountToReachLevelText.text = answersCount;
            correctAnswersAmountPerLevelText.text = correctAnswersPerLevel;
        }

        public void UpdateLevel(string level) 
            => levelText.text = level;
        
        public void UpdateCorrectAnswersAmountPerLevel(string amount)
            => correctAnswersAmountPerLevelText.text = amount;
    }
}