using UnityEngine;

namespace UI
{
    public class UIQuitter : MonoBehaviour
    {
        public void CallExit()
        {
            Application.Quit();
        }
    }
}