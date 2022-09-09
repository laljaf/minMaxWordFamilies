using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WordFamilies
{
    public static class Program
    {

        public static string displayDif;




        private static char difficulty;
       


       
        //List for the family of the word to guess
        public static List<string> wordFamily;



        static void Main(string[] args)
        {

            PrintStartMessage();
            
           

            if (!SetDifficultyHard()) 
              LauncherEZ.LaunchEasy();
            else
            LauncherHard.InitialiseGame();
            
            PlayAgain();
            Console.ReadKey();

        }

        public static void PrintStartMessage()
        {

            //A welcome message for the player
            Console.WriteLine("Welcome to Guess the word game \n");
            Console.WriteLine("A word with a random length will be chosen out of the words in the dictionary");
            Console.WriteLine("Your duty is to find the word, one letter at a time without exhausting all your chances");
            Console.WriteLine("The number of chances you are offered is equal to double the length of the words (8 chances for a 4 letters word fe) \n");
            Console.WriteLine("Try to not get too frustrated when you lose, cause you will, NAIVE HUMAN! >:) \n");
            Console.WriteLine("cough...cough... I mean, I'M A ROBOT \n");
            Console.WriteLine("HAVE FUN \n");
            Console.WriteLine("PRESS A KEY TO CONTINUE \n");
            Console.WriteLine("BIP BOUP \n");
            Console.WriteLine("'-'\n");

            Console.ReadKey();
            //Clear Console
            Console.Clear();
        }



        // ////////////////
        private static bool SetDifficultyHard()
        {
            bool isHard = false;


            Console.WriteLine("\n Please choose a difficulty (E for easy or H for Hard)");

            difficulty = Console.ReadKey().KeyChar;
            difficulty = char.ToUpper(difficulty);


            while (difficulty != 'E' && difficulty != 'H')
            {

                Console.Clear();
                Console.WriteLine("\n Please enter (E for easy or H for Hard)  ");
                difficulty = Console.ReadKey().KeyChar;
                difficulty = char.ToUpper(difficulty);
            }


            if (difficulty == 'H')
            {
                isHard = true;
                displayDif = "Hard";
            }

            else displayDif = "Easy";

            return isHard;

        }

        // ////////////////
        


        // function to offer the user the possibility of playing again
        public static void PlayAgain()
        {
            Console.WriteLine("\n Are you interested in another round? :) ");
            Console.Write("Y for N for No ?");

            char pAgainChoice = Console.ReadKey().KeyChar;

            pAgainChoice = char.ToUpper(pAgainChoice);
            if (pAgainChoice != 'Y' && pAgainChoice != 'N')
            {
                Console.WriteLine("\n It's Y or N! ");
                PlayAgain();
            }
            else if (pAgainChoice == 'Y')
            {

                if (!SetDifficultyHard())
                    LauncherEZ.LaunchEasy();
                else
                    LauncherHard.InitialiseGame();

                PlayAgain();

            }
            else if (pAgainChoice == 'N')
            {
                Console.Clear();
                Console.WriteLine("See you soon :)))!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();

            }

        }

        // ////////////////




        public static void LoadDict()
        {
            //Get list of words from word dictionary
            LauncherHard.wordsInDict = File.ReadAllLines("dictionary.txt").ToList();
        }





        public static string PickRandWordFromDict(int nbrOfLtrsInWord)
        {
            LoadDict();
            Random wordRandomizer = null;
            if (wordRandomizer == null)
                wordRandomizer = new Random();
            var wordsL = LauncherHard.wordsInDict.Where(x => x.Length == nbrOfLtrsInWord).ToList();
        
            return wordsL[wordRandomizer.Next(0, wordsL.Count())];
            
        }


        // ////////////////








    }
    
}


