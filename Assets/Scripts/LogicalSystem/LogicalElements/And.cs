using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// And implementation of LogicalComponent
    /// </summary>
    public class And : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.All(item => item);
        }
    }
}