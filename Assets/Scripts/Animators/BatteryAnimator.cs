using DG.Tweening;
using UnityEngine;

namespace Animators
{
    public class BatteryAnimator : MonoBehaviour
    {
        [SerializeField] private Color offColor = Color.black;
        [SerializeField] private Color onColor = Color.yellow;
        [SerializeField] private float transitionDuration = 0.5f;
        [SerializeField] private GameObject vfxContainer;

        private MeshRenderer _meshRenderer;

        private Material _materialFirst;
        private Material _materialSecond;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _materialFirst = _meshRenderer.materials[2];
            _materialSecond = _meshRenderer.materials[3];
        }

        public void Animate(bool value)
        {
            if (value)
            {
                _materialFirst.DOColor(onColor, EmissionColor, transitionDuration);
                _materialSecond.DOColor(onColor, EmissionColor, transitionDuration);
                vfxContainer.SetActive(true);
            }
            else
            {
                _materialFirst.DOColor(offColor, EmissionColor, transitionDuration);
                _materialSecond.DOColor(offColor, EmissionColor, transitionDuration);
                vfxContainer.SetActive(false);
            }
        }
    }
}