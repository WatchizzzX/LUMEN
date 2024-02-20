using System;
using LogicalSystem.Interfaces;
using LogicalSystem.Utils;
using TypeReferences;
using UnityEditor;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

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
            Logger.Log(LoggerChannel.LogicalSystem, Priority.Info, $"(SourceComponent) - {name}. Value is: {Result}");
            OnValueChanged();
        }
        
#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Handles.Label(transform.position,$"<color=#FFFFFF>{name}</color>", HandlesDrawer.GUIStyle);
        }
#endif
    }
}