using Enums;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnGameStateChangedSignal : ISignal
    {
        public readonly GameState GameState;

        public OnGameStateChangedSignal(GameState gameState)
        {
            GameState = gameState;
        }
    }
}