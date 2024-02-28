using System.Collections;
using EasyTransition;
using EventBusSystem;
using EventBusSystem.Signals.TransitionSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Logger = Utils.Logger;

namespace Managers
{
    public class TransitionManager : MonoBehaviour, IService
    {
        #region Public Variables

        public TransitionManagerSettings transitionManagerSettings;

        #endregion

        #region Private Variables

        private bool _runningTransition;
        private EventBus _eventBus;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        #endregion

        #region Methods

        private void SubscribeToEventBus()
        {
        }

        private void UnsubscribeFromEventBus()
        {
        }

        /// <summary>
        /// Starts a default transition without loading a new level.
        /// </summary>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(float startDelay)
        {
            if (!CheckTransition(transitionManagerSettings.defaultTransitionSettings)) return;

            _runningTransition = true;
            StartCoroutine(Timer(startDelay, transitionManagerSettings.defaultTransitionSettings));
        }

        /// <summary>
        /// Loads the new Scene with a default transition.
        /// </summary>
        /// <param name="sceneName">The name of the scene you want to load.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(string sceneName, float startDelay)
        {
            if (!CheckTransition(transitionManagerSettings.defaultTransitionSettings)) return;

            _runningTransition = true;
            StartCoroutine(Timer(sceneName, startDelay, transitionManagerSettings.defaultTransitionSettings));
        }

        /// <summary>
        /// Loads the new Scene with a default transition.
        /// </summary>
        /// <param name="sceneIndex">The index of the scene you want to load.</param>
        /// <param name="startDelay">The delay before the transition starts.</param>
        public void Transition(int sceneIndex, float startDelay)
        {
            if (!CheckTransition(transitionManagerSettings.defaultTransitionSettings)) return;

            _runningTransition = true;
            StartCoroutine(Timer(sceneIndex, startDelay, transitionManagerSettings.defaultTransitionSettings));
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

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Started));

            var template = Instantiate(transitionManagerSettings.transitionPrefab);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            var transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
                transitionTime /= transitionSettings.transitionSpeed;

            yield return new WaitForSecondsRealtime(transitionTime);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Cutout));

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Finished));

            _runningTransition = false;
        }

        private IEnumerator Timer(int sceneIndex, float startDelay, TransitionSettings transitionSettings)
        {
            yield return new WaitForSecondsRealtime(startDelay);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Started));

            var template = Instantiate(transitionManagerSettings.transitionPrefab);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            var transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
                transitionTime /= transitionSettings.transitionSpeed;

            yield return new WaitForSecondsRealtime(transitionTime);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Cutout));

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Finished));

            _runningTransition = false;
        }

        private IEnumerator Timer(float delay, TransitionSettings transitionSettings)
        {
            yield return new WaitForSecondsRealtime(delay);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Started));

            var template = Instantiate(transitionManagerSettings.transitionPrefab);
            template.GetComponent<Transition>().transitionSettings = transitionSettings;

            var transitionTime = transitionSettings.transitionTime;
            if (transitionSettings.autoAdjustTransitionTime)
                transitionTime /= transitionSettings.transitionSpeed;

            yield return new WaitForSecondsRealtime(transitionTime);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Cutout));

            template.GetComponent<Transition>().OnSceneLoad(UnityEngine.SceneManagement.SceneManager.GetActiveScene(),
                LoadSceneMode.Single);

            yield return new WaitForSecondsRealtime(transitionSettings.destroyTime);

            _eventBus.Invoke(new ChangeTransitionStateSignal(TransitionState.Finished));

            _runningTransition = false;
        }

        #endregion
    }
}