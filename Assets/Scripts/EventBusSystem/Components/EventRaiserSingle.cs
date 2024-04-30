using EventBusSystem.SerializedSignals;
using UnityEngine;

namespace EventBusSystem
{
    public class EventRaiserSingle : EventRaiser
    {
        public SignalEnum eventName;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        [SerializeReference] public SerializedSignal signal;

        public override void Raise()
        {
            RaiseEvent(signal.ConvertToSignal(), delay);
        }
    }
}