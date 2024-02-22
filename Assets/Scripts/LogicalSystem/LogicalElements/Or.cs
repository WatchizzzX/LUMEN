using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// Or implementation of LogicalComponent
    /// </summary>
    public class Or : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.Any(item => item);
        }
    }
}