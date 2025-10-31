using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MenuView : MonoBehaviour
    {
        [Header("Buttons")] 
        [SerializeField] private Button openMiniGameButton;
        [SerializeField] private Button openDailyBonusButton;
        [SerializeField] private Button spinButton;
        [SerializeField] private Button closeDailyBonusButton;
        [Header("Spin")]
        [SerializeField] private Image spinCircleImage;
        [SerializeField] private TMP_Text spinTimerText;
        [Header("Other")] 
        [SerializeField] private GameObject dailyBonusContainer;
        
        public Button OpenMiniGameButton => openMiniGameButton;
        public Button OpenDailyBonusButton => openDailyBonusButton;
        public Button SpinButton => spinButton;
        public Button CloseDailyBonusButton => closeDailyBonusButton;
        public Image SpinCircleImage => spinCircleImage;
        public TMP_Text SpinTimerText => spinTimerText;

        public void SetDailyBonusData(bool isActive)
            => dailyBonusContainer.SetActive(isActive);
    }
}