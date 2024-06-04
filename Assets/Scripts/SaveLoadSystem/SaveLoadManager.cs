using CI.QuickSave;
using DavidFDev.DevConsole;
using ServiceLocatorSystem;
using UnityEngine;
using System.Linq;

namespace SaveLoadSystem
{
    public class SaveLoadManager : MonoBehaviour, IService
    {
        private void Awake()
        {
            DevConsole.AddCommand(Command.Create(
                name: "resetprogress",
                aliases: "",
                helpText: "Reset level progress",
                callback: ResetProgress));
        }
        
        public void SaveLevelProgress(int sceneID, int starsCount)
        {
            var writer = QuickSaveWriter.Create("LevelProgress", new QuickSaveSettings()
            {
                SecurityMode = SecurityMode.Aes,
                Password = SaveLoadConfig.Password,
                CompressionMode = CompressionMode.Gzip
            });
            
            writer.Write($"{sceneID}", starsCount);
            writer.Commit();
        }

        public int LoadLevelProgress(int sceneID)
        {
            if (!QuickSaveBase.RootExists("LevelProgress")) return -1;
            var reader = QuickSaveReader.Create("LevelProgress", new QuickSaveSettings
            {
                SecurityMode = SecurityMode.Aes,
                Password = SaveLoadConfig.Password,
                CompressionMode = CompressionMode.Gzip
            });

            if (!reader.TryRead<int>($"{sceneID}", out var starsCount))
            {
                return -1;
            }
            return starsCount;
        }

        private void ResetProgress()
        {
            var writer = QuickSaveWriter.Create("LevelProgress", new QuickSaveSettings()
            {
                SecurityMode = SecurityMode.Aes,
                Password = SaveLoadConfig.Password,
                CompressionMode = CompressionMode.Gzip
            });
            
            writer.GetAllKeys().ToList().ForEach(key => writer.Delete(key));
            writer.Commit();
        }
    }
}