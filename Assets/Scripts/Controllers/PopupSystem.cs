using MiniIT.ARKANOID.Settings;
using MiniIT.ARKANOID.UIElements;
using UnityEngine;

namespace MiniIT.ARKANOID.Controllers
{
    public class PopupSystem : MonoBehaviour
    {
        [SerializeField] private GameObject         background;
        [SerializeField] private Transform          popupParent;

        private BasePopup                           currentPopup;

        public void ShowPopup<T>(T settings) where T : BasePopupSettings
        {
            if(currentPopup == null)
            {
                Popup<T> popupPrefab = SettingsProvider.Get<PrefabSettings>().GetPopup<Popup<T>>();
                Popup<T> instance = Instantiate(popupPrefab, popupParent, false);
                instance.Setup(settings);
                currentPopup = instance;
                background.SetActive(true);
            }
        }

        public void HidePopup()
        {
            currentPopup.Hide();
            currentPopup = null;
            background.SetActive(false);
        }
    }
}
