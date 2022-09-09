using System.Collections.Generic;

namespace WordFamilies
{
    /// <summary>
    /// Class used to store the 'families' of words based on
    /// a shared pattern determined by the guess taken by the user.
    /// It also determines the score for said family which is used
    /// to determine the best pattern to go forward with.
    /// </summary>
    public class Score
    {
        
        public List<Word> wordList = new List<Word>();
        public double total = 0;
        private double scoreOfList = 0;

        // word sample for a specific score 
        public string shape;
        // Class used to calculate a score to pick the best family/word
        public void CalculateScore()
        {
            

            for (int wrdIndx = 0; wrdIndx < wordList.Count; wrdIndx++)
            {
                int unicLrs = 0;
                int repetitiveLtrs = 0;

                for (int ltrIdx = 0; ltrIdx < 26; ltrIdx++)
                {
                    if (wordList[wrdIndx].frequency[ltrIdx] == 1 && LauncherEZ.lettersGuessed.Contains((char)wrdIndx) == false)
                        unicLrs++;
                    else if (wordList[wrdIndx].frequency[ltrIdx] > 1 && LauncherEZ.lettersGuessed.Contains((char)wrdIndx) == false)
                        repetitiveLtrs++;
                    
                }
                
                scoreOfList += unicLrs*0.7-(repetitiveLtrs*0.3);
            }
            double scoreOfLength = (double)wordList.Count / LauncherEZ.wordList.Count;
            scoreOfList /= wordList.Count;
            total = scoreOfLength * 0.6 + scoreOfList * 0.4;
        }
    }
}
