using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.ARKANOID
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text            title;
        [SerializeField] private Button              button;
        private Action                               action;
        
        public void Setup(string text, Action action, bool interactable = true)
        {
            title.text = text;
            button.interactable = interactable;
            this.action = action;
        }
        
        public void ButtonClick()
        {
            action?.Invoke();
        }
    }
}