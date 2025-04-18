using System;
using System.Collections.Generic;
using System.Linq;

namespace Wordle_Test;

public class WordleGame
{
    private static readonly List<string> Dictionary = new List<string>
    {
        "AVION", "BALAI", "CHAUD", "ENFER", "FLEUR", "GARDE", "JOLIE",
        "KIWIS", "LAPIN", "MONDE", "NAGER", "OPERA", "POMME", "SAUTE", "TIGRE",
        "UNION", "VIVRE", "ZEBRE"
    };

    public readonly string _targetWord;
    public int _remainingAttempts;
    public bool IsGameOver => _remainingAttempts == 0 || IsWordGuessed;
    public bool IsWordGuessed { get; private set; }
    public int RemainingAttempts => _remainingAttempts;

    public WordleGame()
    {
        _targetWord = Dictionary[new Random().Next(Dictionary.Count)].ToUpper();
        _remainingAttempts = 6;
    }

    public WordleGame(string targetWord)
    {
        _targetWord = targetWord.ToUpper();
        _remainingAttempts = 6;
    }

    public bool IsFiveLetters(string guess)
    {
        return guess.Length == 5;
    }

    public bool IsOnlyLetters(string guess)
    {
        return guess.All(char.IsLetter);
    }

    public string CheckGuess(string guess)
    {
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

    public void PrintColoredFeedback(string guess, string feedback)
    {
        for (int i = 0; i < 5; i++)
        {
            switch (feedback[i])
            {
                case 'G':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 'Y':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }

            Console.Write(guess[i]);
            Console.ResetColor();
        }
        Console.WriteLine();
    }

    public double CalculateAverageAttempts(int totalAttemptsUsed, int totalGames)
    {
        if (totalGames == 0) return 0;
        return (double)totalAttemptsUsed / totalGames;
    }
}

//-------------------------------------------------------------------------------------------------

public class Program
{
    public static void Main()
    {
        int streak = 0;
        bool playAgain;
        int totalGames = 0;
        int totalAttemptsUsed = 0;

        do
        {
            Console.WriteLine("Bienvenue dans Wordle ! Devinez le mot mystère de 5 lettres.");
            WordleGame game = new WordleGame();

            int attemptsBefore = game.RemainingAttempts;

            while (!game.IsGameOver)
            {
                Console.WriteLine($"\nTentatives restantes : {game.RemainingAttempts}");
                Console.Write("Entrez votre mot : ");
                string guess = Console.ReadLine().Trim();

                if (!game.IsFiveLetters(guess))
                {
                    Console.WriteLine("Erreur : le mot doit contenir exactement 5 lettres.");
                    continue;
                }

                if (!game.IsOnlyLetters(guess))
                {
                    Console.WriteLine("Erreur : le mot doit contenir uniquement des lettres (pas de chiffres ni symboles).");
                    continue;
                }

                string feedback = game.CheckGuess(guess);
                game.PrintColoredFeedback(guess.ToUpper(), feedback);
            }

            int attemptsUsed = attemptsBefore - game.RemainingAttempts;
            totalGames++;
            totalAttemptsUsed += attemptsUsed;

            if (game.IsWordGuessed)
            {
                Console.WriteLine("\nFélicitations ! Vous avez trouvé le mot !");
                streak++;
            }
            else
            {
                Console.WriteLine($"\nDommage ! Le mot était : {game._targetWord}");
                streak = 0;
            }

            double averageAttempts = game.CalculateAverageAttempts(totalAttemptsUsed, totalGames);

            Console.WriteLine($"Votre streak actuelle est de : {streak}");
            Console.Write("Voulez-vous rejouer ? (o/n) : ");
            
            Console.WriteLine($"Nombre moyen de tentatives utilisées : {averageAttempts:F2}");
            playAgain = Console.ReadLine().Trim().ToLower() == "o";

        } while (playAgain);

        Console.WriteLine("\nMerci d'avoir joué ! Appuyez sur Entrée pour quitter...");
        Console.ReadLine();
    }
}
