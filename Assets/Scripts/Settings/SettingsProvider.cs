using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(menuName = "Arkanoid/SettingsProvider", fileName = "SettingsProvider", order = 0)]
    public class SettingsProvider : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject>         _settingsList;
        private static SettingsProvider                         _settingsProvider;

        /// <summary>
        /// Get settings for T
        /// </summary>
        /// <typeparam name="T"> ScriptableObject from settingsList in Resources </typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : ScriptableObject
        {
            if (_settingsProvider == null)
            {
                _settingsProvider = Resources.Load<SettingsProvider>("SettingsProvider");
            }
        
            return (T)_settingsProvider._settingsList.First(x => x is T);
        }
    }
}