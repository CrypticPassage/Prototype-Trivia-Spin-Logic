using System.Collections;
using Handlers.Impls;
using Services.Impls;
using UnityEngine;
using Utils;
using Views;

namespace Controllers.Impls
{
    public class MenuController : MonoBehaviour, IMenuController
    {
        [SerializeField] private MenuView _menuView;
        [SerializeField] private SpinHandler _spinHandler;
        [SerializeField] private LoadingService _loadingService;
        
        private void Start()
        {
            _spinHandler.OnCooldownEnd += ActivateSpinRoll;
            
            _menuView.OpenMiniGameButton.onClick.AddListener(OnOpenMiniGameButtonClick);
            _menuView.OpenDailyBonusButton.onClick.AddListener(OnOpenDailyBonusButtonClick);
            _menuView.SpinButton.onClick.AddListener(OnSpinButtonClick);
            _menuView.CloseDailyBonusButton.onClick.AddListener(OnCloseDailyBonusButtonClick);

            StartCoroutine(UpdateSpinCooldownUI());
        }

        private void OnOpenMiniGameButtonClick() 
            => _loadingService.LoadScene(SceneNames.MinigameScene);

        private void OnOpenDailyBonusButtonClick() 
            => _menuView.SetDailyBonusData(true);

        private void OnSpinButtonClick()
            => _spinHandler.RollSpin(_menuView.SpinCircleImage);

        private void OnCloseDailyBonusButtonClick() 
        {
            if (!_spinHandler.IsSpinning)
                _menuView.SetDailyBonusData(false);
        }
        
        private IEnumerator UpdateSpinCooldownUI()
        {
            while (true)
            {
                if (_spinHandler.IsCooldown)
                {
                    var remaining = _spinHandler.CooldownRemaining;
                    var minutes = Mathf.FloorToInt(remaining / 60f);
                    var seconds = Mathf.FloorToInt(remaining % 60f);
                    _menuView.SpinButton.interactable = false;
                    _menuView.SpinTimerText.text = $"{minutes:D2}:{seconds:D2}";
                }
                else if (!_spinHandler.IsSpinning)
                    ActivateSpinRoll();

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void ActivateSpinRoll()
        {
            _menuView.SpinButton.interactable = true;
            _menuView.SpinTimerText.text = "Ready!";
        }
    }
}