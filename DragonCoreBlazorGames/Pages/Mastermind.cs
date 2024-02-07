namespace DragonCoreBlazorGames.Pages;

using System;
using System.Collections.Generic;
using System.Linq;

public class MastermindGame
{
    public const int CodeLength = 4;
    public const int ColorCount = 6;
    private readonly int[] secretCode;
    private readonly Random random;

    public MastermindGame()
    {
        random = new Random();
        secretCode = GenerateSecretCode();
    }

    public (int correctPosition, int correctColor) CheckGuess(int[] guess)
    {
        if (guess.Length != CodeLength)
        {
            throw new ArgumentException($"Guess must have {CodeLength} elements.");
        }

        int correctPosition = 0;
        int correctColor = 0;

        var secretCodeColorCounts = new Dictionary<int, int>();
        var guessColorCounts = new Dictionary<int, int>();

        for (int i = 0; i < CodeLength; i++)
        {
            if (guess[i] == secretCode[i])
            {
                correctPosition++;
            }
            else
            {
                if (!secretCodeColorCounts.ContainsKey(secretCode[i]))
                {
                    secretCodeColorCounts[secretCode[i]] = 0;
                }
                secretCodeColorCounts[secretCode[i]]++;

                if (!guessColorCounts.ContainsKey(guess[i]))
                {
                    guessColorCounts[guess[i]] = 0;
                }
                guessColorCounts[guess[i]]++;
            }
        }

        foreach (var key in guessColorCounts.Keys.Intersect(secretCodeColorCounts.Keys))
        {
            correctColor += Math.Min(guessColorCounts[key], secretCodeColorCounts[key]);
        }

        return (correctPosition, correctColor);
    }

    private int[] GenerateSecretCode()
    {
        var code = new int[CodeLength];
        for (int i = 0; i < CodeLength; i++)
        {
            code[i] = random.Next(1, ColorCount + 1);
        }
        return code;
    }
}
