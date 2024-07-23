using System;
using TMPro;
using UnityEngine;

namespace MiniIT.ARKANOID
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text            title;
        private Action                               action;
        
        public void Setup(string text, Action action)
        {
            title.text = text;
            this.action = action;
        }
        
        public void ButtonClick()
        {
            action?.Invoke();
        }
    }
}