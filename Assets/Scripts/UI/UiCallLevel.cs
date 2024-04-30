using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UiCallLevel : EventBehaviour
    {
        public void CallToLoadLevel(string levelName)
        {
<<<<<<< Updated upstream
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

<<<<<<< Updated upstream
            eventBus.Invoke(new OnSetSceneSignal(SceneUtility.GetBuildIndexByScenePath(levelName), 0f));
=======
            RaiseEvent(new OnSetScene(SceneUtility.GetBuildIndexByScenePath(levelName), 0f));
>>>>>>> Stashed changes
=======
            eventBus.RaiseEvent(new OnSetSceneSignal(SceneUtility.GetBuildIndexByScenePath(levelName), 0f));
>>>>>>> Stashed changes
        }
    }
}