using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.TransitionSignals
{
    public class OnChangeTransitionStateSignal : ISignal
    {
        public readonly TransitionState TransitionState;
        public readonly bool IsChangingScene;

        public OnChangeTransitionStateSignal(TransitionState transitionState, bool isChangingScene)
        {
            TransitionState = transitionState;
            IsChangingScene = isChangingScene;
        }
    }

    public enum TransitionState
    {
        Started,
        Cutout,
        Finished
    }
}