namespace TicTacToe.Application;

public class Board
{
    private char[] _cells = new char[9];

    public List<int> GetAvailablePositions()
    {
        var availablePositions = new List<int>();

        foreach (var cell in _cells.Select((value, index) => new { value, index }))
        {
            if (cell.value == '\0')
                availablePositions.Add(cell.index);
        }

        return availablePositions;
    }

    public void PlaceMarkerOnCell(int position, char marker)
    {
        if (position < 0 || position >= _cells.Length)
            throw new ArgumentOutOfRangeException(nameof(position), "Position must be between 0 and 8.");

        if (_cells[position] == '\0')
            _cells[position] = marker;
        else
            throw new InvalidOperationException("Cell is already occupied.");
    }

    public void Reset() => _cells = new char[9];
    public bool IsFull() => _cells.All(cell => cell != '\0');
    public bool IsEmpty() => _cells.All(cell => cell == '\0');
    public char[] GetCells() => _cells;
}