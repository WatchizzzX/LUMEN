using Enums;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnExitCutscene : ISignal
    {
        public readonly int NextSceneID;
        public readonly float CutsceneDuration;
        public readonly ExitCamera ExitCamera;

        public OnExitCutscene(int nextSceneID, float cutsceneDuration, ExitCamera exitCamera)
        {
            NextSceneID = nextSceneID;
            CutsceneDuration = cutsceneDuration;
            ExitCamera = exitCamera;
        }
    }
}