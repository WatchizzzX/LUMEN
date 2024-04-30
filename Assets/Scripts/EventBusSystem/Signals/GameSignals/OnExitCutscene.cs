<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnExitCutsceneSignal.cs
=======
using Enums;
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnExitCutscene.cs
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnExitCutsceneSignal.cs
    public class OnExitCutsceneSignal : ISignal
=======
    public class OnExitCutscene : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnExitCutscene.cs
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