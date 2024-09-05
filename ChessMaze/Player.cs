public class Player(IPosition position, IBoard board) : IPlayer
{
    public IPosition CurrentPosition { get; set; } = position;

    public bool CanMove(IPosition newPosition, IBoard board)
    {
        return board.IsMoveLegal(CurrentPosition, newPosition);
    }

    public void Move(IPosition newPosition, IBoard board)
    {
        this.CurrentPosition = newPosition;
    }
}
