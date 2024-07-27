using MiniIT.ARKANOID.Save;
using MiniIT.ARKANOID.Settings;
using MiniIT.ARKANOID.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniIT.ARKANOID.Controllers
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text               scoreTMP;
    
        private int                                     currentScore = 0;
        private PopupSystem                             popupSystem;
        private ISaveManager                            saveManager;

        [Inject]
        public void Construct(PopupSystem popupSystem, ISaveManager saveManager)
        {
            this.popupSystem = popupSystem;
            this.saveManager = saveManager;
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

        public async void ShowLosePopup()
        {
            await saveManager.SaveMaxScore(currentScore);
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

        public async void ShowWinPopup()
        {
            await saveManager.SaveMaxScore(currentScore);
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