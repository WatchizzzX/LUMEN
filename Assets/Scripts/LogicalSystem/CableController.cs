using System;
using Animators;
using UnityEngine;
using UnityEngine.Serialization;

namespace LogicalSystem
{
    [RequireComponent(typeof(MaterialAnimator))]
    public class CableController : MonoBehaviour
    {
        [SerializeField] private ConnectableComponent inputComponent;

        [SerializeField]
        private MaterialAnimator materialAnimator;

        private void OnValidate()
        {
            materialAnimator = GetComponent<MaterialAnimator>();
        }

        private void Awake()
        {
            if (inputComponent)
                inputComponent.ValueChanged += materialAnimator.Animate;
        }

        private void Start()
        {
            materialAnimator.Animate(inputComponent.Result);
        }
    }
}