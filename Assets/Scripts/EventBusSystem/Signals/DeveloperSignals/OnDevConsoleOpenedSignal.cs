using System;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.DeveloperSignals
{
    [Serializable]
    public class OnDevConsoleOpenedSignal : Signal
    {
        public readonly bool IsOpened;

        public OnDevConsoleOpenedSignal(bool isOpened)
        {
            IsOpened = isOpened;
        }
    }
}