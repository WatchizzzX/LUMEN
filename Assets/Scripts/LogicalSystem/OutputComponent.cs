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
    public class OutputComponent : MonoBehaviour
    {
        [SerializeField] private List<ConnectableComponent> inputsList;
        [SerializeField] private UnityEvent onChange;
        [SerializeField] private UnityEvent<bool> onChangeWithValue;
        
        private ConnectableComponent[] _inputsArray;
        private bool[] _cachedInputs;
        private bool _cachedResult;

        private void Awake()
        {
            inputsList.ClearListFromNulls();
            
            _inputsArray = inputsList.ToArray();

            foreach (var connector in _inputsArray)
            {
                connector.ValueChanged += Recalculate;
            }
        }

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
        
#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Handles.Label(transform.position,$"<color=#FFFFFF>{name}</color>", HandlesDrawer.GUIStyle);
            if (_inputsArray?.Length > 0)
            {
                foreach (var connector in _inputsArray)
                {
                    if(connector == null) continue;
                    
                    var position = connector.transform.position;

                    Handles.color = connector.Result ? Color.green : Color.red;
                    Handles.DrawLine(position, transform.position, 1f);
                }
            }
            else if (inputsList.Count > 0)
            {
                foreach (var connector in inputsList)
                {
                    if(connector == null) continue;
                    
                    var position = connector.transform.position;

                    Handles.color = connector.Result ? Color.green : Color.red;
                    Handles.DrawLine(position, transform.position, 2f);
                }
            }
        }
#endif
    }
}