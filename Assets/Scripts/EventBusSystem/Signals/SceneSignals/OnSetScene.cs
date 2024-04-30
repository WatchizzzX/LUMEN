using EasyTransition;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSetScene : ISignal
    {
        public readonly int NewSceneID;
        public readonly float Delay;
        public readonly TransitionSettings OverrideTransitionSettings;

        public OnSetScene(int newSceneID, float delay, TransitionSettings overrideTransitionSettings = null)
        {
            NewSceneID = newSceneID;
            Delay = delay;
            OverrideTransitionSettings = overrideTransitionSettings;
        }
    }
}