namespace LogicalSystem.LogicalElements
{
    public class Xnor : Xor
    {
        public new bool Calculate(bool[] inputs)
        {
            return !base.Calculate(inputs);
        }
    }
}