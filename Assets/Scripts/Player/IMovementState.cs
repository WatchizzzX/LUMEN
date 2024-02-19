namespace Player
{
    public interface IMovementController
    {
        public MovementState GetMovementState();
    }

    public struct MovementState
    {
        public bool IsFalling { get; private set; }
        
        public float RelativeWalkSpeed { get; private set; }

        public MovementState(bool isFalling, float relativeWalkSpeed)
        {
            IsFalling = isFalling;
            RelativeWalkSpeed = relativeWalkSpeed;
        }
    }
}