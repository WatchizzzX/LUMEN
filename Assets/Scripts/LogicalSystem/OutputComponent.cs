using System.Collections.Generic;
using System.Linq;
using LogicalSystem.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Utils.Extensions;
using Logger = Utils.Logger;

namespace LogicalSystem
{
    /// <summary>
    /// The component that outputs the signal from the LogicalSystem
    /// </summary>
    public class OutputComponent : MonoBehaviour
    {
        #region Serialized Fileds

        /// <summary>
        /// Inputs list
        /// </summary>
        [Tooltip("Inputs list")] [SerializeField]
        private List<ConnectableComponent> inputsList;

        /// <summary>
        /// Event on value changed
        /// </summary>
        [Tooltip("On value changed")] [SerializeField]
        private UnityEvent onChange;

        /// <summary>
        /// Event on value changed with internal value
        /// </summary>
        [Tooltip("On value changed with value")] [SerializeField]
        private UnityEvent<bool> onChangeWithValue;

        #endregion

        #region Private Variables

        /// <summary>
        /// Array of inputs
        /// </summary>
        private ConnectableComponent[] _inputsArray;

        /// <summary>
        /// Cached inputs on last calculated
        /// </summary>
        private bool[] _cachedInputs;

        /// <summary>
        /// Cached result on last calculated
        /// </summary>
        private bool _cachedResult;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            inputsList.ClearListFromNulls();

            _inputsArray = inputsList.ToArray();

            foreach (var connector in _inputsArray)
            {
                connector.ValueChanged += Recalculate;
            }
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

        #endregion

        #region Methods

        /// <summary>
        /// Calculate value on internal LogicalComponent and inputs
        /// </summary>
        private void Recalculate()
        {
            _cachedInputs = _inputsArray.Select(x => x.Result).ToArray();
            var result = _cachedInputs.All(item => item);

            Logger.Log(LoggerChannel.LogicalSystem, Priority.Info, $"(OutputComponent) - {name}. Value is: {result}");

            if (_cachedResult == result) return;

            _cachedResult = result;
            onChange.Invoke();
            onChangeWithValue.Invoke(_cachedResult);
        }

        #endregion
    }
}