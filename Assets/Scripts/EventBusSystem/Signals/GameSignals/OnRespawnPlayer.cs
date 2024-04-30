using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnRespawnPlayerSignal.cs
    public class OnRespawnPlayerSignal : ISignal
=======
    public class OnRespawnPlayer : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnRespawnPlayer.cs
    {
        public readonly float TransitionStartDelay;

        public OnRespawnPlayer(float transitionStartDelay = 0f)
        {
            TransitionStartDelay = transitionStartDelay;
        }
    }
}