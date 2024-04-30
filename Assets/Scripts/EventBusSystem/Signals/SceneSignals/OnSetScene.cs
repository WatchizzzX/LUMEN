using System;
using EasyTransition;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.SceneSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSetScene.cs
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSetSceneSignal.cs
    public class OnSetSceneSignal : ISignal
=======
    public class OnSetScene : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSetScene.cs
=======
    [Serializable]
    public class OnSetSceneSignal : Signal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSetSceneSignal.cs
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