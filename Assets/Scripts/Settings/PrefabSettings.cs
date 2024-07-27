using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/PrefabSettings", fileName = "PrefabSettings", order = 0)]
    public class PrefabSettings : ScriptableObject
    {
        [SerializeField] private List<MonoBehaviour>        prefabs;

        [Space (25), Header("UI")]
        [SerializeField] private List<BasePopup>            popups;
        [SerializeField] private PopupButton                popupButton;

        public PopupButton                                  PopupButton => popupButton;

        
        public T Get<T>() where T : MonoBehaviour
        {
            try
            {
                T result = null;
            
                prefabs.First(x => x.TryGetComponent(out result));

                return result;
            }
            catch (Exception e)
            {
                Debug.LogWarning(typeof(T));
                throw;
            }
        }

        public T GetPopup<T>() where T : BasePopup
        {
            try
            {
                return (T)popups.First(x => x is T);
            }
            catch (Exception e)
            {
                Debug.LogWarning(typeof(T));
                throw;
            }
        }
    }
}