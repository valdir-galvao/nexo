using System.IO;
using UnityEngine;
using Nexo.Progression;

namespace Nexo.Persistence
{
    public interface ISaveStore
    {
        SaveData Load();
        bool Save(SaveData data);
    }

    public sealed class JsonFileSaveStore : ISaveStore
    {
        private readonly string path;
        private readonly string backupPath;

        public JsonFileSaveStore(string fileName = "nexo-save.json")
        {
            path = Path.Combine(Application.persistentDataPath, fileName);
            backupPath = path + ".bak";
        }

        public SaveData Load()
        {
            SaveData data = Read(path);
            if (SaveValidation.IsValid(data)) return data;
            data = Read(backupPath);
            return SaveValidation.IsValid(data) ? data : new SaveData();
        }

        public bool Save(SaveData data)
        {
            if (!SaveValidation.IsValid(data)) return false;
            string temporary = path + ".tmp";
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(temporary, JsonUtility.ToJson(data, true));
                if (File.Exists(path)) File.Copy(path, backupPath, true);
                File.Copy(temporary, path, true);
                File.Delete(temporary);
                return true;
            }
            catch (IOException) { return false; }
        }

        private static SaveData Read(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return null;
                return JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));
            }
            catch (System.Exception) { return null; }
        }
    }
}
