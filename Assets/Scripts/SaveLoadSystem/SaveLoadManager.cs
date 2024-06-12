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
        
        public void SaveLevelProgress(int sceneID, int starsCount, string time)
        {
            var writer = QuickSaveWriter.Create("LevelProgress", new QuickSaveSettings()
            {
                SecurityMode = SecurityMode.Aes,
                Password = SaveLoadConfig.Password,
                CompressionMode = CompressionMode.Gzip
            });
            
            writer.Write($"{sceneID}_stars", starsCount);
            writer.Write($"{sceneID}_time", time);
            writer.Commit();
        }

        public void LoadLevelProgress(int sceneID, out int starsCount, out string time)
        {
            if (!QuickSaveBase.RootExists("LevelProgress"))
            {
                starsCount = -1;
                time = "";
                return;
            }
            var reader = QuickSaveReader.Create("LevelProgress", new QuickSaveSettings
            {
                SecurityMode = SecurityMode.Aes,
                Password = SaveLoadConfig.Password,
                CompressionMode = CompressionMode.Gzip
            });

            if(!reader.TryRead($"{sceneID}_stars", out starsCount))
            {
                starsCount = -1;
                time = "";
                return;
            }
            time = reader.Read<string>($"{sceneID}_time");
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