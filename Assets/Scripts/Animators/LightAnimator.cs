using DG.Tweening;
using UnityEngine;

namespace Animators
{
    public class LightAnimator : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private float targetIntensity;
        [SerializeField] private float targetRange;
        [SerializeField] private Color targetColor;
        [SerializeField] private Light targetLightSource;
        
        /// <summary>
        /// Duration of transition
        /// </summary>
        [Tooltip("Duration of transition")]
        [SerializeField] private float transitionDuration = 0.5f;

        #endregion

        #region Methods

        public void Animate(bool value)
        {
            DOTween.To(() => targetLightSource.range, x => targetLightSource.range = x, value ? targetRange : 0,
                transitionDuration);
            targetLightSource.DOIntensity(value ? targetIntensity : 0, transitionDuration);
            targetLightSource.DOColor(value ? targetColor : Color.black, transitionDuration);
        }

        #endregion
    }
}