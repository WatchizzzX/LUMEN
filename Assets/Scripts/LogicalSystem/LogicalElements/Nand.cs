using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// Nand implementation of LogicalComponent
    /// </summary>
    public class Nand : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return !inputs.All(item => item);
        }
    }
}