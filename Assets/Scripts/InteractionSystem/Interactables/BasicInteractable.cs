using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace InteractionSystem.Interactables
{
    public abstract class BasicInteractable : MonoBehaviour, IInteractable
    {
        [Header("Basic Interactable Settings")] [SerializeField]
        private InteractableType interactableType;

        [SerializeField] private string message;

        private InteractableType _interactableType;
        private string _message;

        protected virtual void Awake()
        {
            _interactableType = interactableType;
            _message = message.Trim() == "" ? "NULL" : message;

            if (gameObject.layer != LayerMask.NameToLayer("Interactable"))
            {
                gameObject.layer = LayerMask.NameToLayer("Interactable");
                Logger.Log(LoggerChannel.InteractableSystem, Priority.Warning,
                    $"{name} not assigned to Interactable layer. " +
                    $"It will be switched, but you need to change it in inspector");
            }

            if (_message == "NULL")
                Logger.Log(LoggerChannel.InteractableSystem, Priority.Warning,
                    $"{name} doesn't have message. It will be NULL");
        }

        public InteractableType GetInteractableType() => _interactableType;

        public string GetMessage() => _message;

        public abstract void Interact();
        
        public abstract void Interact(InteractorController interactor);
    }
}