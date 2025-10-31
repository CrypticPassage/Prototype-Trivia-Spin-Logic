using Items;
using Models;
using TMPro;
using UnityEngine;

namespace Views
{
    public class MiniGameView : MonoBehaviour
    {
        [SerializeField] private MiniGameItem[] miniGameItems;
        [SerializeField] private TMP_Text quizDescriptionText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text answersAmountToReachLevelText;

        public MiniGameItem[] MiniGameItems => miniGameItems;

        public void SetQuizData(MiniGameVo data)
        {
            quizDescriptionText.text = data.Description;

            for (var i = 0; i < data.MiniGameItemVos.Length; i++)
                miniGameItems[i].SetItemData(data.MiniGameItemVos[i]);
        }

        public void SetCommonData(string level, string answersCount)
        {
            levelText.text = level;
            answersAmountToReachLevelText.text = answersCount;
        }

        public void UpdateLevel()
        {
            var level = int.Parse(levelText.text);

            level += 1;
            levelText.text = level.ToString();
        }
    }
}