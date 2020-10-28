using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    class Program
    {   
        static string[] defaultWords = {"Baseball", "Armstrong", "Reptile", "Catapult", "Toaster", "Beaver" };
        static StringBuilder stringVisual = new StringBuilder();
        static int numGuesses = 0;
        static int totGuessesRemaining = 5;
        static string hiddenWord;
        static void Main(string[] args)
        {
            AskForNewGame();
        }

        //function that deals with asking the player if they want to play again
        static void AskForNewGame()
        {
            string answer;
            do
            {
                Console.WriteLine("Would You like to start a new game? Y/N");
                answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    StartGame();
                }
                else if (answer.ToLower() == "n")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Wrong Response! Try again!");
                }
            } while (answer.ToLower() != "y" || answer.ToLower() != "n");
        } 

        //function that deals with the actual game
        static void StartGame()
        {
            //Generate the hidden word randomly
            Random randNums = new Random();
            numGuesses = 0;
            int randIndex = randNums.Next(defaultWords.Length);
            hiddenWord = defaultWords[randIndex];
            bool hasGuessedCorrect = false;
            //REMOVE THIS AFTER WE ARE DONE!
            //Console.WriteLine(hiddenWord);
            stringVisual.Clear();
            for(int i = 0; i<defaultWords.Length; i++)
            {
                stringVisual.Append("_");
            }
            while((numGuesses < totGuessesRemaining) && !hasGuessedCorrect)
            {
                TotalVisualInformation(numGuesses);
                Console.WriteLine($"You have {totGuessesRemaining - numGuesses} guesses remaining");
                string answer = Console.ReadLine();
                if(answer.Length == 1 )
                {                
                    if (hiddenWord.ToLower().Contains(answer))
                    {
                        WordInformation(answer);
                        Console.WriteLine($"The word does contain the letter {answer}! Well done!");
                    }
                    else
                    {
                        WordInformation(answer);
                        Console.WriteLine($"The word doesn't contain the letter {answer}!");
                        numGuesses++;
                    }
                }
                else if(answer.Length > 1)
                {
                    if (hiddenWord.ToLower() == answer.ToLower())
                    {
                        Console.WriteLine($"The word is {answer}! Well done!");
                        hasGuessedCorrect = true;
                    }
                    else
                    {
                        Console.WriteLine($"The word is not {answer}!");
                        numGuesses++;
                    }
                }
                else
                {
                    Console.WriteLine("You need to guess either a letter or a word!");
                }
            }
            EndOfGame(hasGuessedCorrect);
        }
        //end of game function ends the game and asks the player if they want to play again
        static void EndOfGame(bool isWinner)
        {
            TotalVisualInformation(numGuesses);
            if (isWinner)
            {
                Console.WriteLine($"Congratulations! You won with only {totGuessesRemaining - numGuesses} guesses remaining!");
            }
            else
            {
                Console.WriteLine($"Sorry, the correct word was {hiddenWord}. Better luck next time!");
            }
            AskForNewGame();
        }

        //Word information contains the data needed to output the letters that have been found as well as their location in hidden word
        static StringBuilder WordInformation(string letter)
        {
            bool containsLetter = hiddenWord.ToLower().Contains(letter);
            if (containsLetter)
            {
                for (int i = 0; i < hiddenWord.Length; i++)
                {
                    if (hiddenWord[i].ToString().ToLower() == letter.ToLower())
                    {
                        //Console.WriteLine("new shit!");
                        stringVisual.Replace("_", letter,i, 1);
                    }
                }
            }
            return stringVisual;
            
        }
        //Function that physically shows the hangman
        static void VisualHangman(int wrongguesses)
        {
            //Switch statement to show the hangman visually
            switch (wrongguesses)
            {
                case 0:
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    break;
                case 1:
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    Console.WriteLine("        ");
                    Console.WriteLine("________");
                    break;
                case 2:
                    Console.WriteLine("      | ");
                    Console.WriteLine("      | ");
                    Console.WriteLine("      | ");
                    Console.WriteLine("______|_");
                    break;
                case 3:
                    Console.WriteLine("  ____  ");
                    Console.WriteLine("      | ");
                    Console.WriteLine("      | ");
                    Console.WriteLine("      | ");
                    Console.WriteLine("______|_");
                    break;
                case 4:
                    Console.WriteLine("  ____  ");
                    Console.WriteLine(" |    | ");
                    Console.WriteLine(" O    | ");
                    Console.WriteLine(" |    | ");
                    Console.WriteLine("______|_");
                    break;
                case 5:
                    Console.WriteLine("  ____  ");
                    Console.WriteLine(" |    | ");
                    Console.WriteLine("_O_   | ");
                    Console.WriteLine("_|_   | ");
                    Console.WriteLine("______|_");
                    break;
            }
        }
        static void TotalVisualInformation(int wrongguesses)
        {
            VisualHangman(wrongguesses);
            Console.WriteLine($"Word:{stringVisual.ToString()}");
        }
    }
}
