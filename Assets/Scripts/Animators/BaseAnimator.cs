using System;
using Animations;
using Animators.Interfaces;
using Managers;
using ServiceLocatorSystem;
using UnityEngine;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace Animators
{
    public class BaseAnimator : MonoBehaviour, IAnimator
    {
        [SerializeField] protected float transitionDuration;

        private AnimationManager _animationManager;

        protected bool IsEnabled;

        protected virtual void Awake()
        {
            if (!ServiceLocator.TryGet(out _animationManager))
                Logger.Log(LoggerChannel.AnimationManager, Priority.Warning,
                    $"Animator on {transform.name} can't initialize, because can't find AnimationManager");
        }

        public virtual void Animate()
        {
            IsEnabled = !IsEnabled;
            ProduceAnimation(IsEnabled, transitionDuration);
        }

        public virtual void Animate(bool value)
        {
            ProduceAnimation(value, transitionDuration);
        }

        public virtual void Animate(bool value, float duration)
        {
            ProduceAnimation(value, duration);
        }

        private void ProduceAnimation(bool value, float duration)
        {
            var createdAnimation = CreateAnimation(value, duration);
            _animationManager.AddAnimationToQueue(createdAnimation);
        }

        protected virtual BaseAnimation CreateAnimation(bool value, float duration)
        {
            throw new NotImplementedException("Create animation method was not implemented");
        }
    }
}