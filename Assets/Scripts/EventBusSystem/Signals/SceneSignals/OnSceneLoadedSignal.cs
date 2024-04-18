using EventBusSystem.Interfaces;
using UnityEngine.SceneManagement;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSceneLoadedSignal : ISignal
    {
        public readonly Scene LoadedScene;
        public readonly bool IsGameLevel;
        
        public OnSceneLoadedSignal(Scene loadedScene, bool isGameLevel)
        {
            LoadedScene = loadedScene;
            IsGameLevel = isGameLevel;
        }
    }
}