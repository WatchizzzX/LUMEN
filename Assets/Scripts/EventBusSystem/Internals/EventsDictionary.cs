using System;
using System.Collections.Generic;
using System.Reflection;

namespace EventBusSystem
{
    internal class EventsDictionary : Dictionary<SignalEnum, SortedEventsList<ListenerModel>>
    {
        public EventsDictionary()
        {
            foreach (var item in Enum.GetValues(typeof(SignalEnum)))
                Add((SignalEnum)item, new SortedEventsList<ListenerModel>());
        }

        public void Add(SignalEnum eventName, object targetObject, MethodInfo method, int priority)
        {
            this[eventName].Add(priority, new ListenerModel()
            {
                TargetObject = targetObject,
                Method = method
            });
        }

        public IEnumerable<ListenerModel> GetAllValues(SignalEnum eventName)
        {
            return this[eventName].GetAllValues();
        }

        public void Remove(SignalEnum eventName, object obj)
        {
            this[eventName].Remove((o) =>
            {
                return ReferenceEquals(obj, o.TargetObject);
            });
        }

        public void Remove(SignalEnum eventName, object obj, MethodInfo listener)
        {
            this[eventName].Remove((o) =>
            {
                return ReferenceEquals(obj, o.TargetObject) && o.Method == listener;
            });
        }
    }
}
