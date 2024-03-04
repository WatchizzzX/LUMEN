using Animators.Interfaces;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;

namespace Animators
{
    public class PlatformAnimator : MonoBehaviour, IAnimator
    {
        #region Serialized Fields

        [SerializeField] private Vector3 offPosition;
        [SerializeField] private Vector3 onPosition;
        [SerializeField] private float transitionDuration;
        [SerializeField] private UnityEvent onFinishedInStart;
        [SerializeField] private UnityEvent onFinishedInEnd;

        #endregion

        #region Private Variables

        private bool _isEnabled;
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

        public void Animate()
        {
            _isEnabled = !_isEnabled;
            StartAnimation(transitionDuration);
        }

        public void Animate(bool value)
        {
            if (_isEnabled == value) return;
            _isEnabled = value;
            StartAnimation(transitionDuration);
        }

        public void Animate(bool value, float duration)
        {
            if (_isEnabled == value) return;
            _isEnabled = value;
            StartAnimation(duration);
        }
        
        private void StartAnimation(float duration)
        {
            var currentDistance = Vector3.Distance(transform.position, _isEnabled ? onPosition : offPosition);
            var calculatedDuration = duration * (currentDistance / _distance);
            _tweenerCore?.Kill();
            _tweenerCore = transform.DOMove(_isEnabled ? onPosition : offPosition, calculatedDuration)
                .OnComplete(() => HandleEvent(_isEnabled)).SetUpdate(UpdateType.Fixed).SetEase(Ease.InOutSine);
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