using System;

namespace EventBusSystem
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ListenToAttribute : Attribute
    {
        public SignalEnum Event { get; private set; }
        public int Priority { get; private set; }

        public ListenToAttribute(SignalEnum eventName)
        {
            Event = eventName;
            Priority = 0;
        }

        public ListenToAttribute(SignalEnum eventName, int priority)
        {
            Event = eventName;
            Priority = priority;
        }
    }
}