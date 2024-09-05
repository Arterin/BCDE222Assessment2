public class Game : IGame
{
    private int moveCount = 0;
    private List<IPosition> positionHistory = new List<IPosition>();

    public Game(ILevel aLevel)
    {
        CurrentLevel = aLevel;
    }

    public ILevel CurrentLevel { get; set; }

    public bool IsGameOver { get { return CurrentLevel.IsCompleted; } }

    public int GetMoveCount()
    {
        return moveCount;
    }

    public void LoadLevel(ILevel aLevel)
    {
        CurrentLevel = aLevel;
    }

    public bool MakeMove(IPosition newPosition)
    {
        if (!CurrentLevel.Player.CanMove(newPosition, CurrentLevel.Board))
        {
            return false;
        }

        positionHistory.Add(CurrentLevel.Player.CurrentPosition);
        CurrentLevel.Player.Move(newPosition, CurrentLevel.Board);
        moveCount++;

        return true;
    }

    public void Restart()
    {
        CurrentLevel.Player.Move(CurrentLevel.StartPosition, CurrentLevel.Board);
        moveCount = 0;
        positionHistory = [];
    }

    public void Undo()
    {
        if (moveCount > 0)
        {
            moveCount--;
            IPosition lastPosition = positionHistory[positionHistory.Count - 1];
            positionHistory.RemoveAt(positionHistory.Count - 1);
            CurrentLevel.Player.Move(lastPosition, CurrentLevel.Board);
        }
    }
}
