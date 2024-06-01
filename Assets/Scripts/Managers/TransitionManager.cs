using System;
using System.Collections;
using Baracuda.Monitoring;
using EasyTransition;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace Managers
{
    [MTag("Transition Manager")]
    [MGroupName("Transition Manager")]
    public class TransitionManager : EventBehaviour, IService
    {
        #region Public Variables

        [NonSerialized]
        public TransitionManagerSettings Settings;

        public bool IsTransitionStarted => _runningTransition;

        #endregion

        #region Private Variables

        private bool _runningTransition;

        #endregion

        #region Methods

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.StopMonitoring();
        }
        
        [ListenTo(SignalEnum.OnDevModeChanged)]
        private void OnDevModeChanged(EventModel eventModel)
        {
            var payload = (OnDevModeChanged)eventModel.Payload;
            if(payload.InDeveloperMode)
                this.StartMonitoring();
            else
                this.StopMonitoring();
        }

        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
        {
            Transition(((OnRespawnPlayer)eventModel.Payload).TransitionStartDelay);
        }

        /// <summary>
        /// Starts a default transition without loading a new level.
        /// </summary>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(float startDelay)
        {
            if (!CheckTransition(Settings.DefaultTransitionSettings)) return;

            _runningTransition = true;
            StartCoroutine(Timer(startDelay, Settings.DefaultTransitionSettings));
        }

        /// <summary>
        /// Loads the new Scene with a default transition.
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to load.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(string sceneName, float startDelay)
        {
            if (!CheckTransition(Settings.DefaultTransitionSettings)) return;

            _runningTransition = true;
            StartCoroutine(Timer(sceneName, startDelay, Settings.DefaultTransitionSettings));
        }

        /// <summary>
        /// Loads the new Scene with a default transition.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene you want to load.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(int sceneIndex, float startDelay)
        {
            if (!CheckTransition(Settings.DefaultTransitionSettings)) return;

            _runningTransition = true;
            StartCoroutine(Timer(sceneIndex, startDelay, Settings.DefaultTransitionSettings));
        }

        /// <summary>
        /// Starts a transition without loading a new level.
        /// </summary>
        /// <param name="transition">The settings of the transition you want to use.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(TransitionSettings transition, float startDelay)
        {
            if (!CheckTransition(transition)) return;

            _runningTransition = true;
            StartCoroutine(Timer(startDelay, transition));
        }

        /// <summary>
        /// Loads the new Scene with a transition.
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to load.</param>
        /// <param name="transition">The settings of the transition you want to use to load you new scene.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(string sceneName, TransitionSettings transition, float startDelay)
        {
            if (!CheckTransition(transition)) return;

            _runningTransition = true;
            StartCoroutine(Timer(sceneName, startDelay, transition));
        }

        /// <summary>
        /// Loads the new Scene with a transition.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene you want to load.</param>
        /// <param name="transition">The settings of the transition you want to use to load you new scene.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(int sceneIndex, TransitionSettings transition, float startDelay)
        {
            if (!CheckTransition(transition)) return;

            _runningTransition = true;
            StartCoroutine(Timer(sceneIndex, startDelay, transition));
        }

        #endregion

        #region Utils

        /// <summary>
        /// Gets the index of a scene from its name.
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to get the index of.</param>
        private int GetSceneIndex(string sceneName)
        {
            return UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).buildIndex;
        }

        /// <summary>
        /// Check if TransitionSettings is not null. If is null it will be logged
        /// </summary>
        /// <param name="transition">Transition for checking</param>
        /// <returns>Result of checking</returns>
        private bool CheckTransition(TransitionSettings transition)
        {
            if (transition != null && !_runningTransition) return true;

            Logger.Log(LoggerChannel.TransitionManager, Priority.Error,
                _runningTransition
                    ? "Can't run transition when other transition is running"
                    : "Transition settings doesn't assigned. Transition will be skipped");
            return false;
        }

        #endregion

        #region Timer Overloads

        private IEnumerator Timer(string sceneName, float startDelay, TransitionSettings transitionSettings)
        {
            yield return new WaitForSecondsRealtime(startDelay);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Started, true));

            var template = Instantiate(Settings.TransitionPrefab);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            var transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
                transitionTime /= transitionSettings.transitionSpeed;

            yield return new WaitForSecondsRealtime(transitionTime);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Cutout, true));

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Finished, true));

            _runningTransition = false;
        }

        private IEnumerator Timer(int sceneIndex, float startDelay, TransitionSettings transitionSettings)
        {
            yield return new WaitForSecondsRealtime(startDelay);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Started, true));

            var template = Instantiate(Settings.TransitionPrefab);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            var transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
                transitionTime /= transitionSettings.transitionSpeed;

            yield return new WaitForSecondsRealtime(transitionTime);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Cutout, true));

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Finished, true));

            _runningTransition = false;
        }

        private IEnumerator Timer(float delay, TransitionSettings transitionSettings)
        {
            yield return new WaitForSecondsRealtime(delay);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Started, false));

            var template = Instantiate(Settings.TransitionPrefab);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            var transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
                transitionTime /= transitionSettings.transitionSpeed;

            yield return new WaitForSecondsRealtime(transitionTime);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Cutout, false));

            template.GetComponent<Transition>().OnSceneLoad(UnityEngine.SceneManagement.SceneManager.GetActiveScene(),
                LoadSceneMode.Single);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            RaiseEvent(new OnChangeTransitionState(TransitionState.Finished, false));

            _runningTransition = false;
        }

        #endregion
    }
}