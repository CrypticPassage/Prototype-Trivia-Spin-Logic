using System;
using UnityEngine.UI;

namespace Handlers
{
    public interface ISpinHandler
    {
        event Action OnCooldownEnd;
        float CooldownRemaining { get; }
        bool IsSpinning { get; }
        bool IsCooldown { get; }
        void RollSpin(Image spinImage);
    }
}