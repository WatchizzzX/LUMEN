using DG.Tweening;
using UnityEngine;

namespace LogicalSystem
{
    /// <summary>
    /// A simple class that implements changing the color of the cable, depending on the signal from the input
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class CableController : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Color when cable is enable
        /// </summary>
        [Tooltip("Color when cable is enable")] [SerializeField]
        private Color onColor = Color.yellow;

        /// <summary>
        /// Color when cable is disable
        /// </summary>
        [Tooltip("Color when cable is disable")] [SerializeField]
        private Color offColor = Color.black;

        /// <summary>
        /// Time to change color
        /// </summary>
        [Tooltip("Time to change color")] [SerializeField]
        private float changeTime = 0.5f;

        /// <summary>
        /// Input ConnectableComponent
        /// </summary>
        [Tooltip("Input ConnectableComponent")] [SerializeField]
        public ConnectableComponent input;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached MeshRenderer
        /// </summary>
        private MeshRenderer _meshRenderer;

        /// <summary>
        /// Internal state
        /// </summary>
        private bool _isEnabled;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _isEnabled = input.Result;
            _meshRenderer.material.color = offColor;
            input.ValueChanged += OnValueChanged;
        }

        private void OnDestroy()
        {
            input.ValueChanged -= OnValueChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Callback when value of input is changed
        /// </summary>
        private void OnValueChanged()
        {
            _isEnabled = input.Result;
            ChangeColor();
        }

        /// <summary>
        /// Change color of cable. Based on internal state
        /// </summary>
        private void ChangeColor()
        {
            _meshRenderer.material.DOColor(_isEnabled ? onColor : offColor, "_EmissionColor", changeTime);
        }

        #endregion
    }
}