using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    public class Xnor : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.Count(item => item) % 2 == 0;
        }
    }
}