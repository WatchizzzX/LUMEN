using System;
using System.Collections.Generic;
using System.Linq;
using LogicalSystem.Interfaces;
using TypeReferences;
using UnityEngine;

namespace LogicalSystem
{
    [DisallowMultipleComponent]
    public class LogicalComponent : ConnectableComponent
    {
        [Inherits(typeof(ILogicalComponent), ShortName = true, ShowNoneElement = false), SerializeField]
        private TypeReference logicalType;

        [SerializeField] private List<ConnectableComponent> inputs;

        private ILogicalComponent _logicalComponent;
        private ConnectableComponent[] _inputs;
        private bool[] _cachedInputs;
        private bool _cachedResult;

        private void Awake()
        {
            if (logicalType == null) return;
            
            _logicalComponent = Activator.CreateInstance(logicalType) as ILogicalComponent;
            _inputs = inputs.ToArray();

            foreach (var connector in _inputs)
            {
                connector.ValueChanged += Recalculate;
            }
        }

        private void OnDestroy()
        {
            foreach (var connector in _inputs)
            {
                connector.ValueChanged -= Recalculate;
            }
        }

        private void Recalculate()
        {
            _cachedInputs = _inputs.Select(x => x.Result).ToArray();
            _cachedResult = _logicalComponent.Calculate(_cachedInputs);
            
            Debug.Log($"{name}-Logical recalculated: {_cachedResult}");

            if (_cachedResult == Result) return;
            
            Result = _cachedResult;
            OnValueChanged();
        }
    }
}