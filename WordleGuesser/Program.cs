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

namespace WordleGuesser {
    class Program
    {
        public static T[] ArraySubset<T>(T[] src, int first, int last)
        {
            T[] ret = new T[last - first];
            Array.Copy(src, first, ret, 0, last - first);
            return ret;
        }

        static void Main()
        {
            string[] words = { "", "", "", "", "", "" };
            int[] vals = new int[30];
            Array.Fill(vals, -1);
            int j = 0;
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(string.Format("Enter guess {0}: ", i + 1));
                words = words.Append(Console.ReadLine()!).ToArray();
                Console.WriteLine(string.Format("Enter values for guess {0}: ", i + 1));
                //vals = vals.Append(new int[5]).ToArray();
                for (int k = j; k < 5 + j; k++)
                {
                    vals[k] = int.Parse(Console.ReadLine()!);
                }
                j += 5;
                Array.ForEach(ArraySubset(Guesser.Guess(words.Where(x => x != "").ToArray(), vals.Where(x => x != -1).ToArray()), 0, 5), x => Console.WriteLine(x));
            }
        }
    }
}

