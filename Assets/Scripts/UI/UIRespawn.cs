using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

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
            
            eventBus.RaiseEvent(new OnRespawnPlayer());
        }
    }
}