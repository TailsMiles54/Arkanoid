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
            if (PlayerPrefs.HasKey("max_score"))
            {
                string data = PlayerPrefs.GetString("max_score");
                return Task.FromResult(Convert.ToInt32(data));
            }
        
            AddDataToSave("max_score", 0);

            return Task.FromResult(0);
        }

        public Task<bool> GetSoundState()
        {
            if (PlayerPrefs.HasKey("sound_active"))
            {
                string data = PlayerPrefs.GetString("sound_active");
                return Task.FromResult(Convert.ToBoolean(data));
            }
        
            AddDataToSave("sound_active", true);

            return Task.FromResult(true);
        }
#endregion

        private void AddDataToSave(string key, object value)
        {
            string jsonString = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, jsonString);
        }
    }
}