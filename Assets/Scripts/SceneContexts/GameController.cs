public class GameController
{
    private GameType            gameType;
    public GameType             GameType => gameType;

    public void ChangeGameType(GameType gameType)
    {
        this.gameType = gameType;
    }
}