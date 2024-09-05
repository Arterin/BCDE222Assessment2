public class Level : ILevel
{
    public IBoard Board { get; }
    public IPosition StartPosition { get; }
    public IPosition EndPosition { get; }
    public IPlayer Player { get; }
    public bool IsCompleted { get { return Player.CurrentPosition.Equals(EndPosition); } }

    public Level(IBoard board, IPlayer player, IPosition startPosition, IPosition endPosition)
    {
        Board = board;
        Player = player;
        StartPosition = startPosition;
        EndPosition = endPosition;
    }
}
