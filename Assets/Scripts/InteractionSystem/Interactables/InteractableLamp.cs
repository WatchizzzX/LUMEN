using UnityEngine;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Standard implementation of interactable lamp with switch mechanism
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class InteractableLamp : BasicInteractable
    {
        #region Serialized Fields

        [Tooltip("Color of material when lamp is enabled")] [SerializeField]
        private Color offColor = Color.black;

        [Tooltip("Color of material when lamp is disabled")] [SerializeField]
        private Color onColor = Color.yellow;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached MeshRender
        /// </summary>
        private MeshRenderer _meshRenderer;

        /// <summary>
        /// Internal state of lamp
        /// </summary>
        private bool _isEnabled;

        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material.color = offColor;
        }

        #endregion

        #region Interface Realizations

        public override void Interact()
        {
            Interact(null);
        }

        public override void Interact(InteractorController interactor)
        {
            _isEnabled = !_isEnabled;
            _meshRenderer.material.color = _isEnabled ? onColor : offColor;
        }

        #endregion
    }
}