using System;
using System.Collections.Generic;
using DG.Tweening;
using MiniIT.ARKANOID.Enums;
using MiniIT.ARKANOID.Save;
using MiniIT.ARKANOID.Settings;
using MiniIT.ARKANOID.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniIT.ARKANOID.Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Transform          buttonsParent;
        [SerializeField] private GameObject         uiBlocker;
        [SerializeField] private TMP_Text           maxScoreTMP;
            
        private ObjectPool<MenuButton>              buttonsPool;
        private Sequence                            menuTransitionSequence;
        private GameController                      gameController;
        private SoundController                     soundController;
        private ISaveManager                        saveManager;
        private List<MenuButton>                    menuButtons = null;

        [Inject]
        public void Construct(GameController gameController, SoundController soundController, ISaveManager saveManager)
        {
            this.gameController = gameController;
            this.soundController = soundController;
            this.saveManager = saveManager;
        }
        
        public async void Start()
        {
            await saveManager.CheckSave();
            
            soundController.PlayMusic(MusicType.Menu);
            menuButtons = new List<MenuButton>();
            buttonsPool = new ObjectPool<MenuButton>(
                () =>
                {
                    MenuButton menuButton = Instantiate(SettingsProvider.Get<PrefabSettings>().Get<MenuButton>(), buttonsParent);
                    menuButtons.Add(menuButton);
                    return menuButton;
                },
                button => {button.gameObject.SetActive(true);},
                button => {button.gameObject.SetActive(false);},
                button => {Destroy(button.gameObject);});
            
            ShowMainMenu();
            menuButtons.Reverse();
            
            maxScoreTMP.text = $"Max score: {await saveManager.GetMaxScore()}";
        }

        private void ShowMainMenu()
        {
            buttonsPool.Get().Setup("Play offline", () =>
            {
                ChangeMenu(() =>
                {
                    ShowGameTypeMenu(() => SceneManager.LoadScene("SoloGameScene"));
                });
            });
            buttonsPool.Get().Setup("Play online", () =>
            {
                ChangeMenu(() =>
                {
                    ShowGameTypeMenu(() => SceneManager.LoadScene("MultiplayerGameScene"));
                });
            }, false);
            buttonsPool.Get().Setup("Settings", () =>
            {
                ChangeMenu(ShowSettingsMenu);
            });
            buttonsPool.Get().Setup("Exit", Application.Quit);
        }

        private void ShowGameTypeMenu(Action action)
        {
            buttonsPool.Get().Setup("Full field", () =>
            {
                gameController.ChangeGameType(GameType.Full);
                action?.Invoke();
            });
            buttonsPool.Get().Setup("Random", () =>
            {
                gameController.ChangeGameType(GameType.Random);
                action?.Invoke();
            });
            buttonsPool.Get().Setup("Preset", () =>
            {
                gameController.ChangeGameType(GameType.Preset);
                action?.Invoke();
            }, false);
            buttonsPool.Get().Setup("Back", () =>
            {
                ChangeMenu(ShowMainMenu);
            });
        }

        private void ShowSettingsMenu()
        {
            buttonsPool.Get().Setup("Sound on\\off", () =>
            {
                soundController.SoundStateChange();
            });
            buttonsPool.Get().Setup("My Telegram", () =>
            {
                Application.OpenURL("https://t.me/tailsmiles322");
            });
            buttonsPool.Get().Setup("My github", () =>
            {
                Application.OpenURL("https://github.com/TailsMiles54");
            });
            buttonsPool.Get().Setup("Back", () =>
            {
                ChangeMenu(ShowMainMenu);
            });
        }

        private void ChangeMenu(Action action)
        {
            soundController.PlaySoundEffect(SoundType.ButtonClick);
            uiBlocker.SetActive(true);
            menuTransitionSequence = DOTween.Sequence();
            
            menuTransitionSequence.Append(buttonsParent.DOLocalMoveX(1500, 0.4f));
            menuTransitionSequence.AppendCallback(() =>
            {
                buttonsParent.localPosition = new Vector3(-1500, 0, 0);
                foreach (MenuButton menuButton in menuButtons)
                {
                    buttonsPool.Release(menuButton);   
                }

                action.Invoke();
                uiBlocker.SetActive(false);
                soundController.PlaySoundEffect(SoundType.MenuSwipe);
            });
            menuTransitionSequence.Append(buttonsParent.DOLocalMoveX(0, 0.4f));
        }
    }
}
