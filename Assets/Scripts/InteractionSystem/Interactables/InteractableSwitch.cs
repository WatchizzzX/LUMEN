using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Interactables
{
    public class InteractableSwitch : InteractableButton
    {
        #region Serialized Fields

        [Space(2f)] [Header("Interactable Switch settings")] [SerializeField]
        private UnityEvent<bool> onValueChanged;

        [SerializeField] private bool disableInteractingWithController;

        #endregion

        #region Private Variables

        protected bool IsEnabled;

        #endregion

        #region Override Methods

        public override void Interact(InteractorController interactor)
        {
            if (disableInteractingWithController) return;
            
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

        protected override void OnInteract()
        {
            base.OnInteract();
            onValueChanged.Invoke(IsEnabled);
        }

        #endregion
    }
}