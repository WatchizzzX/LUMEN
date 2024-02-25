using UnityEngine;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Standard implementation of interactable lamp with switch mechanism
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class InteractableLamp : InteractableSwitch
    {
        #region Serialized Fields

        /// <summary>
        /// Color of material when lamp is enabled
        /// </summary>
        [Space(2f)]
        [Header("Interactable Lamp settings")]
        [Tooltip("Color of material when lamp is enabled")]
        [SerializeField]
        private Color offColor = Color.black;

        /// <summary>
        /// Color of material when lamp is disabled
        /// </summary>
        [Tooltip("Color of material when lamp is disabled")] [SerializeField]
        private Color onColor = Color.yellow;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached MeshRender
        /// </summary>
        private MeshRenderer _meshRenderer;

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

        protected override void OnInteract()
        {
            base.OnInteract();
            _meshRenderer.material.color = IsEnabled ? onColor : offColor;
        }

        #endregion
    }
}