namespace Databases
{
    public interface IGameSettingsDatabase
    {
        float LoadingDuration { get; }
        float StartSpinSpeed { get; }
        float DecelerationSpinSpeed { get; }
        float RandomSpinSpeedOffset { get; }
        float SpinSpeedMinRange { get; }
        float SpinSpeedMaxRange { get; }
        float SpinCooldownSec { get; }
    }
}