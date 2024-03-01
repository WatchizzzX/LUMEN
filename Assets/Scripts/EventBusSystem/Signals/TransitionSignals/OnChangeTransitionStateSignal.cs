using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.TransitionSignals
{
    public class OnChangeTransitionStateSignal : ISignal
    {
        public readonly TransitionState TransitionState;

        public OnChangeTransitionStateSignal(TransitionState transitionState)
        {
            TransitionState = transitionState;
        }
    }

    public enum TransitionState
    {
        Started,
        Cutout,
        Finished
    }
}