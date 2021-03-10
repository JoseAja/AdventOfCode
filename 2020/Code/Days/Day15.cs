using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Aoc2020.Code.Days
{
    class Day15
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 15: Rambunctious Recitation       ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day15-Input.txt").Split(',');
            List<int> inputInts = GeneralFunctions.ToIntList(input);
            int numberSpoken = SpeakOptimized(2020, inputInts);
            Console.WriteLine("2020th number spoken: {0}", numberSpoken);
            numberSpoken = SpeakOptimized(30000000, inputInts);
            Console.WriteLine("30000000th number spoken: {0}", numberSpoken);
        }

        private int Speak(int numberOfNumbers, List<int> inputInts)
        {
            int numberSpoken = 0;

            try
            {
                if(inputInts.Count > 1)
                {
                    for (int i = 0; i < numberOfNumbers; i++)
                    {
                        if (i == 0)
                            numberSpoken = inputInts[i];
                        else
                        {
                            if (inputInts.Count - 1 < i)
                                numberSpoken = inputInts[i - 1];
                            else
                                numberSpoken = inputInts[i];

                            int LastIndexOfNumber = inputInts.FindLastIndex(inputInts.Count - 2, n => n == numberSpoken);

                            if (LastIndexOfNumber > -1 && LastIndexOfNumber < i - 1)
                                numberSpoken = i - 1 - LastIndexOfNumber;
                            else
                                numberSpoken = 0;

                            if (inputInts.Count - 1 < i)
                                inputInts.Add(numberSpoken);
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return numberSpoken;
        }

        private int SpeakOptimized(int numberOfNumbers, List<int> inputInts)
        {
            int numbertoSpeak = 0;

            try
            {
                if (inputInts.Count > 1)
                {
                    Dictionary<int, int> dictionary = new Dictionary<int, int>();
                    int lastNumberSpoken = 0;
                    for (int i = 0; i < numberOfNumbers; i++)
                    {
                        if (i == 0)
                        {
                            numbertoSpeak = inputInts[i];
                            dictionary.Add(numbertoSpeak, i);
                        }
                        else
                        {
                            if (inputInts.Count - 1 < i)
                                numbertoSpeak = lastNumberSpoken;
                            else
                                numbertoSpeak = inputInts[i];

                            if (dictionary.ContainsKey(numbertoSpeak))
                                numbertoSpeak = i - 1 - dictionary[numbertoSpeak];
                            else if (i > inputInts.Count - 1)
                                numbertoSpeak = 0;

                            if (dictionary.ContainsKey(lastNumberSpoken))
                                dictionary[lastNumberSpoken] = i-1;
                            else
                                dictionary.Add(lastNumberSpoken, i-1);

                            lastNumberSpoken = numbertoSpeak;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return numbertoSpeak;
        }
    }
}
