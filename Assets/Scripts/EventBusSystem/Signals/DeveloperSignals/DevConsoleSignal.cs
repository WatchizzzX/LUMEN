namespace EventBusSystem.Signals.DeveloperSignals
{
    public class DevConsoleSignal
    {
        public readonly bool IsOpened;

        public DevConsoleSignal(bool isOpened)
        {
            IsOpened = isOpened;
        }
    }
}