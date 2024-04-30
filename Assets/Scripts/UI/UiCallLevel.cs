using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Logger = Utils.Logger;

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