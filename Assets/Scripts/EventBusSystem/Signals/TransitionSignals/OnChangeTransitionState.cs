<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/TransitionSignals/OnChangeTransitionStateSignal.cs
=======
using Enums;
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/TransitionSignals/OnChangeTransitionState.cs
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.TransitionSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/TransitionSignals/OnChangeTransitionStateSignal.cs
    public class OnChangeTransitionStateSignal : ISignal
=======
    public class OnChangeTransitionState : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/TransitionSignals/OnChangeTransitionState.cs
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