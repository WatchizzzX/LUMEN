namespace LogicalSystem.Interfaces
{
    /// <summary>
    /// Interface for logical elements, that implements own logic. Work with LogicalSystem
    /// </summary>
    public interface ILogicalComponent
    {
        /// <summary>
        /// Calculate value based on inputs
        /// </summary>
        /// <param name="inputs">Array of inputs</param>
        /// <returns>Calculated value</returns>
        public bool Calculate(bool[] inputs);
    }
}