using UnityEngine;

namespace LogicalSystem
{
    public abstract class ConnectableComponent : MonoBehaviour
    {
        public delegate void ConnectorEvent();
        public bool Result { protected set; get; }

        public event ConnectorEvent ValueChanged;

        protected void OnValueChanged()
        {
            ValueChanged?.Invoke();
        }
    }
}