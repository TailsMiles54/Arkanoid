using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID.Save
{
    public class SaveManager : IInitializable, ISaveManager
    {
#region IInitializable
        public void Initialize()
        {
            Debug.Log("<color=#2AFF00> SaveManager initialized! </color>");
        }
#endregion

#region ISaveManager
        public async Task CheckSave()
        {
            Dictionary<string, Item> playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
                "sound_active"
            });

            if (playerData.TryGetValue("sound_active", out var firstKey)) {
                Debug.Log($"sound_active value: {firstKey.Value.GetAs<bool>()}");
                return;
            }
        
            await SaveDefaultData();
        }

        private async Task SaveDefaultData()
        {
            Dictionary<string, object> playerData = new Dictionary<string, object>{
                {"sound_active", true},
                {"max_score", 0}
            };
        
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
            Debug.Log($"Saved data {string.Join(',', playerData)}");
        }

        public async Task SaveMaxScore(int value)
        {
            Dictionary<string, object> playerData = new Dictionary<string, object>{
                {"max_score", value}
            };
        
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
            Debug.Log($"Saved data {string.Join(',', playerData)}");
        }

        public async Task SaveSoundState(bool value)
        {
            Dictionary<string, object> playerData = new Dictionary<string, object>{
                {"sound_active", value}
            };
        
            await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
            Debug.Log($"Saved data {string.Join(',', playerData)}");
        }

        public async Task<int> GetMaxScore()
        {
            Dictionary<string, Item> playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
                "max_score"
            });

            if (playerData.TryGetValue("max_score", out var firstKey)) 
            {
                Debug.Log($"max_score value: {firstKey.Value.GetAs<int>()}");
            }
        
            return playerData["max_score"].Value.GetAs<int>();
        }

        public async Task<bool> GetSoundState()
        {
            Dictionary<string, Item> playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
                "sound_active"
            });

            if (playerData.TryGetValue("sound_active", out var firstKey)) {
                Debug.Log($"sound_active value: {firstKey.Value.GetAs<bool>()}");
            }
        
            return playerData["sound_active"].Value.GetAs<bool>();
        }
#endregion
    }
}