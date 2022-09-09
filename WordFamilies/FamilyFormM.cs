using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFamilies
{ 
    // a class for word Families sed for the MinMax Algorithm
    class FamilyFormM
    {

        public static string newWrdToGuess;

        public static IEnumerable<string> CreateFamForMinMax(string word2g, string mask)
        {

            //Use the dictionary as word family if word family is empty
            var wordList = Program.wordFamily;
            if (wordList == null)
                wordList = LauncherHard.wordsInDict;

            //create a word family for min max
            var wordFamilies = CreateFam(word2g, mask);

            // returns null if empty
            if (wordFamilies.Count() == 0) return null;

            //Get word family with the largest count
            var largestFam = wordFamilies.First();
            if (largestFam.Count() == 0)
                return null;
            return largestFam;

        }


      

            public static bool matchWord(string word) // check if the word has got no incorrect chars
        {
            bool output = true;

            
            foreach (var wg in LauncherHard.wrongGuesses)
            {
                output &= !word.Contains(wg);
            }

           
            foreach (var keyValuePair in LauncherHard.nbreOfCorrectGuesses)  // the correct guesses have the same index
            {
                output &= word.Count(w => w == keyValuePair.Key) == keyValuePair.Value;
            }
            return output;


        }

        public static IEnumerable<string> FindLikeWord(string hidWrd, string wrd2G) // gets words with the same show letters and dashes
        {
            //Use listOfWords as word2g family if word2g family is uninitalized
            var wordList = Program.wordFamily;
            if (wordList == null)
                wordList = LauncherHard.wordsInDict;

            //Get all the words matching word2g encrypted
            return wordList.Where(word => word.Length == hidWrd.Length && word != wrd2G && CompareDashPos(word, hidWrd) && matchWord(word));
        }


        // check if word2g matches word2g encrypted

        public static bool CompareDashPos(string word, string dahes)   // check if the word == the number of ("-") 
        {
            //Double-tape turing machine
            if (word.Length != dahes.Length) throw new Exception("Word and Mask are not the same length");

            bool compareMatchPos = true;
            for (int i = 0; i < dahes.Length; i++)
            {
                if (dahes[i] != '-') compareMatchPos &= dahes[i] == word[i];
            }

            return compareMatchPos;
        }


        public static IEnumerable<IEnumerable<string>> DivideIntoFam(string wrd2g, string mAsk)
        {

            //Use listOfWords as word2g family if word2g family is uninitalized
            var wordList = Program.wordFamily;
            if (wordList == null)
                wordList = LauncherHard.wordsInDict;


            var wordFamilies = FamilyFormM.CreateFam(wrd2g, mAsk);
            if (wordFamilies.Count() == 0) return null;
            return wordFamilies;
        }


        public static IEnumerable<IEnumerable<string>> CreateFam(string word, string encrypted) // create new families for cheating 
        {
            //Use listOfWords as word2g family if word2g family is uninitalized
            var listOfWords = Program.wordFamily;
            if (listOfWords == null)
                listOfWords = Program.wordFamily;

            List<IEnumerable<string>> output = new List<IEnumerable<string>>();

            //For the hidden letters
            List<char> hidden = new List<char>();
            for (int i = 0; i < word.Length; i++)
            {
                if (encrypted[i] == '-')
                    hidden.Add(word[i]);
            }

            //Get all word2g families
            Parallel.ForEach(hidden, hiddenLetter =>
            {
                lock (output)
                {
                    string encrypption = "";
                    for (int i = 0; i < encrypted.Length; i++)
                    {
                        if (word[i] == hiddenLetter)
                            encrypption += hiddenLetter.ToString();
                        else
                            encrypption += encrypted[i].ToString();
                    }
                    output.Add(FamilyFormM.FindLikeWord(encrypption, word));
                }
            });
            return output.OrderByDescending(x => x.Count());
        }


    }
}
