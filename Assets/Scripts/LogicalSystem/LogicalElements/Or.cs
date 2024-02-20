using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    public class Or : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.Any(item => item);
        }
    }
}