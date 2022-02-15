/*
Copyright 2022 Sreekar Bheemavarapu

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WordleGuesser
{
    static class Guesser
    {
        private static List<int> IndexesOf(string word, char c)
        {
            List<int> ret = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == c)
                    ret.Add(i);
            }
            return ret;
        }

        private static int AmountOf(string word, char c)
        {
            int ret = 0;
            foreach (var x in word)
            {
                if (x == c)
                    ret++;
            }
            return ret;
        }

        public static double GetScore(string guess, string answer)
        {
            double score = 0;
            for (int i = 0; i < 5; i++)
            {
                if (guess[i] == answer[i])
                    score += 2.5;
                else
                {
                    var ind = answer.IndexOf(guess[i]);
                    if (ind != -1)
                    {
                        var temp = answer.ToCharArray();
                        temp[ind] = '0';
                        answer = new string(temp);
                        score++;
                    }
                }
            }
            return score;
        }

        //[MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static double AverageInfo(string[] possibleWords, string wordToCheck)
        {
            double sum = 0;

            foreach (var elem in possibleWords)
            {
                sum += GetScore(wordToCheck, elem);
            }
            /*
            for (int j = 0; j < possibleWords.Length; j++)
            {
                for (int i = 0; i < 243; i++)
                {
                    if (MatchesCond(possibleWords[j], wordToCheck, new int[] { (i / 81) % 3, (i / 27) % 3, (i / 9) % 3, (i / 3) % 3, i % 3 }))
                        sum++;
                }
            }
            */

            return sum / possibleWords.Length;
        }

        // Guess is the player's guess for the turn, word is the word being checked to see if it matches the condition.
        public static bool MatchesCond(string word, string guess, int[] guessVals)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (guessVals[i])
                {
                    case 0:
                        if (AmountOf(word, guess[i]) >= AmountOf(guess, guess[i]))
                            return false;
                        break;
                    case 1:
                        if (!word.Contains(guess[i]))
                        {
                            return false;
                        }
                        if (word[i] == guess[i])
                        {
                            return false;
                        }
                        break;
                    case 2:
                        if (word[i] != guess[i])
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        public static string[] Guess(string[] words, int[] vals)
        {
            var temp = (string[])AllWords.allWords.Clone();
            temp = temp.Where(x => !words.Contains(x)).ToArray();
            string[] possibleWords = AllWords.allWords;

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToUpper();
            }

            for (int i = 0; i < words.Length; i++)
            {
                possibleWords = possibleWords.Where(x => MatchesCond(x, words[i], new int[5] { vals[i * 5], vals[i * 5 + 1], vals[i * 5 + 2], vals[i * 5 + 3], vals[i * 5 + 4] })).ToArray();
            }

            Dictionary<string, double> valuePairs = new Dictionary<string, double>(temp.Length);
            foreach (string word in temp)
                valuePairs.Add(word, AverageInfo(possibleWords, word));

            Array.Sort(temp, (x, y) => valuePairs[y].CompareTo(valuePairs[x]));
            for (int i = 0; i < temp.Length; i++)
                temp[i] += ": " + valuePairs[temp[i]].ToString("F3");
            return temp;
        }
    }
}
