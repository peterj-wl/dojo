using TicTacToe.Application;
using Xunit;

namespace TicTacToe.Tests;

public class BoardTests
{
    private readonly Board _board = new();

    [Fact]
    public void Reset_ResettingBoardAfterOnePlacedMarker_ShouldClearBoard()
    {
        // Arrange
        _board.PlaceMarkerOnCell(0, 'X');

        // Act
        _board.Reset();
        var actualCells = _board.GetCells();

        // Assert
        Assert.Equal(new char[9], actualCells);
    }

    [Fact]
    public void IsFull_PlaceOneMarkerOnBoard_ShouldReturnBoardIsNotFull()
    {
        // Arrange
        _board.PlaceMarkerOnCell(0, 'X');

        // Act
        var isFull = _board.IsFull();

        // Assert
        Assert.False(isFull);
    }

    [Fact]
    public void IsFull_PlaceAllMarkersOnBoard_ShouldReturnBoardIsFull()
    {
        // Arrange
        var positions = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        foreach (var position in positions)
            _board.PlaceMarkerOnCell(position, 'X');

        // Act
        var isFull = _board.IsFull();

        // Assert
        Assert.True(isFull);
    }

    [Fact]
    public void IsEmpty_ResetIsMade_ShouldReturnBoardIsEmpty()
    {
        // Arrange
        _board.Reset();

        // Act
        var isEmpty = _board.IsEmpty();

        // Assert
        Assert.True(isEmpty); // The board should be empty after reset
    }

    [Fact]
    public void GetCells_PlaceTwoMarkerOnBoard_ShouldReturnCurrentCells()
    {
        // Arrange
        _board.PlaceMarkerOnCell(0, 'X');
        _board.PlaceMarkerOnCell(1, 'O');
        var expectedCells = new char[9];
        expectedCells[0] = 'X';
        expectedCells[1] = 'O';

        // Act
        var actualCells = _board.GetCells();

        // Assert
        Assert.Equal(expectedCells, actualCells);
    }

    [Fact]
    public void GetAvailablePositions_BoardIsEmpty_ReturnsAllPositions()
    {
        // Arrange
        var board = new Board();

        // Act
        var availablePositions = board.GetAvailablePositions();

        // Assert
        Assert.Equal(9, availablePositions.Count); 
        Assert.All(availablePositions, position => Assert.InRange(position, 0, 8));
    }

    [Fact]
    public void GetAvailablePositions_OneSpotIsTaken_ReturnsEightPositions()
    {
        // Arrange
        _board.PlaceMarkerOnCell(0, 'X');

        // Act
        var availablePositions = _board.GetAvailablePositions();

        // Assert
        Assert.Equal(8, availablePositions.Count); // One position is taken, so 8 should be available
        Assert.DoesNotContain(0, availablePositions); // Position 0 should not be available

        // Assert that all remaining positions are within the range of 1 to 8
        Assert.All(availablePositions, position => Assert.InRange(position, 1, 8));
    }

    [Fact]
    public void PlaceMarkerOnCell_CellIsEmpty_ShouldPlaceMarker()
    {
        // Act
        _board.PlaceMarkerOnCell(0, 'X');

        // Assert
        Assert.Equal('X', _board.GetCells()[0]);
    }

    [Fact]
    public void PlaceMarkerOnCell_CellIsOccupied_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _board.PlaceMarkerOnCell(0, 'X'); // Place 'X' in position 0

        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => _board.PlaceMarkerOnCell(0, 'O'));

        // Assert
        Assert.Equal("Cell is already occupied.", exception.Message); // Check the exception message
    }

    [Fact]
    public void PlaceMarkerOnCell_PositionIsOutOfRange_ShouldThrowArgumentOutOfRangeException()
    {
        // Act
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _board.PlaceMarkerOnCell(10, 'X'));

        // Assert
        Assert.Equal("Position must be between 0 and 8. (Parameter 'position')", exception.Message); // Check the exception message
    }
}