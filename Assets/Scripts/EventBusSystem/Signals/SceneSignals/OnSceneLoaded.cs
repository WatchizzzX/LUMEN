using EventBusSystem.Interfaces;
using UnityEngine.SceneManagement;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSceneLoaded : ISignal
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