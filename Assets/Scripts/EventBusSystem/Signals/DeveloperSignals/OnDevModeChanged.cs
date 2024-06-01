using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.DeveloperSignals
{
    public class OnDevModeChanged : ISignal
    {
        public readonly bool InDeveloperMode;

        public OnDevModeChanged(bool inDeveloperMode)
        {
            InDeveloperMode = inDeveloperMode;
        }
    }
}