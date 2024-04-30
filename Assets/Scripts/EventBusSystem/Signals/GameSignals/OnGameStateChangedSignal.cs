using System;
using Enums;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    [Serializable]
    public class OnGameStateChangedSignal : Signal
    {
        public readonly GameState GameState;

        public OnGameStateChangedSignal(GameState gameState)
        {
            GameState = gameState;
        }
    }
}