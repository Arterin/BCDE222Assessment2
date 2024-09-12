using ChessMaze.Enums;
using Xunit;

public class TestBoard
{
    private readonly Board board;
    private readonly IPiece pawn;
    private readonly IPiece enemyPiece;

    public TestBoard()
    {
        board = new Board(8, 8);
        pawn = new Piece(PieceType.Pawn);
        enemyPiece = new Piece(PieceType.Rook);
    }

    [Fact]
    public void PawnMoveForwardOneStep_IsLegal()
    {
        // Arrange
        var from = new Position(1, 0);
        var to = new Position(2, 0);
        board.PlacePiece(pawn, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PawnMoveForwardTwoStepsFromStart_IsLegal()
    {
        // Arrange
        var from = new Position(1, 0);
        var to = new Position(3, 0);
        board.PlacePiece(pawn, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PawnMoveForwardTwoStepsNotFromStart_IsIllegal()
    {
        // Arrange
        var from = new Position(2, 0);
        var to = new Position(4, 0);
        board.PlacePiece(pawn, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void PawnCaptureDiagonally_IsLegal()
    {
        // Arrange
        var from = new Position(1, 0);
        var to = new Position(2, 1);
        board.PlacePiece(pawn, from);
        board.PlacePiece(enemyPiece, to);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PawnMoveForwardBlockedByPiece_IsIllegal()
    {
        // Arrange
        var from = new Position(1, 0);
        var to = new Position(2, 0);
        board.PlacePiece(pawn, from);
        board.PlacePiece(enemyPiece, to);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RookMoveHorizontally_IsLegal()
    {
        // Arrange
        var rook = new Piece(PieceType.Rook);
        var from = new Position(0, 0);
        var to = new Position(0, 7);
        board.PlacePiece(rook, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RookMoveVertically_IsLegal()
    {
        // Arrange
        var rook = new Piece(PieceType.Rook);
        var from = new Position(0, 0);
        var to = new Position(7, 0);
        board.PlacePiece(rook, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RookMoveDiagonally_IsIllegal()
    {
        // Arrange
        var rook = new Piece(PieceType.Rook);
        var from = new Position(0, 0);
        var to = new Position(7, 7);
        board.PlacePiece(rook, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void BishopMoveDiagonally_IsLegal()
    {
        // Arrange
        var bishop = new Piece(PieceType.Bishop);
        var from = new Position(0, 0);
        var to = new Position(7, 7);
        board.PlacePiece(bishop, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void BishopMoveHorizontally_IsIllegal()
    {
        // Arrange
        var bishop = new Piece(PieceType.Bishop);
        var from = new Position(0, 0);
        var to = new Position(0, 7);
        board.PlacePiece(bishop, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void KnightMoveInLShape_IsLegal()
    {
        // Arrange
        var knight = new Piece(PieceType.Knight);
        var from = new Position(0, 0);
        var to = new Position(2, 1);
        board.PlacePiece(knight, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void KnightMoveStraight_IsIllegal()
    {
        // Arrange
        var knight = new Piece(PieceType.Knight);
        var from = new Position(0, 0);
        var to = new Position(0, 2);
        board.PlacePiece(knight, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void KingMoveOneStepInAnyDirection_IsLegal()
    {
        // Arrange
        var king = new Piece(PieceType.King);
        var from = new Position(4, 4);
        var to = new Position(5, 5);
        board.PlacePiece(king, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void KingMoveMoreThanOneStep_IsIllegal()
    {
        // Arrange
        var king = new Piece(PieceType.King);
        var from = new Position(4, 4);
        var to = new Position(6, 6);
        board.PlacePiece(king, from);

        // Act
        bool result = board.IsMoveLegal(from, to);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValidPosition_WithinBounds_IsTrue()
    {
        // Arrange
        var position = new Position(4, 4);

        // Act
        bool result = board.IsValidPosition(position);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValidPosition_OutOfBounds_IsFalse()
    {
        // Arrange
        var position = new Position(8, 8);

        // Act
        bool result = board.IsValidPosition(position);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValidPosition_NegativeCoordinates_IsFalse()
    {
        // Arrange
        var position = new Position(-1, -1);

        // Act
        bool result = board.IsValidPosition(position);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void PlacePiece_PieceIsPlacedCorrectly()
    {
        // Arrange
        var position = new Position(4, 4);
        var piece = new Piece(PieceType.King);

        // Act
        board.PlacePiece(piece, position);
        var placedPiece = board.GetPieceAt(position);

        // Assert
        Assert.Equal(piece, placedPiece);
    }

    
}
