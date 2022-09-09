using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


using System.Timers;

namespace WordFamilies
{
    class LauncherHard
    {
        

        // the player can choose to play either the easy (E) or the hard (H) mode at the beginning of the game

        private static int nbrLtrsInWord;
        private static int nbrGuessesAtStart;
        public static char guess;
        public static string wordToGuess;
        

        public static List<string> wordFamily;
        public static Dictionary<char, int> correctLettersCount = new Dictionary<char, int>();

        public static Dictionary<char, int> nbreOfCorrectGuesses = new Dictionary<char, int>();
        public static List<string> wordsInDict;

        public static int posCount=1;

        private static int nbrGuessesMade;
        public static int nbrGuessesLeft => nbrGuessesAtStart - nbrGuessesMade;

        public static List<char> correctGuesses = new List<char>();
        public static List<char> wrongGuesses = new List<char>();


        
        public static List<char> guessesMade = new List<char>();

        // List of the correct guess attempts
        //

        private static string hiddenWord;

        

        // ////////////////
        public static void InitialiseGame()
        {

            //Clear variables
            correctGuesses.Clear();
            nbreOfCorrectGuesses.Clear();
            wrongGuesses.Clear();

            nbrGuessesMade = 0;
            
            //Get list of words from word dictionary
             wordsInDict = File.ReadAllLines("dictionary.txt").ToList();
            


            Random rand = new Random();
            nbrLtrsInWord = rand.Next(4, 13);
            nbrGuessesAtStart = nbrLtrsInWord * 2;

                //picks a random word from the dictionary with the random number of letter
                wordToGuess = PickRandWordFromDict(nbrLtrsInWord);
                correctGuesses.Clear();
                wrongGuesses.Clear();



                Console.Clear();
            int timetoguessInSec = nbrLtrsInWord * 10;
            Timer guessinTime = new Timer(nbrLtrsInWord * 10000);
            guessinTime.Elapsed += TimedGame;
            Console.WriteLine($"You have {timetoguessInSec}s to guess the hidden word");
            Console.WriteLine("Press a key to continy");
            Console.ReadKey();
            guessinTime.Start();


                do
                {
                    hiddenWord = HideWord();
                    StartGame();


                }
                while (nbrGuessesLeft > 0 && hiddenWord.Contains("-"));


                if (hiddenWord.Contains("-"))
                {
                    Console.WriteLine();
                    Console.WriteLine("\n You've ran out of time or guesses! \n AND YOU LOST AS EXPECTED HAHAHAAH! >:) \n the word was {0}", wordToGuess);
                }

                else

                {
                    Console.WriteLine("\n Congratulation you have won");
                }

            



        }

        private static void TimedGame(object Sender, ElapsedEventArgs e) 
        {
            nbrGuessesMade = nbrGuessesAtStart;


        }

        private static string HideWord()
        {
            return new string(wordToGuess.Select(x => correctGuesses.Contains(x) ? x : '-').ToArray());
        }


        // ////////////////
        private static void CheatNow()
        {
            //The rules the AI uses to change values
            var newWordFamily = MinMax.MinMaxxFam(wordToGuess, hiddenWord);

            if (newWordFamily == null) return;
            if (newWordFamily.Count() != 0)
            {
                wordToGuess = MinMax.newWordToGuessMM;
                wordFamily = newWordFamily.ToList();
                posCount = wordFamily.Count;
            }

        }

        // ////////////////
        private static string PickRandWordFromDict(int nbrOfLtrsInWord)
        {
            Random wordRandomizer = null;
            if (wordRandomizer == null)
                wordRandomizer = new Random();
            var wordsl = wordsInDict.Where(x => x.Length == nbrOfLtrsInWord).ToList();
            return wordsl[wordRandomizer.Next(0, wordsl.Count())];

            //Word of specified length has been found

        }



        private static void StartGame()
        {

            if (!hiddenWord.Contains("-"))
                return;
            Console.Clear();


            //

            Console.WriteLine("You are currently playing on " + Program.displayDif + " mode");
            Console.WriteLine();
            Console.WriteLine($"You have {nbrGuessesLeft} guesses left");
            Console.WriteLine($"Used letters: {string.Join(" ", wrongGuesses)}");
            Console.WriteLine($"Correct Guesses: {string.Join(" , ", correctGuesses)}");


            Console.WriteLine($"There are {posCount} words in the word family");

            Console.WriteLine("Word: " + hiddenWord);

            // for debugging
           // Console.WriteLine("Word: " + wordToGuess);
            do
            {

                Console.Write("Guess a letter by entering it: ");
                guess = Console.ReadKey().KeyChar;
            }
            while (!char.IsLetter(guess));


            if (wordToGuess.Contains(guess))
            {
                if (wordFamily != null && nbrGuessesLeft == 1 && wordFamily.Count > 1)
                {
                    wordToGuess = wordFamily.Where(x => x != wordToGuess).First();
                }
                else
                {
                    //if (displayDif == "Hard")
                     CheatNow();
                    
                  

                }

            }


            if (correctGuesses.Contains(guess) || wrongGuesses.Contains(guess))
            {
                Console.WriteLine("Do all humans have memory issues? You've guessed this letter already!");
            }
            else if (wordToGuess.Contains(guess))
            {
                nbreOfCorrectGuesses.Add(guess, wordToGuess.Count(x => x == guess));
                //Correct guess
                correctGuesses.Add(guess);
                guessesMade.Add(guess);

                //Update number of possible alternatives
                posCount = wordFamily.Count() + 1;
            }
            else
            {
                wrongGuesses.Add(guess);
                guessesMade.Add(guess);
                nbrGuessesMade++;
            }


        }


    }




}
