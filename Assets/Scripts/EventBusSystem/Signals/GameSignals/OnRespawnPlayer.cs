using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnRespawnPlayer : ISignal
    {
        public readonly float TransitionStartDelay;

        public OnRespawnPlayer(float transitionStartDelay = 0f)
        {
            TransitionStartDelay = transitionStartDelay;
        }
    }
}