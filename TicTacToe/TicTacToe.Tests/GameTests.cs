using TicTacToe.Application;
using Xunit;

namespace TicTacToe.Tests
{
    public class GameTests
    {
        private readonly Game _game = new();

        [Fact]
        public void Game_GameIsCreatedWithABoardContainingCells_ShouldReturnNineCells()
        {
            // Assert
            Assert.Equal(9, _game.GetBoardCells().Length);
        }

        [Fact]
        public void Game_ListPlayers_ShouldReturnPlayerNames()
        {
            // Act
            var playerNames = _game.ListPlayers();

            // Assert
            Assert.Equal("Bot 1, Bot 2", playerNames);
        }

        [Fact]
        public void Game_CurrentPlayerNameIsListed_ShouldReturnBot1()
        {
            // Assert
            Assert.Equal("Bot 1", _game.GetCurrentPlayer().Name);
        }

        [Fact]
        public void IsBoardFull_AllPositionsIsTakenOnBoard_ShouldReturnThatBoardIsFull()
        {
            // Arrange
            int[] positions = [0, 1, 2, 3, 4, 5, 6, 7, 8];

            // Act
            foreach (var position in positions)
                _game.CurrentPlayerPlaceNextMarker(position);

            // Assert
            Assert.True(_game.IsBoardFull());
        }

        [Fact]
        public void IsBoardFull_ThereIsFreePositionsOnBoard_ShouldNotReturnThatBoardIsFull()
        {
            // Arrange
            int[] positions = [0, 1];

            // Act
            foreach (var position in positions)
                _game.CurrentPlayerPlaceNextMarker(position);

            // Assert
            Assert.False(_game.IsBoardFull());
        }

        [Fact]
        public void RandomPosition_RandomMoveIsValid_ShouldReturnAValidPositionInRangeOfBoard()
        {
            // Act
            var position = _game.GetRandomPositionThatIsNotTakenOnBoard();

            // Assert
            Assert.InRange(position, 0, 8);
            Assert.True(_game.GetBoardCells()[position] == '\0');
        }

        [Fact]
        public void SwitchingPlayers_SwitchPlayerFromCurrentToNextPlayer_ShouldReturnNextPlayer()
        {
            // Arrange
            var playerBeforeNextMarkerMove = _game.GetCurrentPlayer();

            // Act
            _game.SwitchPlayer();

            // Assert
            Assert.NotEqual(playerBeforeNextMarkerMove, _game.GetCurrentPlayer());
        }

        [Fact]
        public void PlaceNextMarker_CurrentPlayerPlacesNextMarkerOnAValidPosition_ShouldPlaceMarker()
        {
            // Arrange
            var position = 0;
            var expectedMarker = _game.GetCurrentPlayer().MarkerSymbol;

            // Act
            _game.CurrentPlayerPlaceNextMarker(position);

            // Assert
            Assert.Equal(expectedMarker, _game.GetBoardCells()[position]);
        }

        [Fact]
        public void CheckForAnyWinningCombination_Draw_ShouldReturnNoWinner()
        {
            // Arrange
            _game.CurrentPlayerPlaceNextMarker(0); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(1); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(2); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(4); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(7); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(3); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(5); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(8); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(5); // X (Draw move)

            // Act
            var winner = _game.CheckForAnyWinningCombinationMarkerSymbol();

            // Assert
            Assert.Equal('\0', winner);
        }

        [Fact]
        public void CheckForAnyWinningCombinationMarker_XWins_ShouldReturnXAsWinner()
        {
            // Arrange
            _game.CurrentPlayerPlaceNextMarker(0); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(1); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(3); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(4); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(6); // X (Winning move)

            // Act
            char winner = _game.CheckForAnyWinningCombinationMarkerSymbol();

            // Assert
            Assert.Equal('X', winner);
        }

        [Fact]
        public void CheckForAnyWinningCombinationMarker_OWins_ShouldReturnOAsWinner()
        {
            // Arrange
            _game.CurrentPlayerPlaceNextMarker(0); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(1); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(3); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(4); // O
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(2); // X
            _game.SwitchPlayer();
            _game.CurrentPlayerPlaceNextMarker(7); // O (Winning move)

            // Act
            char winner = _game.CheckForAnyWinningCombinationMarkerSymbol();

            // Assert
            Assert.Equal('O', winner);
        }

        [Theory]
        [InlineData(new[] { 'X', 'X', 'X', ' ', ' ', ' ', ' ', ' ', ' ' }, 'X')] // Row 1
        [InlineData(new[] { ' ', ' ', ' ', 'X', 'X', 'X', ' ', ' ', ' ' }, 'X')] // Row 2
        [InlineData(new[] { ' ', ' ', ' ', ' ', ' ', ' ', 'X', 'X', 'X' }, 'X')] // Row 3
        [InlineData(new[] { 'X', ' ', ' ', 'X', ' ', ' ', 'X', ' ', ' ' }, 'X')] // Column 1
        [InlineData(new[] { ' ', 'X', ' ', ' ', 'X', ' ', ' ', 'X', ' ' }, 'X')] // Column 2
        [InlineData(new[] { ' ', ' ', 'X', ' ', ' ', 'X', ' ', ' ', 'X' }, 'X')] // Column 3
        [InlineData(new[] { 'X', ' ', ' ', ' ', 'X', ' ', ' ', ' ', 'X' }, 'X')] // Diagonal \
        [InlineData(new[] { ' ', ' ', 'X', ' ', 'X', ' ', 'X', ' ', ' ' }, 'X')] // Diagonal /
        [InlineData(new[] { 'O', 'O', 'O', ' ', ' ', ' ', ' ', ' ', ' ' }, 'O')] // Row 1
        [InlineData(new[] { ' ', ' ', ' ', 'O', 'O', 'O', ' ', ' ', ' ' }, 'O')] // Row 2
        [InlineData(new[] { ' ', ' ', ' ', ' ', ' ', ' ', 'O', 'O', 'O' }, 'O')] // Row 3
        [InlineData(new[] { 'O', ' ', ' ', 'O', ' ', ' ', 'O', ' ', ' ' }, 'O')] // Column 1
        [InlineData(new[] { ' ', 'O', ' ', ' ', 'O', ' ', ' ', 'O', ' ' }, 'O')] // Column 2
        [InlineData(new[] { ' ', ' ', 'O', ' ', ' ', 'O', ' ', ' ', 'O' }, 'O')] // Column 3
        [InlineData(new[] { 'O', ' ', ' ', ' ', 'O', ' ', ' ', ' ', 'O' }, 'O')] // Diagonal \
        [InlineData(new[] { ' ', ' ', 'O', ' ', 'O', ' ', 'O', ' ', ' ' }, 'O')] // Diagonal /
        [InlineData(new[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }, '\0')] // No winner
        public void CheckForAnyWinningCombinationMarker_AllCombinations_ShouldReturnExpectedMarker(char[] cells, char expected)
        {
            // Arrange
            for (var i = 0; i < cells.Length; i++)
            {
                if (cells[i] != ' ')
                {
                    _game.GetBoard().PlaceMarkerOnCell(i, cells[i]); // Place markers on the board
                }
            }

            // Act
            var actual = _game.CheckForAnyWinningCombinationMarkerSymbol();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}