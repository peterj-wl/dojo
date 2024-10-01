using TicTacToe.Application;
using TicTacToe.Tests.Helpers;
using Xunit;

namespace TicTacToe.Tests
{
    public class PlayerTests
    {
        [Theory]
        [InlineData("Bot 1")]
        [InlineData("Bot 2")]
        public void CreatePlayer_WithValidName_ShouldPass(string name)
        {
            var model = new Player(name, 'X');
            var validationResult = ModelValidationHelper.ValidateModel(model);

            Assert.Empty(validationResult);
        }

        [Theory]
        [InlineData("Jens")]
        [InlineData(null)]
        public void CreatePlayer_WithInvalidOrEmptyName_ShouldNotPass(string name)
        {
            var model = new Player(null, 'X');
            var validationResult = ModelValidationHelper.ValidateModel(model);

            Assert.NotEmpty(validationResult);
        }

        [Theory]
        [InlineData('X')]
        [InlineData('O')]
        public void CreatePlayer_WithValidMarkerSymbol_ShouldPass(char symbol)
        {
            var model = new Player("Bot 1", symbol);
            var validationResult = ModelValidationHelper.ValidateModel(model);

            Assert.Empty(validationResult);
        }

        [Theory]
        [InlineData('B')]
        [InlineData(null)]
        public void CreatePlayer_WithInvalidOrEmptyMarkerSymbol_ShouldNotPass(char symbol)
        {
            var model = new Player("Bot 1", symbol);
            var validationResult = ModelValidationHelper.ValidateModel(model);

            Assert.NotEmpty(validationResult);
        }
    }
}
