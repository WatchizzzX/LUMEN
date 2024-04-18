using EasyTransition;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSetSceneSignal : ISignal
    {
        public readonly int NewSceneID;
        public readonly float Delay;
        public readonly TransitionSettings OverrideTransitionSettings;

        public OnSetSceneSignal(int newSceneID, float delay, TransitionSettings overrideTransitionSettings = null)
        {
            NewSceneID = newSceneID;
            Delay = delay;
            OverrideTransitionSettings = overrideTransitionSettings;
        }
    }
}