namespace LogicalSystem.Interfaces
{
    public interface ISourceComponent
    {
        public delegate void SourceEvent(bool state);

        public event SourceEvent OnValueChanged;
        
        public bool GetValue();

        public void SetValue(bool value);
    }
}