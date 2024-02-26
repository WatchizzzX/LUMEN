using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace Animators
{
    public class PlatformAnimator : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private Vector3 offPosition;
        [SerializeField] private Vector3 onPosition;
        [SerializeField] private float transitionDuration;
        [SerializeField] private UnityEvent onFinishedInStart;
        [SerializeField] private UnityEvent onFinishedInEnd;

        #endregion

        #region Private Variables

        private float _distance;
        private TweenerCore<Vector3, Vector3, VectorOptions> _tweenerCore;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _distance = Vector3.Distance(onPosition, offPosition);
        }

        #endregion

        #region Methods

        public void Animate(bool value)
        {
            var currentDistance = Vector3.Distance(transform.position, value ? onPosition : offPosition);
            var calculatedDuration = transitionDuration * (currentDistance / _distance);
            _tweenerCore?.Kill();
            _tweenerCore = transform.DOMove(value ? onPosition : offPosition, calculatedDuration)
                .OnComplete(() => HandleEvent(value)).SetUpdate(UpdateType.Fixed).SetEase(Ease.InOutSine);
        }

        private void HandleEvent(bool value)
        {
            if (value)
            {
                onFinishedInEnd.Invoke();
            }
            else
            {
                onFinishedInStart.Invoke();
            }
        }

        #endregion
    }
}