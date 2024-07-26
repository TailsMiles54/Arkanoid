using System.Collections;
using System.Collections.Generic;
using MiniIT.ARKANOID.Settings;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class PopupSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private Transform _popupParent;

        private BasePopup _currentPopup;

        public void ShowPopup<T>(T settings) where T : BasePopupSettings
        {
            if(_currentPopup == null)
            {
                var popupPrefab = SettingsProvider.Get<PrefabSettings>().GetPopup<Popup<T>>();
                var instance = Instantiate(popupPrefab, _popupParent, false);
                instance.Setup(settings);
                _currentPopup = instance;
                _background.SetActive(true);
            }
        }

        public void HidePopup()
        {
            _currentPopup.Hide();
            _currentPopup = null;
            _background.SetActive(false);
        }
    }
}
