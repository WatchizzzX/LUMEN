using EventBusSystem;
using EventBusSystem.Signals.GameSignals;

namespace UI
{
    public class UIRespawn : EventBehaviour
    {
        public void RespawnPlayer()
        {
<<<<<<< Updated upstream
            if (!ServiceLocator.TryGet(out EventBus eventBus))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error, "Can't find EventBus. Loading level is impossible");
                return;
            }
            
<<<<<<< Updated upstream
            eventBus.Invoke(new OnRespawnPlayerSignal());
=======
            RaiseEvent(new OnRespawnPlayer());
>>>>>>> Stashed changes
=======
            eventBus.RaiseEvent(new OnRespawnPlayerSignal());
>>>>>>> Stashed changes
        }
    }
}