using EventBusSystem.Interfaces;

namespace EventBusSystem.Signals.UISignals
{
    public class ShowUIHelp : ISignal
    {
        public readonly string Name;
        public readonly string Info;
        
        public ShowUIHelp(string name, string info)
        {
            Name = name;
            Info = info;
        }
    }
}