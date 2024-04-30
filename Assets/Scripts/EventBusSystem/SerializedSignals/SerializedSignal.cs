using System;
<<<<<<< Updated upstream
=======
using System.Reflection;
>>>>>>> Stashed changes
using EventBusSystem.Interfaces;

namespace EventBusSystem.SerializedSignals
{
    [Serializable]
    public abstract class SerializedSignal
    {
<<<<<<< Updated upstream
        public ISignal ConvertToSignal()
=======
        public Signal ConvertToSignal()
>>>>>>> Stashed changes
        {
            var targetType = SignalDictionary.SerializedTypeToType[this.GetType()];

            var fields = GetType().GetFields();
            
            var paramTypes = new Type[fields.Length];
            var paramValues = new object[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                paramTypes[i] = fields[i].FieldType;
                paramValues[i] = fields[i].GetValue(this);
            }

            var constructor = targetType.GetConstructor(paramTypes);
            
            if (constructor == null)
            {
                throw new InvalidOperationException("Конструктор не найден.");
            }

            // Создаем экземпляр объекта
<<<<<<< Updated upstream
            return (ISignal)constructor.Invoke(paramValues);
=======
            return (Signal)constructor.Invoke(paramValues);
>>>>>>> Stashed changes
        }
    }
}