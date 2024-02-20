using System.Linq;
using LogicalSystem.Interfaces;
using UnityEngine;

namespace LogicalSystem.LogicalElements
{
    public class And : ILogicalComponent
    {
        public bool Calculate(bool[] inputs)
        {
            return inputs.All(item => item);
        }
    }
}