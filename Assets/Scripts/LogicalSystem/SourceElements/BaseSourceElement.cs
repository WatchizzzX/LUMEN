using LogicalSystem.Interfaces;

namespace LogicalSystem.SourceElements
{
    public abstract class BaseSourceElement : ISourceComponent
    {
        public event ISourceComponent.SourceEvent OnValueChanged;

        private bool _value;
        
        public bool GetValue()
        {
            return _value;
        }

        public void SetValue(bool value)
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}