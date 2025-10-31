using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class MiniGameItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text description;
        [SerializeField] private Button clickButton;

        private bool _isCorrectAnswer;

        public bool IsCorrectAnswer => _isCorrectAnswer;
        
        public Button ClickButton => clickButton;

        public void SetItemData(MiniGameItemVo data)
        {
            description.text = data.Description;
            _isCorrectAnswer = data.IsCorrectAnswer;
        }
    }
}