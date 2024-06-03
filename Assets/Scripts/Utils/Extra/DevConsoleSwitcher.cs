using DavidFDev.DevConsole;
using UnityEngine;

namespace Utils.Extra
{
    public class DevConsoleSwitcher : MonoBehaviour
    {
        public void SwitchConsole(bool value)
        {
            if (value)
                DevConsole.EnableConsole();
            else

                DevConsole.DisableConsole();
        }
    }
}