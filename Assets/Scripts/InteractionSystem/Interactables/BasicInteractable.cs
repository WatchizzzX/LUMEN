using UnityEngine;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// An abstract class for objects that the InteractorController should interact with
    /// </summary>
    public abstract class BasicInteractable : MonoBehaviour, IInteractable
    {
        #region Serialized Fields

        [Tooltip("The message that will be displayed if this object is selected as active (WIP)")]
        [SerializeField] private string message;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached InteractableType
        /// </summary>
        protected InteractableType InteractableType;
        
        /// <summary>
        /// Cached message
        /// </summary>
        private string _message;

        #endregion

        #region MonoBehaviour

        protected virtual void Awake()
        {
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
        #endregion

        #region Interface Realizations

        public InteractableType GetInteractableType() => InteractableType;

        public string GetMessage() => _message;

        public abstract void Interact();

        public abstract void Interact(InteractorController interactor);

        protected abstract void OnInteract();

        #endregion
    }
}