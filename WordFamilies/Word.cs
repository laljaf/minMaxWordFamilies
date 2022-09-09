using System;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    // a word class that uses asci code to hold the frequency of a letter in this word
    public class Word
    {
       
        public string word;
        public int nbrLtrsInWrd;

        // the frequency of the letter in the word
        public int[] frequency = new int[26];


        public Word(string wrd)
        {
            word = wrd;
            nbrLtrsInWrd = wrd.Length;

            for (int i = 0; i < wrd.Length; i++)
            {
                frequency[wrd[i] - 97]++;

            }
        }
    }
}
