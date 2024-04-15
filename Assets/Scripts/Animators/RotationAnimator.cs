using System;
using Animations;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Animators
{
    public class RotationAnimator : BaseAnimator
    {
        [SerializeField] private Vector3 offRotation;
        [SerializeField] private Vector3 onRotation;
        [SerializeField] private bool additiveRotation;
        [SerializeField] private bool isLocalRotation;
        [SerializeField] private bool needAnimationOnStart;
        [SerializeField] private Transform overrideTransform;
        [SerializeField] private RotateMode rotateMode;

        private Vector3 _initialRotation;
        
        protected override void Awake()
        {
            base.Awake();
            overrideTransform ??= transform;
            _initialRotation = isLocalRotation ? overrideTransform.localEulerAngles :  overrideTransform.eulerAngles;
        }

        private void Start()
        {
            if(needAnimationOnStart)
                Animate(IsEnabled);
        }

        protected override BaseAnimation CreateAnimation(bool value, float duration)
        {
            var targetRotation = value ? onRotation : offRotation;
            if (additiveRotation)
                targetRotation += _initialRotation;
            return new RotationAnimation(overrideTransform, targetRotation, duration, rotateMode,
                isLocalRotation);
        }
    }
}