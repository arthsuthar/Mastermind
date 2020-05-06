using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maximumNumberOfGuesses = 10;
            const int maximumDigitValue = 6;
            const int minimumDigitValue = 1;
            const int maximumDigitLength = 4;

            string guess;
            int numberOfGuesses = 0;

            DisplayInstructions(maximumNumberOfGuesses, maximumDigitValue, minimumDigitValue, maximumDigitLength);

            var codeToGuess = GenerateCode(minimumDigitValue, maximumDigitValue, maximumDigitLength);

            while ((numberOfGuesses < maximumNumberOfGuesses))
            {
                Console.WriteLine($"You have {maximumNumberOfGuesses - numberOfGuesses} guesses left. What is your guess?");

                guess = Console.ReadLine();
                guess = guess.Trim();

                if (!IsGuessValidCode(guess))
                {
                    Console.WriteLine($"{guess} is NOT a valid code! Please look at the rules for what makes up a valid guess.");
                }
                else
                {
                    var formattedGuess = FormatGuess(guess);

                    DisplayCodeClue(formattedGuess, codeToGuess);

                    if (IsGuessCorrect(guess, codeToGuess))
                    {
                        numberOfGuesses = 11;
                        Console.WriteLine("You guessed the code correctly! You win!");
                    }
                    else
                    {
                        numberOfGuesses++;
                    }
                }
            }

            if (numberOfGuesses == maximumNumberOfGuesses)
            {
                Console.WriteLine($"You couldn't guess the code in {maximumNumberOfGuesses} guesses. The code was {string.Join("", codeToGuess)}. Game over.");
            }

            Console.ReadLine();
        }

        private static void DisplayCodeClue(int[] formattedGuess, int[] codeToGuess)
        {

            for (int i = 0; i < formattedGuess.Length; i++)
            {
                if (codeToGuess[i] == formattedGuess[i])
                {
                    Console.Write("+");
                }
                else if (codeToGuess.Contains(formattedGuess[i]))
                {
                    Console.Write("-");
                }
            }

            Console.WriteLine();
        }

        private static bool IsGuessCorrect(string guess, int[] codeToGuess)
        {
            return guess == string.Join("", codeToGuess);
        }

        private static int[] FormatGuess(string guess)
        {
            var convertedGuess = guess.ToCharArray();
            return Array.ConvertAll(convertedGuess, c => (int)char.GetNumericValue(c));
        }

        public static bool IsGuessValidCode(string guess)
        {            
            if (guess.Length != 4)
            {
                return false;
            }

            Regex validPattern = new Regex("[1-6]{4}");
            return validPattern.IsMatch(guess);
        }

        private static int[] GenerateCode(int minimumDigitValue, int maxmimumDigitValue, int maximumDigitLength)
        {
            var randomNumber = new Random();
            int[] code = Enumerable
                    .Repeat(0, maximumDigitLength)
                    .Select(i => randomNumber.Next(minimumDigitValue, maxmimumDigitValue))
                    .ToArray();

            return code;
        }

        private static void DisplayInstructions(int maximumNumberOfGuesses, int maximumDigitValue, int minimumDigitValue, int maximumDigitLength)
        {
            Console.WriteLine($"Welcome to simple mastermind! I have created a {maximumDigitLength} digit code.");
            Console.WriteLine("Your job is to guess the code!");
            Console.WriteLine($"The individual digits will only have values of {minimumDigitValue} through {maximumDigitValue}.");
            Console.WriteLine("The rules:");
            Console.WriteLine($"1. Enter a {maximumDigitLength} digit code");
            Console.WriteLine($"2. The code must contain only the numbers {minimumDigitValue} through {maximumDigitValue}");
            Console.WriteLine("3. I will display a minus (-) if a digit is correct but in the incorrect position");
            Console.WriteLine("4. I will display a plus (+) if a digit is correct and in the correct position");
            Console.WriteLine($"5. You win if you guess my code within {maximumNumberOfGuesses} guesses" + Environment.NewLine);
        }

    }
}
