using System;
using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using EventBusSystem.Signals.TransitionSignals;
using Input;
using InteractionSystem;
using PickupSystem;
using Player;
using ServiceLocatorSystem;
using Unity.Cinemachine;
using UnityEngine;
using Utils;

namespace Managers
{
    public class SpawnManager : MonoBehaviour, IService
    {
        #region Public Variables

        public SpawnManagerSettings spawnManagerSettings;

        public PlayerController PlayerController { get; private set; }
        public PlayerAnimator PlayerAnimator { get; private set; }
        public PickupController PickupController { get; private set; }
        public InteractorController InteractorController { get; private set; }
        public CinemachineCamera PlayerCamera { get; private set; }
        public PlayerInputHandler PlayerInputHandler { get; private set; }

        #endregion

        #region Private Variables

        private EventBus _eventBus;

        private bool _isLevelChanging;

        private GameObject _spawnedPlayerGo;

        private GameObject _spawnedInputGo;

        private GameObject _spawnedCameraGo;

        private GameObject _spawnedPlayerObjectsGo;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        #endregion

        #region Methods

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnSceneLoadedSignal>(OnSceneLoaded);
            _eventBus.Subscribe<ChangeTransitionStateSignal>(OnChangeTransitionState);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnSceneLoadedSignal>(OnSceneLoaded);
        }

        private void Initialize()
        {
            _spawnedPlayerObjectsGo = new GameObject("Player Objects");
            _spawnedPlayerObjectsGo.AddComponent<DontDestroyOnLoad>();

            _spawnedCameraGo = Instantiate(spawnManagerSettings.cameraPrefab, new Vector3(-4f, 3f, -4f),
                Quaternion.identity);
            _spawnedCameraGo.name = "Player Camera";
            _spawnedCameraGo.transform.SetParent(_spawnedPlayerObjectsGo.transform);
            PlayerCamera = _spawnedCameraGo.transform.GetComponentInChildren<CinemachineCamera>();

            _spawnedInputGo = Instantiate(spawnManagerSettings.inputPrefab, Vector3.zero, Quaternion.identity);
            _spawnedInputGo.name = "Input";
            _spawnedInputGo.transform.SetParent(_spawnedPlayerObjectsGo.transform);
            PlayerInputHandler = _spawnedInputGo.GetComponent<PlayerInputHandler>();
        }

        private void SpawnPlayer()
        {
            try
            {
                _spawnedPlayerGo = Instantiate(spawnManagerSettings.playerPrefab, Vector3.zero, Quaternion.identity);
                _spawnedPlayerGo.name = "Player";
                PlayerAnimator = _spawnedPlayerGo.GetComponent<PlayerAnimator>();
                PlayerController = _spawnedPlayerGo.GetComponent<PlayerController>();
                InteractorController = _spawnedPlayerGo.GetComponent<InteractorController>();
                PickupController = _spawnedPlayerGo.GetComponent<PickupController>();

                PlayerCamera.Follow = _spawnedPlayerGo.transform.Find("CameraTarget");

                PlayerInputHandler.onMoveEvent.AddListener(PlayerController.SetMoveDirection);
                PlayerInputHandler.onInteractEvent.AddListener(InteractorController.Interact);
                PlayerInputHandler.onInteractEvent.AddListener(PickupController.OnPickupEvent);
                PlayerInputHandler.onJumpEvent.AddListener(PlayerController.CallToJump);
                PlayerInputHandler.onSprintEvent.AddListener(PlayerController.SetSprint);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        #endregion

        #region Event Handlers

        private void OnSceneLoaded(OnSceneLoadedSignal signal)
        {
            if (signal.LoadedScene.name != "Start")
                SpawnPlayer();
        }

        private void OnChangeTransitionState(ChangeTransitionStateSignal signal)
        {
            switch (signal.TransitionState)
            {
                case TransitionState.Started:
                    PlayerInputHandler.onMoveEvent.RemoveAllListeners();
                    PlayerInputHandler.onInteractEvent.RemoveAllListeners();
                    PlayerInputHandler.onJumpEvent.RemoveAllListeners();
                    PlayerInputHandler.onSprintEvent.RemoveAllListeners();
                    PlayerAnimator = null;
                    PlayerController = null;
                    PickupController = null;
                    break;
                case TransitionState.Cutout:
                    PlayerCamera.transform.position = new Vector3(-4f, 3f, -4f);
                    break;
                case TransitionState.Finished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}