using System;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.InputSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;
using Logger = Utils.Logger;

namespace Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : EventBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// On change move direction
        /// </summary>
        [Tooltip("On change move direction")] public UnityEvent<Vector2> onMoveEvent;

        /// <summary>
        /// On change sprint state
        /// </summary>
        [Tooltip("On change sprint state")] public UnityEvent<bool> onSprintEvent;

        /// <summary>
        /// Trigger on jump
        /// </summary>
        [Tooltip("Trigger on jump")] public UnityEvent onJumpEvent;

        /// <summary>
        /// Trigger on interact
        /// </summary>
        [Tooltip("Trigger on interact")] public UnityEvent onInteractEvent;

        /// <summary>
        /// Trigger on interact
        /// </summary>
        [Tooltip("Trigger on pickup")] public UnityEvent onPickupEvent;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached PlayerInput
        /// </summary>
        private PlayerInput _input;

        /// <summary>
        /// Action for move direction
        /// </summary>
        private InputAction _moveAction;

        /// <summary>
        /// Action for sprint
        /// </summary>
        private InputAction _sprintAction;

        /// <summary>
        /// Action for jump
        /// </summary>
        private InputAction _jumpAction;

        /// <summary>
        /// Action for interact
        /// </summary>
        private InputAction _interactAction;

        /// <summary>
        /// Action for pickup
        /// </summary>
        private InputAction _pickupAction;

        private InputAction _pauseAction;

        private bool _isInputEnabled;

        private GameState _lastGameState;

        #endregion

        #region Public Fields

        /// <summary>
        /// Move direction
        /// </summary>
        public Vector2 Move { get; private set; }

        /// <summary>
        /// Sprint state
        /// </summary>
        public bool IsSprinting { get; private set; }

        /// <summary>
        /// Jumping state
        /// </summary>
        public bool IsJumping { get; private set; }

        /// <summary>
        /// Interacting state
        /// </summary>
        public bool IsInteracting { get; private set; }

        /// <summary>
        /// Pickup state
        /// </summary>
        public bool IsPickup { get; private set; }

        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
            _input = GetComponent<PlayerInput>();
            _isInputEnabled = true;

            var actionMap = _input.currentActionMap;

            try
            {
                _moveAction = actionMap.FindAction("Move", true);
                _sprintAction = actionMap.FindAction("Sprint", true);
                _jumpAction = actionMap.FindAction("Jump", true);
                _interactAction = actionMap.FindAction("Interact", true);
                _pickupAction = actionMap.FindAction("Pickup", true);
                _pauseAction = actionMap.FindAction("Pause", true);
            }
            catch
            {
                Logger.Log(LoggerChannel.Input, Priority.Error, "Some action can't be found. InputHandler will be off");
                enabled = false;
                return;
            }

            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _sprintAction.performed += OnSprint;
            _sprintAction.canceled += OnSprint;

            _jumpAction.performed += OnJump;
            _jumpAction.canceled += OnJump;

            _interactAction.performed += OnInteract;
            _interactAction.canceled += OnInteract;

            _pickupAction.performed += OnPickup;
            _pickupAction.canceled += OnPickup;

            _pauseAction.performed += OnPause;
            _pauseAction.canceled += OnPause;
        }

        #endregion

        #region Methods

        private void ChangeInputState(bool isEnabled)
        {
            _isInputEnabled = isEnabled;
            ChangeCursorVisible(false);

            if (_isInputEnabled) return;

            Move = Vector2.zero;
            IsJumping = false;
            IsSprinting = false;
            IsInteracting = false;

            onMoveEvent.Invoke(Move);
            onSprintEvent.Invoke(IsSprinting);
            
            ChangeCursorVisible(true);
        }

        private void ChangeCursorVisible(bool value)
        {
            Cursor.visible = value;
            Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        }

        [ListenTo(SignalEnum.OnDevConsoleOpened)]
        private void OnDevConsoleChangeState(EventModel eventModel)
        {
            if (_lastGameState is GameState.MainMenu or GameState.Paused) return;
            ChangeInputState(!((OnDevConsoleOpened)eventModel.Payload).IsOpened);
        }

        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnStartExitCutscene(EventModel eventModel)
        {
            ChangeInputState(false);
        }

        [ListenTo(SignalEnum.OnSpawnPlayer)]
        private void OnSpawnPlayer(EventModel eventModel)
        {
            ChangeInputState(true);
        }

        [ListenTo(SignalEnum.OnGameStateChanged)]
        private void OnGameStateChanged(EventModel eventModel)
        {
            var payload = (OnGameStateChanged)eventModel.Payload;
            _lastGameState = payload.GameState;
            switch (payload.GameState)
            {
                case GameState.Level:
                    ChangeInputState(true);
                    break;
                case GameState.Paused:
                    ChangeInputState(false);
                    break;
                case GameState.MainMenu:
                    ChangeInputState(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Events

        private void OnPause(InputAction.CallbackContext obj)
        {
            if (obj.ReadValueAsButton())
                RaiseEvent(SignalEnum.OnPauseKeyPressed);
        }

        /// <summary>
        /// Event on pickup key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnPickup(InputAction.CallbackContext obj)
        {
            if (!_isInputEnabled) return;
            IsPickup = obj.ReadValueAsButton();
            if (IsPickup)
                onPickupEvent.Invoke();
        }

        /// <summary>
        /// Event on interact key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnInteract(InputAction.CallbackContext obj)
        {
            if (!_isInputEnabled) return;
            IsInteracting = obj.ReadValueAsButton();
            if (IsInteracting)
                onInteractEvent.Invoke();
        }

        /// <summary>
        /// Event on jump key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnJump(InputAction.CallbackContext obj)
        {
            if (!_isInputEnabled) return;
            IsJumping = obj.ReadValueAsButton();
            if (IsJumping)
                onJumpEvent.Invoke();
        }

        /// <summary>
        /// Event on sprint key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnSprint(InputAction.CallbackContext obj)
        {
            if (!_isInputEnabled) return;
            IsSprinting = obj.ReadValueAsButton();
            onSprintEvent.Invoke(IsSprinting);
        }

        /// <summary>
        /// Event on move key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnMove(InputAction.CallbackContext obj)
        {
            if (!_isInputEnabled) return;
            Move = obj.ReadValue<Vector2>();
            onMoveEvent.Invoke(Move);
        }

        #endregion
    }
}