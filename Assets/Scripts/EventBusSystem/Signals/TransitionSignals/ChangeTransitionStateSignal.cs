namespace EventBusSystem.Signals.TransitionSignals
{
    public class ChangeTransitionStateSignal
    {
        public readonly TransitionState TransitionState;

        public ChangeTransitionStateSignal(TransitionState transitionState)
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