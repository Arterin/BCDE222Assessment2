using ChessMaze.Enums;

public class Piece(PieceType type) : IPiece
{
    public PieceType Type { get; } = type;
}
