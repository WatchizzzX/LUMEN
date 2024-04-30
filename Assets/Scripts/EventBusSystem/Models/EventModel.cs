using EventBusSystem.Interfaces;

namespace EventBusSystem
{
    public class EventModel
    {
        public SignalEnum EventName;
        public object Sender;
        public float Delay = 0;
        public bool IsHandled = false;
        public ISignal Payload;

        public EventModel(SignalEnum eventName, ISignal payload = null, object sender = null, float delay = 0)
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
