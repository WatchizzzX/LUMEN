using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnStartExitCutsceneSignal : ISignal
    {
        public readonly int NextSceneID;
        public readonly float CutsceneDuration;
        public readonly ExitCamera ExitCamera;

        public OnStartExitCutsceneSignal(int nextSceneID, float cutsceneDuration, ExitCamera exitCamera)
        {
            NextSceneID = nextSceneID;
            CutsceneDuration = cutsceneDuration;
            ExitCamera = exitCamera;
        }
    }

    public enum ExitCamera
    {
        FarView,
        StaticView
    }
}