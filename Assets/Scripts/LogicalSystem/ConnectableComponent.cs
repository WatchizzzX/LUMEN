using System;
using UnityEngine;

namespace LogicalSystem
{
    /// <summary>
    /// An abstract class for all elements that should be attached to other elements of LogicalSystem
    /// </summary>
    public abstract class ConnectableComponent : MonoBehaviour
    {
        /// <summary>
        /// Event on value changed
        /// </summary>
        public event Action ValueChanged;

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