using Wordle_Test;
using System;
using Xunit;

public class WordleTest
{
    [Fact]
    public void Constructor_InitializesGameCorrectly()
    {
        var game = new WordleGame();
        Assert.False(game.IsGameOver);
        Assert.False(game.IsWordGuessed);
        Assert.Equal(6, game.RemainingAttempts);
    }

    [Theory]
    [InlineData("ABCDE")]
    [InlineData("apple")]
    public void CheckGuess_ValidInput_DoesNotThrowException(string guess)
    {
        var game = new WordleGame();
        var exception = Record.Exception(() => game.CheckGuess(guess));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("ABCD")]
    [InlineData("12345")]
    [InlineData("ABCD1")]
    public void CheckGuess_InvalidInput_ThrowsArgumentException(string guess)
    {
        var game = new WordleGame();
        Assert.Throws<ArgumentException>(() => game.CheckGuess(guess));
    }

    [Fact]
    public void CheckGuess_IncorrectGuess_DecreasesAttempts()
    {
        var game = new WordleGame();
        int attemptsBefore = game.RemainingAttempts;
        game.CheckGuess("CRISP");
        Assert.Equal(attemptsBefore - 1, game.RemainingAttempts);
    }

    [Fact]
    public void Game_Ends_AfterMaxAttempts()
    {
        var game = new WordleGame();
        for (int i = 0; i < 6; i++)
        {
            game.CheckGuess("CRISP");
        }
        Assert.True(game.IsGameOver);
    }

    [Fact]
    public void CheckGuess_CorrectGuess_SetsIsWordGuessedTrue()
    {
        var game = new WordleGame("CRISP");
        game.CheckGuess("CRISP");
        Assert.True(game.IsWordGuessed);
        Assert.True(game.IsGameOver);
    }

    [Fact]
    public void CheckGuess_NoCorrectLetters_ReturnsAllX()
    {
        var game = new WordleGame("LUCKY");
        string feedback = game.CheckGuess("TREND");
        Assert.Equal("XXXXX", feedback);
    }
}
