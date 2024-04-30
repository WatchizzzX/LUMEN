using Enums;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnGameStateChanged : ISignal
    {
        public readonly GameState GameState;

        public OnGameStateChanged(GameState gameState)
        {
            GameState = gameState;
        }
    }
}