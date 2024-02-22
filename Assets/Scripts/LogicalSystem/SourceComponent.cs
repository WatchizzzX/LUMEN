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
    /// <summary>
    /// The component that emits the logical signal
    /// </summary>
    [DisallowMultipleComponent]
    public class SourceComponent : ConnectableComponent
    {
        #region Serialized Fields

        /// <summary>
        /// Selected source type
        /// </summary>
        [Tooltip("Source type")]
        [Inherits(typeof(ISourceComponent), ShortName = true, ShowNoneElement = false), SerializeField]
        private TypeReference logicalType;

        #endregion

        #region Private Variables

        /// <summary>
        /// Instance of SourceComponent
        /// </summary>
        private ISourceComponent _logicalComponent;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            if (logicalType != null)
            {
                _logicalComponent = Activator.CreateInstance(logicalType) as ISourceComponent;
            }
        }

#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            Handles.Label(transform.position, $"<color=#FFFFFF>{name}</color>", HandlesDrawer.GUIStyle);
        }
#endif

        #endregion

        #region Methods

        /// <summary>
        /// Public void that allow other components interact with SourceComponent
        /// </summary>
        public void Interact()
        {
            Result = !Result;
            Logger.Log(LoggerChannel.LogicalSystem, Priority.Info, $"(SourceComponent) - {name}. Value is: {Result}");
            OnValueChanged();
        }

        #endregion
    }
}