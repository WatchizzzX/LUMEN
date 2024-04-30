using System;
using EventBusSystem.Interfaces;
using UnityEngine.SceneManagement;

namespace EventBusSystem.Signals.SceneSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSceneLoaded.cs
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSceneLoadedSignal.cs
    public class OnSceneLoadedSignal : ISignal
=======
    public class OnSceneLoaded : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSceneLoaded.cs
=======
    [Serializable]
    public class OnSceneLoadedSignal : Signal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/SceneSignals/OnSceneLoadedSignal.cs
    {
        public readonly Scene LoadedScene;
        public readonly bool IsGameLevel;
        
        public OnSceneLoaded(Scene loadedScene, bool isGameLevel)
        {
            LoadedScene = loadedScene;
            IsGameLevel = isGameLevel;
        }
    }
}