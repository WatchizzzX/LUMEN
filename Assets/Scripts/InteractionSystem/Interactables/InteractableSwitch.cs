using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Interactables
{
    public class InteractableSwitch : InteractableButton
    {
        #region Serialized Fields

        /// <summary>
        /// Event when switch is changed value
        /// </summary>
        [Space(2f)] [Header("Interactable Switch settings")] [SerializeField]
        private UnityEvent<bool> onValueChanged;

        /// <summary>
        /// Disable interacting with object through InteractorController?
        /// </summary>
        [Tooltip("Disable interacting with object through InteractorController?")] [SerializeField]
        private bool disableInteractingWithController;

        #endregion

        #region Private Variables

        protected bool IsEnabled;

        #endregion

        #region Override Methods

        public override void Interact(InteractorController interactor)
        {
            if (disableInteractingWithController) return;
            if (IsInCooldown) return;
            
            base.Interact(interactor);
            InteractSwitch(!IsEnabled);
        }

        #endregion

        #region Methods

        public void InteractSwitch(bool value)
        {
            IsEnabled = value;
            OnInteract();
        }

        public void ChangeAbilityToInteractive()
        {
            disableInteractingWithController = !disableInteractingWithController;
        }

        protected override void OnInteract()
        {
            base.OnInteract();
            onValueChanged.Invoke(IsEnabled);
        }

        #endregion
    }
}