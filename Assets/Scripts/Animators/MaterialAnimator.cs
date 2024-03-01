using DG.Tweening;
using UnityEngine;

namespace Animators
{
    public class MaterialAnimator : MonoBehaviour
    {
        [SerializeField] private Color offColor = Color.black;
        [SerializeField] private Color onColor = Color.green;
        [SerializeField] private float transitionDuration = 1.5f;

        private bool _isEnabled;
        
        private Material _material;

        private void Awake()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            _material = meshRenderer.material;
            _material.color = offColor;
        }

        public void Animate(bool value)
        {
            _isEnabled = value;
            AnimateMaterial();
        }
        
        public void Animate()
        {
            _isEnabled = !_isEnabled;
            AnimateMaterial();
        }

        private void AnimateMaterial()
        {
            _material.DOColor(_isEnabled ? onColor : offColor, transitionDuration);
        }
    }
}