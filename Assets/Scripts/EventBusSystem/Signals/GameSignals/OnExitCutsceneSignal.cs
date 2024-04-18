using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnExitCutsceneSignal : ISignal
    {
        public readonly int NextSceneID;
        public readonly float CutsceneDuration;
        public readonly ExitCamera ExitCamera;

        public OnExitCutsceneSignal(int nextSceneID, float cutsceneDuration, ExitCamera exitCamera)
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