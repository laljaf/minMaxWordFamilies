using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordFamilies
{ 
    // class that uses the minmax algo for the creation of a family
    public static class MinMax
    {






        public static string newWordToGuessMM = "";   // the best choice for the word


        



        public static IEnumerable<string> MinMaxxFam(string word2g, string hidenWord)
        {


            var mMFam = FamilyFormM.CreateFamForMinMax( word2g,  hidenWord);


            // value that reduces family the most
            int famLowesScr = -1;
            newWordToGuessMM = "";
            Parallel.ForEach
              (
                mMFam, wrd =>
                {
                lock (newWordToGuessMM)
                {
                        //encrypt new word to guess encrypted by replacing '-' with previously hidden character
                        var newWordMask = new string(wrd.Select(x => LauncherHard.correctGuesses.Contains(x) ? x : '-').ToArray());

                    //number of words would in the new word to guess family
                    int nbrOfWrdsInFam = FamilyFormM.FindLikeWord(hidenWord, wrd).Count();

                    if (famLowesScr == -1 || nbrOfWrdsInFam < famLowesScr)
                    {
                        famLowesScr = nbrOfWrdsInFam;
                        newWordToGuessMM = wrd;
                    }
                }
           
                }
                 ) ;


            return mMFam;
        }


        
       
        
     
     


       



    }

}