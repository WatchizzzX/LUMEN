using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> onMoveEvent;
        [SerializeField] private UnityEvent<bool> onSprintEvent;
        [SerializeField] private UnityEvent onJumpEvent;
        [SerializeField] private UnityEvent<bool> onInteractEvent;

        private PlayerInput _input;

        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;

        public Vector2 Move { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsInteracting { get; private set; }

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
                //TODO: add logging on incorrect input map
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

        private void OnInteract(InputAction.CallbackContext obj)
        {
            IsInteracting = obj.ReadValueAsButton();
            onInteractEvent.Invoke(IsInteracting);
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            IsJumping = obj.ReadValueAsButton();
            if (IsJumping)
                onJumpEvent.Invoke();
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            IsSprinting = obj.ReadValueAsButton();
            onSprintEvent.Invoke(IsSprinting);
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            Move = obj.ReadValue<Vector2>();
            onMoveEvent.Invoke(Move);
        }
    }
}