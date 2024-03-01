using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSetSceneSignal : ISignal
    {
        public readonly int NewSceneID;
        public readonly float Delay;

        public OnSetSceneSignal(int newSceneID, float delay)
        {
            NewSceneID = newSceneID;
            Delay = delay;
        }
    }
}