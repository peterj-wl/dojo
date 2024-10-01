using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Application
{
    public record Player(string Name, char MarkerSymbol)
    {
        [Required]
        [AllowedValues("Bot 1", "Bot 2")]
        public string Name { get; } = Name;

        [Required]
        [AllowedValues('X', 'O')]
        public char MarkerSymbol { get; } = MarkerSymbol;
    }
}
