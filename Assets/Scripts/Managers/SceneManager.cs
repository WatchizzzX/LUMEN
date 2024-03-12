using System;
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
            _eventBus.Subscribe<OnSetSceneSignal>(OnSetScene);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnSetSceneSignal>(OnSetScene);
        }
        
        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
        {
            _eventBus.Invoke(new OnSceneLoadedSignal(loadedScene));
        }

        private void OnSetScene(OnSetSceneSignal signal)
        {
            LoadScene(signal.NewSceneID, signal.Delay);
        }

        private void LoadScene(int sceneID, float delay)
        {
            if (!CheckIfSceneExists(sceneID)) return;

            var transitionManager = ServiceLocator.Get<TransitionManager>();
            if (transitionManager != null)
            {
                transitionManager.Transition(sceneID, delay);
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
                $"Scene required to load, but this scene doesn't exists in build");
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
                    if (!DoesSceneExist(sceneName)) return;
                    LoadScene(SceneUtility.GetBuildIndexByScenePath(sceneName), 0f);
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

        #region Utils

        public static bool DoesSceneExist(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            for (var i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var lastSlash = scenePath.LastIndexOf("/", StringComparison.Ordinal);
                var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".", StringComparison.Ordinal) - lastSlash - 1);

                if (string.Compare(name, sceneName, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;
            }

            return false;
        }

        #endregion
    }
}