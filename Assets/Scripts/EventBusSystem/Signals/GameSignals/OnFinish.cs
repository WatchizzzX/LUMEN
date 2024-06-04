using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.GameSignals
{
    public class OnFinish : ISignal
    {
        public readonly string ElapsedTime;
        public readonly int StarsCount;
        public readonly int NextSceneID;

        public OnFinish(string elapsedTime, int starsCount, int nextSceneID)
        {
            ElapsedTime = elapsedTime;
            StarsCount = starsCount;
            NextSceneID = nextSceneID;
        }
    }
}