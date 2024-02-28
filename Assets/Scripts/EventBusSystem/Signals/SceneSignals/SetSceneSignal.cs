namespace EventBusSystem.Signals.SceneSignals
{
    public class SetSceneSignal
    {
        public readonly int NewSceneID;

        public SetSceneSignal(int newSceneID)
        {
            NewSceneID = newSceneID;
        }
    }
}