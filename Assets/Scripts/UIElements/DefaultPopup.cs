using MiniIT.ARKANOID.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.ARKANOID.UIElements
{
    public class DefaultPopup : Popup<DefaultPopupSettings>
    {
        [SerializeField] private TMP_Text           titleTMP;
        [SerializeField] private TMP_Text           contentTMP;
        [SerializeField] private GameObject         imageParent;
        [SerializeField] private Image              iconImage;
        [SerializeField] private Transform          buttonParent;

        public override void Setup(DefaultPopupSettings settings)
        {
            titleTMP.text = settings.Title;
            contentTMP.text = settings.Content;
            iconImage.gameObject.SetActive(settings.Icon!= null);
            iconImage.sprite = settings.Icon;

            foreach (var buttonSetting in settings.Buttons)
            {
                var button = Instantiate(SettingsProvider.Get<PrefabSettings>().PopupButton, buttonParent);
                button.Setup(buttonSetting);
            }
        }
    }

    public class DefaultPopupSettings : BasePopupSettings
    {
        public string Title;
        public string Content;
        public Sprite Icon = null;
        public PopupButtonSettings[] Buttons;
    }
}