using MiniIT.ARKANOID.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniIT.ARKANOID.Controllers
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text            scoreTMP;

        private int                                  currentScore = 0;
        private PopupSystem                          popupSystem;

        [Inject]
        public void Construct(PopupSystem popupSystem)
        {
            this.popupSystem = popupSystem;
        }
        
        public void AddScore(int value)
        {
            currentScore += value;
            scoreTMP.text = $"Score: {currentScore}";
        }

        public void Exit()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void ShowLosePopup()
        {
            popupSystem.ShowPopup(new DefaultPopupSettings()
            {
                Title = "Game Over",
                Content = "You lost!",
                Buttons = new []
                {
                 new PopupButtonSettings()
                 {
                     Text = "Exit to menu",
                     ButtonAction = () => SceneManager.LoadScene("MenuScene")
                 }   
                }
            });
        }

        public void ShowWinPopup()
        {
            popupSystem.ShowPopup(new DefaultPopupSettings()
            {
                Title = "Congratulations!",
                Content = "You won!",
                Icon = SettingsProvider.Get<GameFieldSettings>().WinSprite,
                Buttons = new []
                {
                 new PopupButtonSettings()
                 {
                     Text = "Exit to menu",
                     ButtonAction = () => SceneManager.LoadScene("MenuScene")
                 }   
                }
            });
        }
    }
}