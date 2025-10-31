using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/GameSettingsDatabase", fileName = "GameSettingsDatabase")] 
    public class GameSettingsDatabase : ScriptableObject, IGameSettingsDatabase
    {
        [SerializeField] private float loadingDuration;
        [SerializeField] private float startSpinSpeed;
        [SerializeField] private float decelerationSpinSpeed;
        [SerializeField] private float randomSpinSpeedOffset;
        [SerializeField] private float spinSpeedMinRange;
        [SerializeField] private float spinSpeedMaxRange;
        [SerializeField] private int spinCooldownSec;

        public float LoadingDuration => loadingDuration;
        public float StartSpinSpeed => startSpinSpeed;
        public float DecelerationSpinSpeed => decelerationSpinSpeed;
        public float RandomSpinSpeedOffset => randomSpinSpeedOffset;
        public float SpinSpeedMinRange => spinSpeedMinRange;
        public float SpinSpeedMaxRange => spinSpeedMaxRange;
        public float SpinCooldownSec => spinCooldownSec;
    }
}