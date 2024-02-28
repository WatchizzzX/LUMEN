using DavidFDev.DevConsole;
using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Logger = Utils.Logger;

namespace Managers
{
    public class SceneManager : MonoBehaviour, IService
    {
        #region Private Variables

        private EventBus _eventBus;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            
            RegisterCommands();
            SubscribeToEventBus();
        }

        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            
            UnregisterCommands();
            UnsubscribeFromEventBus();
        }

        #endregion

        #region Methods

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<SetSceneSignal>(OnSetScene);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<SetSceneSignal>(OnSetScene);
        }
        
        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
        {
            _eventBus.Invoke(new OnSceneLoadedSignal(loadedScene));
        }

        private void OnSetScene(SetSceneSignal signal)
        {
            LoadScene(signal.NewSceneID);
        }

        private void LoadScene(int sceneID)
        {
            if (!CheckIfSceneExists(sceneID)) return;

            var transitionManager = ServiceLocator.Get<TransitionManager>();
            if (transitionManager != null)
            {
                transitionManager.Transition(sceneID, 0f);
            }
            else
            {
                Logger.Log(LoggerChannel.SceneManager, Priority.Warning,
                    "TransitionManager doesn't find. Start hard loading scene");
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
            }
        }

        #endregion

        #region Utils
        
        private static bool CheckIfSceneExists(int sceneID)
        {
            if (sceneID > 0)
            {
                return true;
            }

            Logger.Log(LoggerChannel.SceneManager, Priority.Error,
                $"{UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(sceneID).name} scene required to load, but this scene doesn't exists in build");
            return false;
        }

        private void RegisterCommands()
        {
            var result = DevConsole.RemoveCommand("scene_load");
            result &= DevConsole.AddCommand(Command.Create<string>(
                name: "load_scene",
                aliases: "load",
                helpText: "Request a scene manager to load other scene",
                p1: Parameter.Create(
                    name: "Scene name",
                    helpText: "Scene name to load"),
                callback: sceneName =>
                {
                    LoadScene(SceneUtility.GetBuildIndexByScenePath(sceneName));
                }));

            if (result)
                Logger.Log(LoggerChannel.SceneManager, Priority.Info, "Registering dev-commands was successful");
            else
                Logger.Log(LoggerChannel.SceneManager, Priority.Warning, "Registering dev-commands was failed");
        }

        private void UnregisterCommands()
        {
            var result = DevConsole.RemoveCommand("load_scene");
            if (result)
                Logger.Log(LoggerChannel.SceneManager, Priority.Info, "Unregistering dev-commands was successful");
            else
                Logger.Log(LoggerChannel.SceneManager, Priority.Warning, "Unregistering dev-commands was failed");
        }

        #endregion
    }
}