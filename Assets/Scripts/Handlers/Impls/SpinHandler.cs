using System;
using System.Collections;
using Databases.Impls;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace Handlers.Impls
{
    public class SpinHandler : MonoBehaviour, ISpinHandler
    {
        [SerializeField] private GameSettingsDatabase _gameSettingsDatabase;
        
        private DateTime _nextAvailableTime;
        private bool _isSpinning;
        private bool _isCooldownActive;
        
        public event Action OnCooldownEnd;
        public float CooldownRemaining => Mathf.Max(0f, (float)(_nextAvailableTime - DateTime.UtcNow).TotalSeconds);
        public bool IsSpinning => _isSpinning;
        public bool IsCooldown => _isCooldownActive;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKeys.LastSpinTime)) 
                return;

            var saved = PlayerPrefs.GetString(PlayerPrefsKeys.LastSpinTime);

            if (!DateTime.TryParse(saved, null, System.Globalization.DateTimeStyles.RoundtripKind, out var savedTime)
                || savedTime <= DateTime.UtcNow) 
                return;
            
            _nextAvailableTime = savedTime;
            _isCooldownActive = true;
            
            StartCoroutine(WatchSpinCooldown());
        }

        public void RollSpin(Image spinImage)
        {
            if (_isSpinning || _isCooldownActive) 
                return;
            
            StartCoroutine(DoSpin(spinImage));
        }

        private IEnumerator DoSpin(Image spinImage)
        {
            _isSpinning = true;
            
            var speed = Random.Range(_gameSettingsDatabase.SpinSpeedMinRange, _gameSettingsDatabase.SpinSpeedMaxRange) 
                        * Random.Range(1f - _gameSettingsDatabase.RandomSpinSpeedOffset, 1 + _gameSettingsDatabase.RandomSpinSpeedOffset);
            
            while (speed > 0f)
            {
                spinImage.transform.Rotate(0f, 0f, -speed * Time.deltaTime);
                speed -= _gameSettingsDatabase.DecelerationSpinSpeed * Time.deltaTime;
                
                yield return null;
            }
            
            _isSpinning = false;
            StartSpinCooldown();
        }

        private void StartSpinCooldown()
        {
            _nextAvailableTime = DateTime.UtcNow.AddSeconds(_gameSettingsDatabase.SpinCooldownSec);
            
            PlayerPrefs.SetString(PlayerPrefsKeys.LastSpinTime, _nextAvailableTime.ToString("o"));
            PlayerPrefs.Save();
            
            _isCooldownActive = true;
            StartCoroutine(WatchSpinCooldown());
        }

        private IEnumerator WatchSpinCooldown()
        {
            while (_nextAvailableTime > DateTime.UtcNow)
                yield return new WaitForSeconds(1f);
            
            _isCooldownActive = false;
            OnCooldownEnd?.Invoke();
        }
    }
}