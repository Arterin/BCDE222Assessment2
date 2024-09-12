using Xunit;
using Moq;


public class LevelTests
{
    [Fact]
    public void Level_ShouldInitializeWithProperties()
    {
        // Arrange
        var mockBoard = new Mock<IBoard>();
        var mockPlayer = new Mock<IPlayer>();
        var mockStartPosition = new Mock<IPosition>();
        var mockEndPosition = new Mock<IPosition>();

        // Act
        var level = new Level(mockBoard.Object, mockPlayer.Object, mockStartPosition.Object, mockEndPosition.Object);

        // Assert
        Assert.Equal(mockBoard.Object, level.Board);
        Assert.Equal(mockPlayer.Object, level.Player);
        Assert.Equal(mockStartPosition.Object, level.StartPosition);
        Assert.Equal(mockEndPosition.Object, level.EndPosition);
    }

    [Fact]
    public void IsCompleted_ShouldReturnTrue_WhenPlayerIsAtEndPosition()
    {
        // Arrange
        var mockBoard = new Mock<IBoard>();
        var mockPlayer = new Mock<IPlayer>();
        var mockStartPosition = new Mock<IPosition>();
        var mockEndPosition = new Mock<IPosition>();

        mockPlayer.Setup(p => p.CurrentPosition).Returns(mockEndPosition.Object);
        mockEndPosition.Setup(e => e.Equals(mockEndPosition.Object)).Returns(true);

        var level = new Level(mockBoard.Object, mockPlayer.Object, mockStartPosition.Object, mockEndPosition.Object);

        // Act
        var isCompleted = level.IsCompleted;

        // Assert
        Assert.True(isCompleted);
    }

    [Fact]
    public void IsCompleted_ShouldReturnFalse_WhenPlayerIsNotAtEndPosition()
    {
        // Arrange
        var mockBoard = new Mock<IBoard>();
        var mockPlayer = new Mock<IPlayer>();
        var mockStartPosition = new Mock<IPosition>();
        var mockEndPosition = new Mock<IPosition>();
        var mockCurrentPosition = new Mock<IPosition>();

        mockPlayer.Setup(p => p.CurrentPosition).Returns(mockCurrentPosition.Object);
        mockEndPosition.Setup(e => e.Equals(mockCurrentPosition.Object)).Returns(false);

        var level = new Level(mockBoard.Object, mockPlayer.Object, mockStartPosition.Object, mockEndPosition.Object);

        // Act
        var isCompleted = level.IsCompleted;

        // Assert
        Assert.False(isCompleted);
    }
}



