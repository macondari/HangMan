using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // Function to print hangman based on wrong guesses
    private static void PrintHangman(int wrongGuesses)
    {
        //format hangman
        Console.WriteLine(" _____");
        Console.WriteLine(" |   |");

        if (wrongGuesses >= 1)
            Console.WriteLine(" |   O");
        else
            Console.WriteLine(" |");

        if (wrongGuesses == 2)
            Console.WriteLine(" |   |");
        else if (wrongGuesses == 3)
            Console.WriteLine(" |  \\|");
        else if (wrongGuesses >= 4)
            Console.WriteLine(" |  \\|/");
        else
            Console.WriteLine(" |");

        if (wrongGuesses == 5)
            Console.WriteLine(" |  /");
        else if (wrongGuesses >= 6)
            Console.WriteLine(" |  / \\");
        else
            Console.WriteLine(" |");

        Console.WriteLine("_|___");
    }

    // Function to print the word with guessed letters
    private static void PrintWord(List<char> guessed, string randomWord)
    {
        foreach (char letter in randomWord)
        {
            if (guessed.Contains(letter))
                Console.Write(letter + " ");
            else
                Console.Write("_ ");
        }
        Console.WriteLine();
    }
    // Function to check if a letter is in the word
    private static bool CheckLetter(string word, char guess)
    {
        return word.Contains(guess);
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Hangman!");
        Console.WriteLine("Rules: Guess the letters of the word. You have up to 6 wrong guesses before the game ends.");
        Console.WriteLine("Let's start!\n");

        Random random = new Random();
        List<string> dictionary = new List<string>();

        // Read words from file into dictionary
        string line;
        StreamReader file = new StreamReader(@"C:\Users\micha\Desktop\GitHub\HangMan\HangMan\Words.txt");
        while ((line = file.ReadLine()) != null)
        {
            dictionary.Add(line);
        }
        file.Close();

        // Select a random word from dictionary I created
        int choose = random.Next(dictionary.Count);
        string word = dictionary[choose].ToUpper(); // Keep word as uppercase for consistency

        List<char> guessed = new List<char>();
        int wrongGuesses = 0;

        while (wrongGuesses < 6)
        {
            Console.WriteLine();
            PrintHangman(wrongGuesses);
            PrintWord(guessed, word);

            Console.Write("\nLetters guessed so far: ");
            foreach (char letter in guessed)
            {
                Console.Write(letter + " ");
            }

            Console.Write("\nGuess a letter: ");
            char guessLetter = Char.ToUpper(Console.ReadLine()[0]); // Convert input to uppercase

            // Check if letter has already been guessed
            if (guessed.Contains(guessLetter))
            {
                Console.WriteLine("You have already guessed '{0}'. Guess a new letter.", guessLetter);
                continue;
            }

            guessed.Add(guessLetter);

            // Check if letter is in the word
            if (CheckLetter(word, guessLetter))
            {
                Console.WriteLine("Correct guess: '{0}' is in the word!", guessLetter);
            }
            else
            {
                Console.WriteLine("Incorrect guess: '{0}' is not in the word.", guessLetter);
                wrongGuesses++;
            }

            // Check if all letters have been guessed
            bool allLettersGuessed = true;
            foreach (char letter in word)
            {
                if (!guessed.Contains(letter))
                {
                    allLettersGuessed = false;
                    break;
                }
            }

            if (allLettersGuessed)
            {
                Console.WriteLine("\nCongratulations! You guessed the word '{0}' correctly!", word);
                break;
            }
        }

        if (wrongGuesses >= 6)
        {
            Console.WriteLine("\nSorry, you did not guess the word '{0}'. Game Over.", word);
            PrintHangman(wrongGuesses);
        }

        Console.WriteLine("\nThanks for playing Hangman!");
    }
}
