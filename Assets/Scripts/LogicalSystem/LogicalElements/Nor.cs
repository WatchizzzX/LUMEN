namespace LogicalSystem.LogicalElements
{
    public class Nor : Or
    {
        public new bool Calculate(bool[] inputs)
        {
            return !base.Calculate(inputs);
        }
    }
}