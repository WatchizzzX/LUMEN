using System;
using System.Collections.Generic;
using System.Linq;
using LogicalSystem.Interfaces;
using LogicalSystem.Utils;
using TypeReferences;
using UnityEditor;
using UnityEngine;
using Utils;
using Utils.Extensions;
using Logger = Utils.Logger;

namespace LogicalSystem
{
    /// <summary>
    /// MonoBehaviour class that implements ILogicalComponent
    /// </summary>
    [DisallowMultipleComponent]
    [SelectionBase]
    public class LogicalComponent : ConnectableComponent
    {
        #region Serialized Fields

        /// <summary>
        /// Type of selected logical operator
        /// </summary>
        [Inherits(typeof(ILogicalComponent), ShortName = true, ShowNoneElement = false), SerializeField]
        [Tooltip("Type of logical operator")]
        private TypeReference logicalType;

        /// <summary>
        /// List of inputs
        /// </summary>
        [Tooltip("List of inputs")] [SerializeField]
        private List<ConnectableComponent> inputsList;

        #endregion

        #region Private Variables

        /// <summary>
        /// Internal instance of selected LogicalComponent
        /// </summary>
        private ILogicalComponent _logicalComponent;

        /// <summary>
        /// Array of inputs. Preferred then list of inputs
        /// </summary>
        private ConnectableComponent[] _inputsArray;

        /// <summary>
        /// Cached inputs from last calculate
        /// </summary>
        private bool[] _cachedInputs;

        /// <summary>
        /// Cached result from last calculate
        /// </summary>
        private bool _cachedResult;

        #endregion

        #region Public Fields

        public TypeReference LogicalType => logicalType;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            if (logicalType == null) return;

            _logicalComponent = Activator.CreateInstance(logicalType) as ILogicalComponent;

            inputsList.ClearListFromNulls();

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


#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Handles.Label(transform.position, $"<color=#FFFFFF>{name}</color>", HandlesDrawer.GUIStyle);
            if (_inputsArray is { Length: > 0 })
            {
                foreach (var connector in _inputsArray)
                {
                    if (connector == null) continue;

                    var position = connector.transform.position;

                    Handles.color = connector.Result ? Color.green : Color.red;
                    Handles.DrawLine(position, transform.position, 2f);
                }
            }
            else if (inputsList is { Count: > 0 })
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

        #endregion

        #region Methods

        /// <summary>
        /// Calculate value on internal LogicalComponent and inputs
        /// </summary>
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

        #endregion
    }
}