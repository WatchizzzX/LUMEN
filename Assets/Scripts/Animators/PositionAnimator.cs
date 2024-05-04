using Animations;
using UnityEngine;

namespace Animators
{
    public class PositionAnimator : BaseAnimator
    {
        [SerializeField] private Vector3 offPosition;
        [SerializeField] private Vector3 onPosition;
        [SerializeField] private bool additivePosition;
        [SerializeField] private bool isLocalPosition;
        [SerializeField] private bool needAnimationOnStart;
        [SerializeField] private Transform overrideTransform;

        private Vector3 _initialPosition;

        protected override void Awake()
        {
            base.Awake();
            overrideTransform ??= transform;
            _initialPosition = isLocalPosition ? overrideTransform.localPosition : overrideTransform.position;
        }

        private void Start()
        {
            if (needAnimationOnStart)
                Animate(IsEnabled);
        }

        protected override BaseAnimation CreateAnimation(bool value, float duration)
        {
            var targetPosition = value ? onPosition : offPosition;
            if (additivePosition)
                targetPosition += _initialPosition;
            return new PositionAnimation(overrideTransform, targetPosition, duration, isLocalPosition);
        }
    }
}