using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using UnityEngine;

namespace UI
{
    public class UIPausePanel : EventBehaviour
    {
        [SerializeField] private GameObject uiPausePanel;

        [ListenTo(SignalEnum.OnGameStateChanged)]
        private void OnGameStateChanged(EventModel eventModel)
        {
            switch (((OnGameStateChanged)eventModel.Payload).GameState)
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