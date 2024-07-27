using System;
using MiniIT.ARKANOID.Enums;

namespace MiniIT.ARKANOID.Controllers
{
    public class GameController
    {
        private GameType            gameType = GameType.Full;
        private bool                soundEnabled = false;
        public GameType             GameType => gameType;
        public bool                 SoundEnabled => soundEnabled;

        public event Action         SoundStateChanged;

        public void ChangeGameType(GameType gameType)
        {
            this.gameType = gameType;
        }

        public void ChangeSoundState()
        {
            soundEnabled = !soundEnabled;
            SoundStateChanged?.Invoke();
        }
    }
}