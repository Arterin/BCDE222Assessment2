using Moq;
using Xunit;

public class TestGame
{
    [Fact]
    public void LoadLevel_ShouldSetCurrentLevel()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        var game = new Game(mockLevel.Object);

        // Act
        game.LoadLevel(mockLevel.Object);

        // Assert
        Assert.Equal(mockLevel.Object, game.CurrentLevel);
    }

    [Fact]
    public void MakeMove_ShouldReturnTrue_WhenMoveIsSuccessful()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        var mockPlayer = new Mock<IPlayer>();
        var mockBoard = new Mock<IBoard>();
        var mockNewPosition = new Mock<IPosition>();

        mockLevel.Setup(l => l.Player).Returns(mockPlayer.Object);
        mockLevel.Setup(l => l.Board).Returns(mockBoard.Object);
        mockPlayer.Setup(p => p.CanMove(mockNewPosition.Object, mockBoard.Object)).Returns(true);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);

        // Act
        var result = game.MakeMove(mockNewPosition.Object);

        // Assert
        Assert.True(result);
        mockPlayer.Verify(p => p.Move(mockNewPosition.Object, mockBoard.Object), Times.Once);
    }

    [Fact]
    public void MakeMove_ShouldReturnFalse_WhenMoveIsUnsuccessful()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        var mockPlayer = new Mock<IPlayer>();
        var mockBoard = new Mock<IBoard>();
        var mockNewPosition = new Mock<IPosition>();

        mockLevel.Setup(l => l.Player).Returns(mockPlayer.Object);
        mockLevel.Setup(l => l.Board).Returns(mockBoard.Object);
        mockPlayer.Setup(p => p.CanMove(mockNewPosition.Object, mockBoard.Object)).Returns(false);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);

        // Act
        var result = game.MakeMove(mockNewPosition.Object);

        // Assert
        Assert.False(result);
        mockPlayer.Verify(p => p.Move(mockNewPosition.Object, mockBoard.Object), Times.Never);
    }

    [Fact]
    public void IsGameOver_ShouldReturnTrue_WhenLevelIsCompleted()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        mockLevel.Setup(l => l.IsCompleted).Returns(true);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);

        // Act
        var isGameOver = game.IsGameOver;

        // Assert
        Assert.True(isGameOver);
    }

    [Fact]
    public void IsGameOver_ShouldReturnFalse_WhenLevelIsNotCompleted()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        mockLevel.Setup(l => l.IsCompleted).Returns(false);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);

        // Act
        var isGameOver = game.IsGameOver;

        // Assert
        Assert.False(isGameOver);
    }

    [Fact]
    public void GetMoveCount_ShouldReturnNumberOfMovesMade()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        var mockPlayer = new Mock<IPlayer>();
        var mockBoard = new Mock<IBoard>();
        var mockNewPosition = new Mock<IPosition>();

        mockLevel.Setup(l => l.Player).Returns(mockPlayer.Object);
        mockLevel.Setup(l => l.Board).Returns(mockBoard.Object);
        mockPlayer.Setup(p => p.CanMove(mockNewPosition.Object, mockBoard.Object)).Returns(true);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);

        // Act
        game.MakeMove(mockNewPosition.Object);
        game.MakeMove(mockNewPosition.Object);
        var moveCount = game.GetMoveCount();

        // Assert
        Assert.Equal(2, moveCount);
    }

    [Fact]
    public void Undo_ShouldRevertLastMove()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        var mockPlayer = new Mock<IPlayer>();
        var mockBoard = new Mock<IBoard>();
        var mockStartPosition = new Mock<IPosition>();
        var mockNewPosition = new Mock<IPosition>();

        mockLevel.Setup(l => l.Player).Returns(mockPlayer.Object);
        mockLevel.Setup(l => l.Board).Returns(mockBoard.Object);
        mockLevel.Setup(l => l.StartPosition).Returns(mockStartPosition.Object);
        mockPlayer.Setup(p => p.CanMove(mockNewPosition.Object, mockBoard.Object)).Returns(true);
        mockPlayer.Setup(p => p.CurrentPosition).Returns(mockStartPosition.Object);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);
        game.MakeMove(mockNewPosition.Object);

        // Act
        game.Undo();

        // Assert
        mockPlayer.Verify(p => p.Move(mockStartPosition.Object, mockBoard.Object), Times.Once);
    }


    [Fact]
    public void Restart_ShouldResetPlayerToStartPosition()
    {
        // Arrange
        var mockLevel = new Mock<ILevel>();
        var mockPlayer = new Mock<IPlayer>();
        var mockBoard = new Mock<IBoard>();
        var mockStartPosition = new Mock<IPosition>();

        mockLevel.Setup(l => l.Player).Returns(mockPlayer.Object);
        mockLevel.Setup(l => l.Board).Returns(mockBoard.Object);
        mockLevel.Setup(l => l.StartPosition).Returns(mockStartPosition.Object);

        var game = new Game(mockLevel.Object);
        game.LoadLevel(mockLevel.Object);

        // Act
        game.Restart();

        // Assert
        mockPlayer.Verify(p => p.Move(mockStartPosition.Object, mockBoard.Object), Times.Once);
    }
}
