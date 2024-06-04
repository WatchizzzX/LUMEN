using UnityEngine;

namespace Utils.Extra
{
    public class Stopwatch : MonoBehaviour
    {
        public float ElapsedTime { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsPaused { get; private set; }

        // Запуск секундомера
        public void Start()
        {
            IsRunning = true;
            IsPaused = false;
        }

        // Остановка секундомера
        public void Stop()
        {
            IsRunning = false;
            IsPaused = false;
            ElapsedTime = 0f;
        }

        // Пауза секундомера
        public void Pause()
        {
            if (!IsRunning) return;
            IsPaused = true;
            IsRunning = false;
        }

        // Снятие с паузы секундомера
        public void Resume()
        {
            if (!IsPaused) return;
            IsRunning = true;
            IsPaused = false;
        }

        // Форматированный вывод времени
        public string GetFormattedTime()
        {
            var minutes = Mathf.FloorToInt(ElapsedTime / 60F);
            var seconds = Mathf.FloorToInt(ElapsedTime % 60F);
            var milliseconds = Mathf.FloorToInt((ElapsedTime * 1000F) % 1000F);
            return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }
        
        private void Update()
        {
            if (IsRunning)
            {
                ElapsedTime += Time.deltaTime;
            }
        }
    }
}