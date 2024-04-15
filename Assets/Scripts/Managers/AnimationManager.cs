using System;
using System.Collections.Generic;
using System.Linq;
using Animations;
using DG.Tweening;
using DG.Tweening.Core;
using ServiceLocatorSystem;
using UnityEngine;

namespace Managers
{
    public class AnimationManager : MonoBehaviour, IService
    {
        private List<BaseAnimation> _animationsList;
        private AnimationsComparer _animationsComparer;

        private void Awake()
        {
            _animationsList = new List<BaseAnimation>();
            _animationsComparer = new AnimationsComparer();
        }

        private void Update()
        {
            var groupedAnimations = GetSimilarAnimations(_animationsList);
            foreach (var animationGroup in groupedAnimations)
            {
                if (animationGroup.Length == 0) return;
                AnimateGroup(animationGroup);
                foreach (var baseAnimation in animationGroup)
                {
                    _animationsList.Remove(baseAnimation);
                }
            }
        }

        private void AnimateGroup(BaseAnimation[] animations)
        {
            switch (animations[0].AnimationParameterType)
            {
                case AnimationParameterType.Color:
                    var colorAnimations = Array.ConvertAll(animations, x => (ColorAnimation)x);
                    DOTween.To(() => colorAnimations[0].Color,
                        x =>
                        {
                            foreach (var colorAnimation in colorAnimations)
                            {
                                colorAnimation.Color = x;
                            }
                        },
                        colorAnimations[0].TargetColor,
                        colorAnimations[0].Duration);
                    break;
                case AnimationParameterType.Rotation:
                    var rotationAnimations = Array.ConvertAll(animations, x => (RotationAnimation)x);
                    DOTween.To(() => rotationAnimations[0].Rotation,
                        x =>
                        {
                            foreach (var rotationAnimation in rotationAnimations)
                            {
                                rotationAnimation.Rotation = x;
                            }
                        },
                        rotationAnimations[0].TargetRotation,
                        rotationAnimations[0].Duration).plugOptions.rotateMode = rotationAnimations[0].RotateMode;
                    break;
                case AnimationParameterType.Float:
                    var floatAnimations = Array.ConvertAll(animations, x => (FloatAnimation)x);
                    DOTween.To(() => floatAnimations[0].Value,
                        x =>
                        {
                            foreach (var floatAnimation in floatAnimations)
                            {
                                floatAnimation.Value = x;
                            }
                        },
                        floatAnimations[0].TargetValue,
                        floatAnimations[0].Duration);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<BaseAnimation[]> GetSimilarAnimations(List<BaseAnimation> animations)
        {
            return animations.GroupBy(x => x, _animationsComparer)
                .Select(group => group.ToArray())
                .ToArray();
        }

        public void AddAnimationToQueue(BaseAnimation baseAnimation)
        {
            _animationsList.Add(baseAnimation);
        }
    }
}