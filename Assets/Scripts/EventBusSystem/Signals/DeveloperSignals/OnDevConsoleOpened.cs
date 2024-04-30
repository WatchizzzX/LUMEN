using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.DeveloperSignals
{
    public class OnDevConsoleOpened : ISignal
    {
        public readonly bool IsOpened;

        public OnDevConsoleOpened(bool isOpened)
        {
            IsOpened = isOpened;
        }
    }
}