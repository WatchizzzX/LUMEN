using Enums;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.TransitionSignals
{
    public class OnChangeTransitionState : ISignal
    {
        public readonly TransitionState TransitionState;
        public readonly bool IsChangingScene;

        public OnChangeTransitionState(TransitionState transitionState, bool isChangingScene)
        {
            TransitionState = transitionState;
            IsChangingScene = isChangingScene;
        }
    }
}