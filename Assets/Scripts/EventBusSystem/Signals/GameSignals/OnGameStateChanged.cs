using Enums;
using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
<<<<<<< Updated upstream:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnGameStateChangedSignal.cs
    public class OnGameStateChangedSignal : ISignal
=======
    public class OnGameStateChanged : ISignal
>>>>>>> Stashed changes:Assets/Scripts/EventBusSystem/Signals/GameSignals/OnGameStateChanged.cs
    {
        public readonly GameState GameState;

        public OnGameStateChanged(GameState gameState)
        {
            GameState = gameState;
        }
    }
}