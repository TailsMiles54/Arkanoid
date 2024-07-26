using System;
using System.Collections.Generic;
using DG.Tweening;
using MiniIT.ARKANOID.Settings;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Transform        buttonsParent;
        [SerializeField] private GameObject       uiBlocker;
        
        private ObjectPool<MenuButton>            buttonsPool;
        private Sequence                          menuTransitionSequence;
        private GameController                    gameController;
        private SoundController                   soundController;
        private List<MenuButton>                  menuButtons;

        [Inject]
        public void Construct(GameController gameController, SoundController soundController)
        {
            this.gameController = gameController;
            this.soundController = soundController;
        }
        
        public void Start()
        {
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
                gameController.ChangeSoundState();
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
                foreach (var menuButton in menuButtons)
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
