using System;

public class GameController
{
    private GameType            gameType;
    private bool                soundEnabled;
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