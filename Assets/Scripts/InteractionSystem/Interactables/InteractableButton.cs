using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Standard implementation of interactable button with event OnClick 
    /// </summary>
    [SelectionBase]
    public class InteractableButton : BasicInteractable
    {
        #region Serialized Fields

        /// <summary>
        /// Event when button is clicked
        /// </summary>
        [Space(2f)] [Header("Interactable Button settings")] [SerializeField]
        private UnityEvent onClick;

        [SerializeField] private float cooldownTimer;

        #endregion

        protected bool IsInCooldown;

        protected override void Awake()
        {
            base.Awake();
            InteractableType = InteractableType.Button;
        }

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
            if (IsInCooldown) return;
            onClick.Invoke();
            StartCoroutine(StartCooldownTimer());
        }

        private IEnumerator StartCooldownTimer()
        {
            IsInCooldown = true;

            yield return new WaitForSecondsRealtime(cooldownTimer);

            IsInCooldown = false;
        }

        #endregion
    }
}