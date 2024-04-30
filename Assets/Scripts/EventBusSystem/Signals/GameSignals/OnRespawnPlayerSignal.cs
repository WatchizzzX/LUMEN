using System;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    [Serializable]
    public class OnRespawnPlayerSignal : Signal
    {
        public readonly float TransitionStartDelay;

        public OnRespawnPlayerSignal(float transitionStartDelay = 0f)
        {
            TransitionStartDelay = transitionStartDelay;
        }
    }
}