using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.DeveloperSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/DeveloperSignals/OnDevConsoleOpenedSignal.cs
    public class OnDevConsoleOpenedSignal : ISignal
=======
    public class OnDevConsoleOpened : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/DeveloperSignals/OnDevConsoleOpened.cs
    {
        public readonly bool IsOpened;

        public OnDevConsoleOpened(bool isOpened)
        {
            IsOpened = isOpened;
        }
    }
}