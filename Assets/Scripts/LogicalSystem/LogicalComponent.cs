using System;
using System.Collections.Generic;
using System.Linq;
using LogicalSystem.Interfaces;
using LogicalSystem.Utils;
using TypeReferences;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Utils.Extensions;
using Logger = Utils.Logger;

namespace LogicalSystem
{
    [DisallowMultipleComponent]
    public class LogicalComponent : ConnectableComponent
    {
        [Inherits(typeof(ILogicalComponent), ShortName = true, ShowNoneElement = false), SerializeField]
        private TypeReference logicalType;

        [SerializeField] private List<ConnectableComponent> inputsList;

        private ILogicalComponent _logicalComponent;
        private ConnectableComponent[] _inputsArray;
        private bool[] _cachedInputs;
        private bool _cachedResult;

        public TypeReference LogicalType => logicalType;

        private void Awake()
        {
            if (logicalType == null) return;

            _logicalComponent = Activator.CreateInstance(logicalType) as ILogicalComponent;
            
            inputsList.ClearList();
            
            _inputsArray = inputsList.ToArray();

            foreach (var connector in _inputsArray)
            {
                connector.ValueChanged += Recalculate;
            }
        }

        private void Start()
        {
            Recalculate();
        }

        private void OnDestroy()
        {
            foreach (var connector in _inputsArray)
            {
                connector.ValueChanged -= Recalculate;
            }
        }

        private void Recalculate()
        {
            _cachedInputs = _inputsArray.Select(x => x.Result).ToArray();
            _cachedResult = _logicalComponent.Calculate(_cachedInputs);

            Logger.Log(LoggerChannel.LogicalSystem, Priority.Info,
                $"(LogicalComponent) - {name}. Value is: {_cachedResult}");

            if (_cachedResult == Result) return;

            Result = _cachedResult;
            OnValueChanged();
        }

#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Handles.Label(transform.position, $"<color=#FFFFFF>{name}</color>", HandlesDrawer.GUIStyle);
            if (_inputsArray?.Length > 0)
            {
                foreach (var connector in _inputsArray)
                {
                    if (connector == null) continue;

                    var position = connector.transform.position;

                    Handles.color = connector.Result ? Color.green : Color.red;
                    Handles.DrawLine(position, transform.position, 1f);
                }
            }
            else if (inputsList.Count > 0)
            {
                foreach (var connector in inputsList)
                {
                    if (connector == null) continue;

                    var position = connector.transform.position;

                    Handles.color = connector.Result ? Color.green : Color.red;
                    Handles.DrawLine(position, transform.position, 2f);
                }
            }
        }
#endif
    }
}