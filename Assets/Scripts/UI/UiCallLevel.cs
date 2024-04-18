using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Logger = Utils.Logger;

namespace UI
{
    public class UiCallLevel : MonoBehaviour
    {
        public void CallToLoadLevel(string levelName)
        {
            if (!ServiceLocator.TryGet(out EventBus eventBus))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error, "Can't find EventBus. Loading level is impossible");
                return;
            }

            if (!SceneUtils.DoesSceneExist(levelName))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error,
                    $"Can't find scene. Please check if {levelName} scene exists");
                return;
            }

            eventBus.Invoke(new OnSetSceneSignal(SceneUtility.GetBuildIndexByScenePath(levelName), 0f));
        }
    }
}