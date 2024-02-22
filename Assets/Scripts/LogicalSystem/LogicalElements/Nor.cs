using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// Nor implementation of LogicalComponent
    /// </summary>
    public class Nor : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return !inputs.Any(item => item);
        }
    }
}