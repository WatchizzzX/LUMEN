using EventBusSystem;
using EventBusSystem.Signals.GameSignals;

namespace UI
{
    public class UISettingsSetter :  EventBehaviour
    {
        public void SetSettings(int settings)
        {
            RaiseEvent(new SetSettingsLevel(settings));
        }
    }
}