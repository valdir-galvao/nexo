using System.IO;
using UnityEngine;
using Nexo.Progression;

namespace Nexo.Safety
{
    public sealed class LocalDataControls
    {
        private readonly string savePath;
        private readonly string backupPath;

        public LocalDataControls(string fileName = "nexo-save.json")
        {
            savePath = Path.Combine(Application.persistentDataPath, fileName);
            backupPath = savePath + ".bak";
        }

        public bool DeleteAll()
        {
            try
            {
                if (File.Exists(savePath)) File.Delete(savePath);
                if (File.Exists(backupPath)) File.Delete(backupPath);
                return !File.Exists(savePath) && !File.Exists(backupPath);
            }
            catch (IOException) { return false; }
        }

        public bool ExportTo(string destinationPath, SaveData data)
        {
            if (string.IsNullOrEmpty(destinationPath) || data == null) return false;
            try
            {
                string directory = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
                File.WriteAllText(destinationPath, JsonUtility.ToJson(data, true));
                return File.Exists(destinationPath);
            }
            catch (IOException) { return false; }
        }
    }
}
