using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class SetSettingsLevel : ISignal
    {
        public readonly int SettingsLevel;
        
        public SetSettingsLevel(int settingsLevel)
        {
            SettingsLevel = settingsLevel;
        }
    }
}