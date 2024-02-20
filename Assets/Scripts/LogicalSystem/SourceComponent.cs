using System;
using LogicalSystem.Interfaces;
using TypeReferences;
using UnityEngine;

namespace LogicalSystem
{
    [DisallowMultipleComponent]
    public class SourceComponent: ConnectableComponent
    {
        [Inherits(typeof(ISourceComponent), ShortName = true, ShowNoneElement = false), SerializeField]
        private TypeReference logicalType;

        private ISourceComponent _logicalComponent;

        private void Awake()
        {
            if (logicalType != null)
            {
                _logicalComponent = Activator.CreateInstance(logicalType) as ISourceComponent;
            }
        }

        public void Interact()
        {
            Result = !Result;
            Debug.Log($"{name}-Source now is: {Result}");
            OnValueChanged();
        }
    }
}