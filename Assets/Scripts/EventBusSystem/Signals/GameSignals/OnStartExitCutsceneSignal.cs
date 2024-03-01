using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnStartExitCutsceneSignal : ISignal
    {
        public readonly int NextSceneID;
        public readonly float CutsceneDuration;

        public OnStartExitCutsceneSignal(int nextSceneID, float cutsceneDuration)
        {
            NextSceneID = nextSceneID;
            CutsceneDuration = cutsceneDuration;
        }
    }
}