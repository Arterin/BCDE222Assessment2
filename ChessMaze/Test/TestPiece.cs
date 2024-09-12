using Xunit;
using ChessMaze.Enums;

public class PieceTests
{
    [Fact]
    public void Piece_ShouldInitializeWithType()
    {
        // Arrange
        var expectedType = PieceType.Knight;

        // Act
        var piece = new Piece(expectedType);

        // Assert
        Assert.Equal(expectedType, piece.Type);
    }

    [Fact]
    public void Piece_ShouldFailWhenTypeIsIncorrect()
    {
        // Arrange
        var expectedType = PieceType.Knight;
        var incorrectType = PieceType.Bishop;

        // Act
        var piece = new Piece(incorrectType);

        // Assert
        Assert.NotEqual(expectedType, piece.Type);
    }
}


