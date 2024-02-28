using UnityEngine.SceneManagement;

namespace EventBusSystem.Signals.SceneSignals
{
    public class OnSceneLoadedSignal
    {
        public readonly Scene LoadedScene;

        public OnSceneLoadedSignal(Scene loadedScene)
        {
            LoadedScene = loadedScene;
        }
    }
}