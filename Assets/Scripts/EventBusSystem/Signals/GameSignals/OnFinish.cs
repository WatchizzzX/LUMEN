using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnFinish : ISignal
    {
        public readonly string ElapsedTime;
        public readonly int StarsCount;

        public OnFinish(string elapsedTime, int starsCount)
        {
            ElapsedTime = elapsedTime;
            StarsCount = starsCount;
        }
    }
}