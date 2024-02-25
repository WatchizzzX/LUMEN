using DG.Tweening;
using UnityEngine;

namespace Animators
{
    /// <summary>
    /// Animator for lever
    /// </summary>
    public class LeverAnimator : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Transform of lever handle
        /// </summary>
        [Tooltip("Transform of lever handle")]
        [SerializeField] private Transform leverTransform;
        
        /// <summary>
        /// Angle of handle when is off
        /// </summary>
        [Tooltip("Angle of handle when is off")]
        [SerializeField] private float offAngle;
        
        /// <summary>
        /// Angle of handle when is on
        /// </summary>
        [Tooltip("Angle of handle when is on")]
        [SerializeField] private float onAngle;
        
        /// <summary>
        /// Duration of transition
        /// </summary>
        [Tooltip("Duration of transition")]
        [SerializeField] private float rotationSpeed;

        #endregion

        #region Methods

        public void Animate(bool value)
        {
            leverTransform.DOLocalRotate(value ? new Vector3(onAngle, 0f, 0f) : new Vector3(offAngle, 0f, 0f),
                rotationSpeed);
        }        

        #endregion
    }
}