namespace TicTacToe.Application;

public sealed class Game
{
    private readonly Board _board;

    private readonly Player _player1;
    private readonly Player _player2;

    private Player _currentPlayer;

    public Game()
    {
        _board = new Board();
        _board.Reset();

        _player1 = new Player("Bot 1", 'X');
        _player2 = new Player("Bot 2", 'O');

        _currentPlayer = _player1;
    }

    public void CurrentPlayerPlaceNextMarker(int position)
    {
        var availablePositions = _board.GetAvailablePositions();

        if (!availablePositions.Contains(position))
            return;

        _board.PlaceMarkerOnCell(position, _currentPlayer.MarkerSymbol);
    }

    public char CheckForAnyWinningCombinationMarkerSymbol()
    {
        var winningCombinations = new[]
        {
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 }
        };

        var cells = _board.GetCells();

        foreach (var combination in winningCombinations)
        {
            if (cells[combination[0]] != '\0' &&
                cells[combination[0]] == cells[combination[1]] &&
                cells[combination[0]] == cells[combination[2]])
            {
                return cells[combination[0]];
            }
        }

        return '\0'; // No winner
    }

    public int GetRandomPositionThatIsNotTakenOnBoard()
    {
        var availablePositions = _board.GetAvailablePositions();

        var random = new Random();
        return availablePositions[random.Next(availablePositions.Count)];
    }

    public bool IsBoardFull() => _board.IsFull();
    public void SwitchPlayer() => _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
    public string ListPlayers() => string.Join(", ", _player1.Name, _player2.Name);
    public char[] GetBoardCells() => _board.GetCells();
    public Player GetCurrentPlayer() => _currentPlayer;


    public Board GetBoard() => _board;
}
