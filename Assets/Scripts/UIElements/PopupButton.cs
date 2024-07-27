using System;
using TMPro;
using UnityEngine;

namespace MiniIT.ARKANOID.UIElements
{
    public class PopupButton : MonoBehaviour, IPopupButton<PopupButtonSettings>
    {
        [SerializeField] private TMP_Text            title;
        private Action                               action;
        
        #region IPopupButton
        public void Setup(PopupButtonSettings settings)
        {
            title.text = settings.Text;
            action = settings.ButtonAction;
        }
        
        public void ButtonClick()
        {
            action?.Invoke();
        }
        #endregion
    }
    
    public class PopupButtonSettings : BasePopupButtonSetting
    {
        public string       Text;
        public Action       ButtonAction;
    }
}