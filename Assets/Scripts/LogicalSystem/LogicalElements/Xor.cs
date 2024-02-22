using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// Xor implementation of LogicalComponent
    /// </summary>
    public class Xor : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.Count(item => item) % 2 != 0;
        }
    }
}