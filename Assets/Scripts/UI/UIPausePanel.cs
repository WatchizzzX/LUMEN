using System;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace UI
{
    public class UIPausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject uiPausePanel;
        
        private void Awake()
        {
            if (!ServiceLocator.TryGet(out EventBus eventBus))
            {
                Logger.Log(LoggerChannel.UI, Priority.Error, "Can't find EventBus. Work of pause panel is impossible");
                return; 
            }
            
            eventBus.Subscribe<OnGameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnDestroy()
        {
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