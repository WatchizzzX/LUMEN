namespace LogicalSystem.LogicalElements
{
    public class Nand : And
    {
        public new bool Calculate(bool[] inputs)
        {
            return !base.Calculate(inputs);
        }
    }
}