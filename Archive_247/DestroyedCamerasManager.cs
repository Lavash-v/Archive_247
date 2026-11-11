using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Archive_247
{
    public static class DestroyedCamerasManager
    {
        private static string _saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "destroyed_cams.json");

        public static List<string> DestroyedCameras { get; private set; } = new List<string>();

        static DestroyedCamerasManager()
        {
            Load();
        }

        public static bool IsCameraDestroyed(string camName)
        {
            return DestroyedCameras.Contains(camName);
        }

        public static void DestroyCamera(string camName)
        {
            if (!IsCameraDestroyed(camName))
            {
                DestroyedCameras.Add(camName);
                Save();
            }
        }
        public static void RestoreCamera(string camName)
        {
            if (IsCameraDestroyed(camName))
            {
                DestroyedCameras.Remove(camName);
                Save();
            }
        }

        private static void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(DestroyedCameras);
                File.WriteAllText(_saveFilePath, json);
            }
            catch {  }
        }

        private static void Load()
        {
            try
            {
                if (File.Exists(_saveFilePath))
                {
                    string json = File.ReadAllText(_saveFilePath);
                    var loadedCams = JsonSerializer.Deserialize<List<string>>(json);
                    if (loadedCams != null) DestroyedCameras = loadedCams;
                }
            }
            catch {  }
        }
    }
}