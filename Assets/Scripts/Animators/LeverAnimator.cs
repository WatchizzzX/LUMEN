using DG.Tweening;
using UnityEngine;

namespace Animators
{
    public class LeverAnimator : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Transform leverTransform;
        [SerializeField] private float offAngle;
        [SerializeField] private float onAngle;
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