using EventBusSystem.Interfaces;

namespace EventBusSystem
{
    public class EventModel
    {
        public SignalEnum EventName;
        public object Sender;
        public float Delay = 0;
        public bool IsHandled = false;
<<<<<<< Updated upstream
        public ISignal Payload;

        public EventModel(SignalEnum eventName, ISignal payload = null, object sender = null, float delay = 0)
=======
        public Signal Payload;

        public EventModel(SignalEnum eventName, Signal payload = null, object sender = null, float delay = 0)
>>>>>>> Stashed changes
        {
            EventName = eventName;
            Sender = sender;
            Delay = delay;
            Payload = payload;
            IsHandled = false;
        }

        public override string ToString()
        {
            return EventName.ToString();
        }
    }
}
