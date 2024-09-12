using Xunit;
using Moq;
using System;


public class PlayerTests
{
    [Fact]
    public void Player_ShouldInitializeWithPosition()
    {
        // Arrange
        var mockPosition = new Mock<IPosition>();
        var mockBoard = new Mock<IBoard>();

        // Act
        var player = new Player(mockPosition.Object, mockBoard.Object);

        // Assert
        Assert.Equal(mockPosition.Object, player.CurrentPosition);
    }

    [Fact]
    public void CanMove_ShouldReturnTrue_WhenMoveIsLegal()
    {
        // Arrange
        var mockPosition = new Mock<IPosition>();
        var mockNewPosition = new Mock<IPosition>();
        var mockBoard = new Mock<IBoard>();
        mockBoard.Setup(b => b.IsMoveLegal(mockPosition.Object, mockNewPosition.Object)).Returns(true);

        var player = new Player(mockPosition.Object, mockBoard.Object);

        // Act
        var result = player.CanMove(mockNewPosition.Object, mockBoard.Object);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanMove_ShouldReturnFalse_WhenMoveIsIllegal()
    {
        // Arrange
        var mockPosition = new Mock<IPosition>();
        var mockNewPosition = new Mock<IPosition>();
        var mockBoard = new Mock<IBoard>();
        mockBoard.Setup(b => b.IsMoveLegal(mockPosition.Object, mockNewPosition.Object)).Returns(false);

        var player = new Player(mockPosition.Object, mockBoard.Object);

        // Act
        var result = player.CanMove(mockNewPosition.Object, mockBoard.Object);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Move_ShouldUpdateCurrentPosition_WhenMoveIsLegal()
    {
        // Arrange
        var mockPosition = new Mock<IPosition>();
        var mockNewPosition = new Mock<IPosition>();
        var mockBoard = new Mock<IBoard>();
        mockBoard.Setup(b => b.IsMoveLegal(mockPosition.Object, mockNewPosition.Object)).Returns(true);

        var player = new Player(mockPosition.Object, mockBoard.Object);

        // Act
        player.Move(mockNewPosition.Object, mockBoard.Object);

        // Assert
        Assert.Equal(mockNewPosition.Object, player.CurrentPosition);
    }

    [Fact]
    public void Move_ShouldThrowException_WhenMoveIsIllegal()
    {
        // Arrange
        var mockPosition = new Mock<IPosition>();
        var mockNewPosition = new Mock<IPosition>();
        var mockBoard = new Mock<IBoard>();
        mockBoard.Setup(b => b.IsMoveLegal(mockPosition.Object, mockNewPosition.Object)).Returns(false);

        var player = new Player(mockPosition.Object, mockBoard.Object);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => player.Move(mockNewPosition.Object, mockBoard.Object));
        Assert.Equal("Illegal move", exception.Message);
    }
}


