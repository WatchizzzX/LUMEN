using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UiCallLevel : EventBehaviour
    {
        public void CallToLoadLevel(string levelName)
        {
            RaiseEvent(new OnSetScene(SceneUtility.GetBuildIndexByScenePath(levelName), 0f));
        }
    }
}