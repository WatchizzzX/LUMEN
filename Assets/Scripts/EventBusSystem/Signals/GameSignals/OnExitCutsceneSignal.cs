using System;
using EventBusSystem.Interfaces;
using UnityEngine;

namespace EventBusSystem.Signals.GameSignals
{
    [Serializable]
    public class OnExitCutsceneSignal : Signal
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