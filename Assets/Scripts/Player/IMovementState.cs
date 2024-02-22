namespace Player
{
    /// <summary>
    /// The interface loosely connects the animator and the player controller
    /// </summary>
    public interface IMovementController
    {
        /// <summary>
        /// Get MovementState from controller
        /// </summary>
        /// <returns>Current MovementState</returns>
        public MovementState GetMovementState();
    }

    /// <summary>
    /// Movement state struct
    /// </summary>
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