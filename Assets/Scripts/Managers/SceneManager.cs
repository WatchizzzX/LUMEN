using System;
using System.Linq;
using DavidFDev.DevConsole;
using EasyTransition;
using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Logger = Utils.Logger;

namespace Managers
{
    public class SceneManager : MonoBehaviour, IService
    {
        [NonSerialized] public SceneManagerSettings Settings;

        public bool IsSceneLoading => _isSceneLoading;

        private EventBus _eventBus;

        private bool _isSceneLoading;

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
            _isSceneLoading = false;
            var isGameScene = !Settings.NonGameScenes.Contains(loadedScene);
            _eventBus.Invoke(new OnSceneLoadedSignal(loadedScene, isGameScene));
        }

        private void OnSetScene(OnSetSceneSignal signal)
        {
            LoadScene(signal.NewSceneID, signal.Delay, signal.OverrideTransitionSettings);
        }

        private void LoadScene(int sceneID, float delay, TransitionSettings overrideTransitionSettings = null)
        {
            if (!CheckIfSceneExists(sceneID)) return;

            var transitionManager = ServiceLocator.Get<TransitionManager>();
            if (transitionManager != null)
            {
                if (overrideTransitionSettings)
                    transitionManager.Transition(sceneID, overrideTransitionSettings, delay);
                else
                    transitionManager.Transition(sceneID, delay);
            }
            else
            {
                Logger.Log(LoggerChannel.SceneManager, Priority.Warning,
                    "TransitionManager doesn't find. Start hard loading scene");
                _isSceneLoading = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
            }
        }

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
                    if (!SceneUtils.DoesSceneExist(sceneName)) return;
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
    }
}