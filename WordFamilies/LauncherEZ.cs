using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WordFamilies
{
    class LauncherEZ
    {
        

        // Variable used to display the dashes and letters on the screen
        public static StringBuilder hiddeEznWord = new StringBuilder();

        // stores words in dictionary
        public static string[] wrdsInDicti = File.ReadAllLines("dictionary.txt");

       
        public static int nbrLtrsInWord;
        public static int nbrOfGuesses;
      

        // stores words left in the fam
        public static List<Word> wordList = new List<Word>();

        // Variable that holds all guesses already taken by the user
        public static List<char> lettersGuessed = new List<char>();

        // 0 ongoing, 1 player lost, 2 player won
        public static int stateOfGame = 0;


        public static void LaunchEasy()
        {
         

         
            InitialiseVar();

           

            

            // After gathering the user input the program proceedes into the algorithm
            StartGameEz();

            Console.Read();
        }



        private static void InitialiseVar()
        {

            // randomise the nbr of letters in the word to guess
            // creating an object of class word to calcute the score





                Random rand = new Random();
                nbrLtrsInWord = rand.Next(4, 13);
                nbrOfGuesses = 2 * nbrLtrsInWord;
               


            foreach (var word in wrdsInDicti)
                    {
                        
                        Word wrd = new Word(word);

                        if (wrd.nbrLtrsInWrd == nbrLtrsInWord)
                        {
                            wordList.Add(wrd);

                           
                        }
            }


                   
                



           

            // Initialises the display of the word based on the lenght
            for (int i = 0; i < nbrLtrsInWord; i++)
            {
                hiddeEznWord.Append(" _");
            }
        }

       


        
        private static void StartGameEz()
        {
            // the game is ongoin whil the player has guesses left or the all the letters were revealed
            while (nbrOfGuesses != 0 && hiddeEznWord.ToString().Contains('_') == true)
            {
                ShowInterface();

                // Retains initial display
                string initial = hiddeEznWord.ToString();

                // Retains initial letters guessed 
                int letterCount = lettersGuessed.Count;


                if (nbrOfGuesses <= 0)
                {
                    Console.WriteLine("HAHAHA! YOU LOST AS EXPECTED OF A HUMAN");
                    stateOfGame = 1;
                    break;
                }
                else if (hiddeEznWord.ToString().Contains('_') == false)
                {
                    Console.WriteLine("Congrats..... You have won human :/");
                    stateOfGame = 2;
                    break;
                }

                
                EzAi.Lauch(VerifyGuess());

                // if all the characters are still hidden, the user loses a guess
                if (initial == hiddeEznWord.ToString() && lettersGuessed.Count != letterCount)
                    nbrOfGuesses--;

            }

            Console.Clear();
            ShowInterface();

            // when the game ends:
            switch (stateOfGame)
            {
                case 1:
                    Console.WriteLine("You lost. No more tries left.");
                    Random random = new Random();

                    // displays a random word from the remaining list
                    Console.WriteLine("The word was {0}. Good luck next time!", wordList[random.Next(wordList.Count)].word);
                    return;

                case 2:
                    Console.WriteLine("You have won! Congratulations!");
                    return;
            }

            Console.ReadKey();

        }

      
        // used to make sure the user has entered a letter and it's not the same as the previous ones 
        private static char VerifyGuess()
        {
            // true if the playr's input is a new letter
            bool isNewLtr = false;
            char input = ' ';

            while (isNewLtr == false)
            {
                Console.Write("Guess a letter by entering it: ");

                input = Console.ReadKey().KeyChar;
                input = char.ToLower(input);

                
                if (input >= 97 && input <= 122 && lettersGuessed.Contains(input) == false)
                {
                    isNewLtr = true;
                }

                // letr has already been guessed or is not a letter
                else
                {
                    Console.Clear();
                    ShowInterface();
                    Console.WriteLine("CHECK YOUR INPUT HUMAN! Enter a letter that hasn't been used yet!");
                }
            }
            
            lettersGuessed.Add(input); // inserted in the used letters when its a new letter
            return input;
        }


       // display
        private static void ShowInterface()
        {
            Console.Clear();
            Console.WriteLine(" You are currently playing on " + Program.displayDif + " mode");
            Console.WriteLine($"\n You have {nbrOfGuesses} guesses left");
           
            Console.Write(" Used Letters: ");

            for (int i = 0; i < lettersGuessed.Count; i++)
            {
                Console.Write(lettersGuessed[i] + " , ");
            }
            Console.WriteLine("\n Word: " + hiddeEznWord);

            Console.WriteLine($" There are {wordList.Count} words in the word family");
        }
    }
}
