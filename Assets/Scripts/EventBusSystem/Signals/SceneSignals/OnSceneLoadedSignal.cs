using EventBusSystem.Interfaces;
using UnityEngine.SceneManagement;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSceneLoadedSignal : ISignal
    {
        public readonly Scene LoadedScene;

        public OnSceneLoadedSignal(Scene loadedScene)
        {
            LoadedScene = loadedScene;
        }
    }
}