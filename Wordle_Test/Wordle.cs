using System;
using System.Collections.Generic;
using System.Linq;

namespace Wordle_Test;

//dotnet test --collect:"XPlat Code Coverage"

public class WordleGame
{
    private static readonly List<string> Dictionary = new List<string>
    {
        "APPLE", "BANJO", "CRISP", "DREAM", "FLUTE", "GRAPE", "HORSE", "JOKER", "KITES", "LUCKY"
    };

    public readonly string _targetWord;
    public int _remainingAttempts;
    public bool IsGameOver => _remainingAttempts == 0 || IsWordGuessed;
    public bool IsWordGuessed { get; private set; }
    public int RemainingAttempts => _remainingAttempts;

    public WordleGame()
    {
        _targetWord = Dictionary[new Random().Next(Dictionary.Count)];
        _remainingAttempts = 6;
    }

    public WordleGame(string targetWord)
    {
        _targetWord = targetWord.ToUpper();
        _remainingAttempts = 6;
    }


    public string CheckGuess(string guess)
    {
        if (guess.Length != 5 || !guess.All(char.IsLetter))
            throw new ArgumentException("Guess must be exactly 5 letters and alphabetic.");

        guess = guess.ToUpper();
        char[] result = new char[5];
        bool[] used = new bool[5];

        for (int i = 0; i < 5; i++)
        {
            if (guess[i] == _targetWord[i])
            {
                result[i] = 'G';
                used[i] = true;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (result[i] == 'G') continue;

            for (int j = 0; j < 5; j++)
            {
                if (!used[j] && guess[i] == _targetWord[j])
                {
                    result[i] = 'Y';
                    used[j] = true;
                    break;
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (result[i] == '\0')
                result[i] = 'X';
        }

        _remainingAttempts--;
        if (guess == _targetWord)
            IsWordGuessed = true;

        return new string(result);
    }
}

public class Program
{
    public static void Main()
    {
        int streak = 0;
        bool playAgain;

        do
        {
            Console.WriteLine("Bienvenue dans Wordle ! Devinez le mot mystère de 5 lettres.");
            WordleGame game = new WordleGame();

            while (!game.IsGameOver)
            {
                Console.WriteLine($"Tentatives restantes : {game.RemainingAttempts}");
                Console.Write("Entrez votre mot : ");
                string guess = Console.ReadLine().Trim();

                try
                {
                    string feedback = game.CheckGuess(guess);
                    Console.WriteLine("Résultat : " + feedback);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (game.IsWordGuessed)
            {
                Console.WriteLine("Félicitations ! Vous avez trouvé le mot !");
                streak++;
            }
            else
            {
                Console.WriteLine("Dommage ! Le mot était inconnu.");
                streak = 0;
            }

            Console.WriteLine($"Votre streak actuelle est de : {streak}");
            Console.Write("Voulez-vous rejouer ? (o/n) : ");
            playAgain = Console.ReadLine().Trim().ToLower() == "o";

        } while (playAgain);

        Console.WriteLine("Merci d'avoir joué ! Appuyez sur Entrée pour quitter...");
        Console.ReadLine();
    }
}
