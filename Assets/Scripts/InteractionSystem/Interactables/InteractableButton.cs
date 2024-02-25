using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Standard implementation of interactable button with event OnClick 
    /// </summary>
    public class InteractableButton : BasicInteractable
    {
        #region Serialized Fields

        [Space(2f)] [Header("Interactable Button settings")] [SerializeField]
        private UnityEvent onClick;

        #endregion

        #region Interface Realizations

        public override void Interact()
        {
            Interact(null);
        }

        public override void Interact(InteractorController interactor)
        {
            OnInteract();
        }

        protected override void OnInteract()
        {
            onClick.Invoke();
        }

        #endregion
    }
}