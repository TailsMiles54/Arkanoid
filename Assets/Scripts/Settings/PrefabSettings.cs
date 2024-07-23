using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniIT.ARKANOID.Settings
{
    [CreateAssetMenu(menuName = "Arkanoid/PrefabSettings", fileName = "PrefabSettings", order = 0)]
    public class PrefabSettings : ScriptableObject
    {
        [SerializeField] private List<MonoBehaviour>              prefabs;
        [SerializeField] private List<MonoBehaviour>              popups;

        public T Get<T>() where T : MonoBehaviour
        {
            T result = null;
            
            prefabs.First(x => x.TryGetComponent(out result));

            return result;
        }

        public T GetPopup<T>() where T : MonoBehaviour
        {
            T result = null;
            
            popups.First(x => x.TryGetComponent(out result));

            return result;
        }
    }
}