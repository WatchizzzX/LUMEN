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
        
        public bool IsJumping { get; private set; }
        
        public bool IsSprinting { get; private set; }

        public MovementState(bool isFalling, float relativeWalkSpeed, bool isJumping, bool isSprinting)
        {
            IsFalling = isFalling;
            RelativeWalkSpeed = relativeWalkSpeed;
            IsJumping = isJumping;
            IsSprinting = isSprinting;
        }
    }
}