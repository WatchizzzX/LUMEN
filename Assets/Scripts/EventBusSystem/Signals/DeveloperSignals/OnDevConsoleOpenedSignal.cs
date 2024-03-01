using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.DeveloperSignals
{
    public class OnDevConsoleOpenedSignal : ISignal
    {
        public readonly bool IsOpened;

        public OnDevConsoleOpenedSignal(bool isOpened)
        {
            IsOpened = isOpened;
        }
    }
}