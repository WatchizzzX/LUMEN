using System.Linq;
using LogicalSystem.Interfaces;

namespace LogicalSystem.LogicalElements
{
    /// <summary>
    /// Not implementation of LogicalComponent
    /// </summary>
    public class Not : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.All(item => !item);
        }
    }
}