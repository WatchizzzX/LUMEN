using System;
using System.Linq;
using DavidFDev.DevConsole;
using EasyTransition;
using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine.SceneManagement;
using Utils;
using Logger = Utils.Logger;

namespace Managers
{
    public class SceneManager : EventBehaviour, IService
    {
        [NonSerialized] public SceneManagerSettings Settings;

        public bool IsSceneLoading => _isSceneLoading;

        private bool _isSceneLoading;

        protected override void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

            RegisterCommands();

            base.Awake();
        }

        protected override void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

            UnregisterCommands();
            
            base.OnDestroy();
        }

        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
        {
            _isSceneLoading = false;
            var isGameScene = !Settings.NonGameScenes.Contains(loadedScene);
            RaiseEvent(new OnSceneLoaded(loadedScene, isGameScene));
        }

        [ListenTo(SignalEnum.OnSetScene)]
        private void OnSetScene(EventModel eventModel)
        {
            var payload = (OnSetScene)eventModel.Payload;
            LoadScene(payload.NewSceneID, payload.Delay, payload.OverrideTransitionSettings);
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