namespace LogicalSystem.Interfaces
{
    /// <summary>
    /// An interface for elements that can output a value themselves. Work with LogicalSystem
    /// </summary>
    public interface ISourceComponent
    {
        /// <summary>
        /// SourceEvent delegate
        /// </summary>
        public delegate void SourceEvent(bool state);

        /// <summary>
        /// Event when internal value changed
        /// </summary>
        public event SourceEvent OnValueChanged;

        /// <summary>
        /// Return internal value
        /// </summary>
        /// <returns>Internal value</returns>
        public bool GetValue();

        /// <summary>
        /// Set internal value
        /// </summary>
        /// <param name="value">Required value</param>
        public void SetValue(bool value);
    }
}