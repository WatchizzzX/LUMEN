using System.Collections.Generic;
using System.Reflection;

namespace EventBusSystem
{
    internal class EventMethodDictionary : Dictionary<SignalEnum, List<MethodInfo>>
    {
        public EventMethodDictionary(SignalEnum eventName)
        {
            Add(eventName, new List<MethodInfo>());
        }

        public void Add(SignalEnum eventName, MethodInfo method)
        {
            if (ContainsKey(eventName) == false)
                Add(eventName, new List<MethodInfo>());

            this[eventName].Add(method);
        }

        public bool Contains(SignalEnum eventName, MethodInfo method)
        {
            return this.ContainsKey(eventName) && this[eventName].Contains(method);
        }
    }
}
