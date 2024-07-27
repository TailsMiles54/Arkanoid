using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.ARKANOID
{
    public class PopupButton : MonoBehaviour, IPopupButton<PopupButtonSettings>
    {
        [SerializeField] private TMP_Text            title;
        private Action                               action;
        
        public void Setup(PopupButtonSettings settings)
        {
            title.text = settings.Text;
            action = settings.ButtonAction;
        }
        
        public void ButtonClick()
        {
            action?.Invoke();
        }
    }
    
    public class PopupButtonSettings : BasePopupButtonSetting
    {
        public string       Text;
        public Action       ButtonAction;
    }
}