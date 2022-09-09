using System;
using System.Collections.Generic;

namespace WordFamilies
{
    public static class EzAi
    {
       // serves as a main method for the easy mode
        public static void Lauch(char guess)
        {
            // generates the families used for the easy mode
            Score result = CreateEasyFamilies(guess, LauncherEZ.wordList);

            LauncherEZ.wordList = result.wordList;

            // hide the word accordingly 
            for (int i = 0; i < result.shape.Length; i++)
            {
                if (result.shape[i] != '-' && LauncherEZ.hiddeEznWord[(2 * i + 1)] == '_')
                    LauncherEZ.hiddeEznWord[(2 * i + 1)] = result.shape[i];
            }

        }

        // creates families of "Word" according to the total score calculate in the calculate score
        private static Score CreateEasyFamilies(char letter, List<Word> wordList)
        {
            // generate a list of words sorted according the the 
            SortedList<string, Score> wordFamilies = new SortedList<string, Score>();

            // find the shape of the word
            foreach (Word word in wordList)
            {
                string nShape = "";

                for (int i = 0; i < word.nbrLtrsInWrd; i++)
                {
                    if (word.word[i] == letter)
                        nShape += letter;
                    else
                    {
                        nShape += '-';
                    }
                }

                
                if (wordFamilies.ContainsKey(nShape)) // adds the words to families by comparing their shapes: shape matches= same fam, else+ new fam
                {
                    wordFamilies[nShape].wordList.Add(word);
                }

                
                else
                {
                    wordFamilies.Add(nShape, new Score { wordList = new List<Word> { word }, shape = nShape });
                }

            }

          
            Score BestScoreFamily = new Score(); //family with the best score

           
            foreach (string key in wordFamilies.Keys)
            {
               
                wordFamilies[key].CalculateScore(); //calculates word's score

                
                if (wordFamilies[key].total > BestScoreFamily.total) // compares families total score and keeps the best
                    BestScoreFamily = wordFamilies[key];

                /* uncomment to see the details of the created word families 
                       if (wordFamilies[key].wordList.Count != 0)
                       Console.WriteLine("Shape = {0} nbre of words {1} Score {2}", wordFamilies[key].shape, wordFamilies[key].wordList.Count, wordFamilies[key].total.ToString("0.##"));
                */

            }

            
            /* uncomment to see the details of the family with the best score
             Console.WriteLine("\n Shape = {0} nbre of words {1} Score {2}", BestScoreFamily.shape, BestScoreFamily.wordList.Count, BestScoreFamily.total.ToString("0.##"));
             Console.ReadKey();
            */

             return BestScoreFamily;

             
        }
    }
}
