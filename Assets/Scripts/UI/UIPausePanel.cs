using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using UnityEngine;

namespace UI
{
    public class UIPausePanel : EventBehaviour
    {
        [SerializeField] private GameObject uiPausePanel;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        
        private void Awake()
        {
            if (!ServiceLocator.TryGet(out EventBus eventBus))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error, "Can't find EventBus. Work of pause panel is impossible");
                return; 
            }
            
            eventBus.Subscribe<OnGameStateChangedSignal>(OnGameStateChanged);
        }
=======
>>>>>>> Stashed changes

        [ListenTo(SignalEnum.OnGameStateChangedSignal)]
        public void OnGameStateChanged(EventModel eventModel)
        {
<<<<<<< Updated upstream
            if (!ServiceLocator.TryGet(out EventBus eventBus))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error, "Can't find EventBus. Pause panel can't unsubscribe");
                return; 
            }
            
            eventBus.Unsubscribe<OnGameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(OnGameStateChangedSignal signal)
        {
            switch (signal.GameState)
=======

        [ListenTo(SignalEnum.OnGameStateChanged)]
        private void OnGameStateChanged(EventModel eventModel)
        {
            switch (((OnGameStateChanged)eventModel.Payload).GameState)
>>>>>>> Stashed changes
=======
            switch (((OnGameStateChangedSignal)eventModel.Payload).GameState)
>>>>>>> Stashed changes
            {
                case GameState.Normal:
                    uiPausePanel.SetActive(false);
                    break;
                case GameState.Paused:
                    uiPausePanel.SetActive(true);
                    break;
            }
            
        }
    }
}