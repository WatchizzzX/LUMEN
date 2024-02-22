using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// Xnor implementation of LogicalComponent
    /// </summary>
    public class Xnor : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.Count(item => item) % 2 == 0;
        }
    }
}