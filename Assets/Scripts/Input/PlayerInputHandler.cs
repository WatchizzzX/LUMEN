using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utils;
using Logger = Utils.Logger;

namespace Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// On change move direction
        /// </summary>
        [Tooltip("On change move direction")]
        [SerializeField] private UnityEvent<Vector2> onMoveEvent;
        
        /// <summary>
        /// On change sprint state
        /// </summary>
        [Tooltip("On change sprint state")]
        [SerializeField] private UnityEvent<bool> onSprintEvent;
        
        /// <summary>
        /// Trigger on jump
        /// </summary>
        [Tooltip("Trigger on jump")]
        [SerializeField] private UnityEvent onJumpEvent;
        
        /// <summary>
        /// Trigger on interact
        /// </summary>
        [Tooltip("Trigger on interact")]
        [SerializeField] private UnityEvent onInteractEvent;

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

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();

            var actionMap = _input.currentActionMap;

            try
            {
                _moveAction = actionMap.FindAction("Move", true);
                _sprintAction = actionMap.FindAction("Sprint", true);
                _jumpAction = actionMap.FindAction("Jump", true);
                _interactAction = actionMap.FindAction("Interact", true);
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
        }

        #endregion

        #region Events

        /// <summary>
        /// Event on interact key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnInteract(InputAction.CallbackContext obj)
        {
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
            IsSprinting = obj.ReadValueAsButton();
            onSprintEvent.Invoke(IsSprinting);
        }

        /// <summary>
        /// Event on move key pressed or unpressed
        /// </summary>
        /// <param name="obj">CallbackContext</param>
        private void OnMove(InputAction.CallbackContext obj)
        {
            Move = obj.ReadValue<Vector2>();
            onMoveEvent.Invoke(Move);
        }

        #endregion
    }
}