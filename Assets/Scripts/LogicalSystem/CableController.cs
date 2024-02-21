using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace LogicalSystem
{
    [RequireComponent(typeof(MeshRenderer))]
    public class CableController : MonoBehaviour
    {
        [SerializeField] private Color onColor = Color.yellow;
        [SerializeField] private Color offColor = Color.black;
        [SerializeField] private float changeTime = 0.5f;

        [SerializeField] public ConnectableComponent input;
        
        private MeshRenderer _meshRenderer;

        private bool _isEnabled;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _isEnabled = input.Result;
            _meshRenderer.material.color = offColor;
            input.ValueChanged += OnValueChanged;
        }

        private void OnDestroy()
        {
            input.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged()
        {
            _isEnabled = input.Result;
            ChangeColor();
        }

        private void ChangeColor()
        {
            _meshRenderer.material.DOColor(_isEnabled ? onColor : offColor, changeTime);
        }
    }
}