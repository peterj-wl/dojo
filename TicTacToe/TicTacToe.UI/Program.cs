using TicTacToe.Application;

namespace TicTacToe.UI;

public class Program
{
    private static void Main()
    {
        var game = new Game();

        while (true)
        {
            Console.Clear();
            DisplayBoard();

            var position = game.GetRandomPositionThatIsNotTakenOnBoard();
            game.CurrentPlayerPlaceNextMarker(position);

            var winnerMarker = game.CheckForAnyWinningCombinationMarkerSymbol();
            if (winnerMarker != '\0')
            {
                Console.Clear();
                Console.WriteLine($"The epic battle between {game.ListPlayers()} is over and we have a winner!");
                Console.WriteLine("");

                DisplayBoard();

                Console.WriteLine($"Player {game.GetCurrentPlayer().Name} wins!");
                break;
            }

            if (game.IsBoardFull())
            {
                Console.Clear();
                Console.WriteLine($"The epic battle between {game.ListPlayers()} is over and we have a draw!");
                Console.WriteLine("");

                DisplayBoard();
                break;
            }

            game.SwitchPlayer();
        }

        return;

        void DisplayBoard()
        {
            var boardCells = game.GetBoardCells();

            Console.WriteLine("Current Board:");
            Console.WriteLine("|---|---|---|");
            for (var i = 0; i < 3; i++)
            {
                Console.Write("|");
                for (var j = 0; j < 3; j++)
                {
                    var cell = boardCells[i * 3 + j] == '\0' ? ' ' : boardCells[i * 3 + j];
                    Console.Write($" {cell} |");
                }
                Console.WriteLine();
                Console.WriteLine("|---|---|---|");
            }
        }
    }
}