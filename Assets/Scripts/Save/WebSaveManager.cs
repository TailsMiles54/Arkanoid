using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID.Save
{
    public class WebSaveManager : IInitializable, ISaveManager
    {
#region IInitializable
        public void Initialize()
        {
            Debug.Log("<color=#2AFF00> WebSaveManager initialized! </color>");
        }
#endregion

#region ISaveManager
        public Task CheckSave()
        {
            CheckOrCreateSaveDataFile();
            return Task.CompletedTask;
        }

        public Task SaveMaxScore(int value)
        {
            AddDataToSave("max_score", value);
            return Task.CompletedTask;
        }

        public Task SaveSoundState(bool value)
        {
            AddDataToSave("sound_active", value);
            return Task.CompletedTask;
        }

        public Task<int> GetMaxScore()
        {
            Dictionary<string, object> userData = LoadFromJsonFile();
        
            if (userData.TryGetValue("max_score", out var value))
            {
                return Task.FromResult(Convert.ToInt32(value));
            }
        
            AddDataToSave("max_score", 0);

            return Task.FromResult(0);
        }

        public Task<bool> GetSoundState()
        {
            Dictionary<string, object> userData = LoadFromJsonFile();
        
            if (userData.TryGetValue("sound_active", out var value))
            {
                return Task.FromResult(Convert.ToBoolean(value));
            }
        
            AddDataToSave("sound_active", true);

            return Task.FromResult(true);
        }
#endregion

        private void AddDataToSave(string key, object value)
        {
            Dictionary<string, object> currentData = LoadFromJsonFile();

            if (!currentData.TryAdd(key, value))
            {
                currentData[key] = value;
            }
        
            SaveToJsonFile(currentData);
        }

        private void SaveToJsonFile(Dictionary<string, object> saveData)
        {
            string jsonString = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(Application.persistentDataPath + "/save_data.json", jsonString);
        }

        private Dictionary<string, object> LoadFromJsonFile()
        {
            string jsonString = File.ReadAllText(Application.persistentDataPath + "/save_data.json");
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
        }

        private void CheckOrCreateSaveDataFile()
        {
            string filePath = Application.persistentDataPath + "/save_data.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "{}");
            }
        }
    }
}