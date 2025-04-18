using Wordle_Test;

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
    public void CheckGuess_ValidInput(string guess)
    {
        var game = new WordleGame();
        var exception = Record.Exception(() => game.CheckGuess(guess));
        Assert.Null(exception);
    }

    [Fact]
    public void CheckGuess_IncorrectGuess_DecreasesAttempts()
    {
        var game = new WordleGame();
        int attemptsBefore = game.RemainingAttempts;
        game.CheckGuess("TESTE");
        Assert.Equal(attemptsBefore - 1, game.RemainingAttempts);
    }

    [Fact]
    public void Game_Ends_AfterMaxAttempts()
    {
        var game = new WordleGame();
        for (int i = 0; i < 6; i++)
        {
            game.CheckGuess("TESTE");
        }
        Assert.True(game.IsGameOver);
    }

    [Fact]
    public void CheckGuess_CorrectGuess()
    {
        var game = new WordleGame("TESTE");
        game.CheckGuess("TESTE");
        Assert.True(game.IsWordGuessed);
        Assert.True(game.IsGameOver);
    }

    [Theory]
    [InlineData("ABCDE", true)]
    [InlineData("ABCD", false)]
    [InlineData("123456", false)]
    public void IsFiveLetters(string input, bool expected)
    {
        var game = new WordleGame();
        Assert.Equal(expected, game.IsFiveLetters(input));
    }

    [Theory]
    [InlineData("APPLE", true)]
    [InlineData("abcde", true)]
    [InlineData("AB1CD", false)]
    [InlineData("12ABC", false)]
    public void IsOnlyLetters(string input, bool expected)
    {
        var game = new WordleGame();
        Assert.Equal(expected, game.IsOnlyLetters(input));
    }

    [Theory]
    [InlineData("GRAPE", "GRAPE", "GGGGG")]
    [InlineData("GRAPE", "PAGAR", "YYYXY")]
    [InlineData("GRAPE", "AAAAA", "XXGXX")]
    [InlineData("GRAPE", "XXXXX", "XXXXX")]
    [InlineData("GRAPE", "EPRAG", "YYYYY")]
    [InlineData("LUCKY", "CURLY", "YGXYG")]
    public void CheckGuess(string targetWord, string guess, string expectedFeedback)
    {
        var game = new WordleGame(targetWord);
        string actualFeedback = game.CheckGuess(guess);
        Assert.Equal(expectedFeedback, actualFeedback);
    }

    [Fact]
    public void PrintColoredFeedback()
    {
        var game = new WordleGame();
        var exception = Record.Exception(() => game.PrintColoredFeedback("APPLE", "GYXXY"));
        Assert.Null(exception);
    }

    [Fact]
    public void Constructor_ConvertsTargetWordToUpperCase()
    {
        var game = new WordleGame("teste");
        var feedback = game.CheckGuess("TESTE");
        Assert.Equal("GGGGG", feedback);
    }

    [Theory]
    [InlineData(12, 3, 4.0)]
    [InlineData(7, 2, 3.5)]
    [InlineData(0, 0, 0.0)]
    public void CalculateAverageAttempts_ReturnsCorrectAverage(int totalAttempts, int totalGames, double expected)
    {
        var game = new WordleGame();
        double result = game.CalculateAverageAttempts(totalAttempts, totalGames);
        Assert.Equal(expected, result, 2);
    }
}
