using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace UI
{
    public class UIRespawn : MonoBehaviour
    {
        public void RespawnPlayer()
        {
            if (!ServiceLocator.TryGet(out EventBus eventBus))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error, "Can't find EventBus. Loading level is impossible");
                return;
            }
            
            eventBus.Invoke(new OnRespawnPlayerSignal());
        }
    }
}