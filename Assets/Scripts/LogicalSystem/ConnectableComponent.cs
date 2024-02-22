using UnityEngine;

namespace LogicalSystem
{
    /// <summary>
    /// An abstract class for all elements that should be attached to other elements of LogicalSystem
    /// </summary>
    public abstract class ConnectableComponent : MonoBehaviour
    {
        /// <summary>
        /// ConnectorEvent delegate
        /// </summary>
        public delegate void ConnectorEvent();

        /// <summary>
        /// Event on value changed
        /// </summary>
        public event ConnectorEvent ValueChanged;

        /// <summary>
        /// Logical result
        /// </summary>
        public bool Result { protected set; get; }

        /// <summary>
        /// Wrapper for event
        /// </summary>
        protected void OnValueChanged()
        {
            ValueChanged?.Invoke();
        }
    }
}