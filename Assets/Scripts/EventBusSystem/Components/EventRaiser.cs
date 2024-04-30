using UnityEngine;

namespace EventBusSystem
{
    public abstract class EventRaiser : EventBehaviour
    {
        [SerializeField] protected float delay;
        [SerializeField] protected bool raiseOnEnable;
        [SerializeField] protected bool raiseOnStart;

        private void OnEnable()
        {
            if (raiseOnEnable)
                Raise();
        }

        private void Start()
        {
            if (raiseOnStart)
                Raise();
        }

        public abstract void Raise();
    }
}