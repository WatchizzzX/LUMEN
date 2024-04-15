using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnRespawnPlayerSignal : ISignal
    {
        public readonly float TransitionStartDelay;

        public OnRespawnPlayerSignal(float transitionStartDelay = 0f)
        {
            TransitionStartDelay = transitionStartDelay;
        }
    }
}