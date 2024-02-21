using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    public class Nand : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return !inputs.All(item => item);
        }
    }
}