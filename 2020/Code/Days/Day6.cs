using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aoc2020.Code.Days
{
    class Day6
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 6: Custom Customs                ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day6-Input.txt");
            int[] yesSum = CountYes(input);
            Console.WriteLine("Sum of yes: {0}", yesSum[0]);
            Console.WriteLine("Sum of everyone yes: {0}", yesSum[1]);
        }

        private int[] CountYes(string[] input)
        {
            int[] count = new int[] { 0, 0};
            try
            {
                string groupYesAnswers = "";
                string groupYesEveryoneAnswers = "";
                bool firstInGroup = true;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Length != 0)
                    {
                        if (firstInGroup)
                        {
                            groupYesAnswers = input[i];
                            groupYesEveryoneAnswers = input[i];
                            firstInGroup = false;
                        }
                        else
                        {
                            groupYesAnswers = String.Concat(groupYesAnswers.Union(input[i]));
                            groupYesEveryoneAnswers = String.Concat(groupYesEveryoneAnswers.Intersect(input[i]));
                        }
                    }

                    if (input[i].Length == 0 || i == (input.Length - 1))
                    {
                        count[0] += groupYesAnswers.Length;
                        count[1] += groupYesEveryoneAnswers.Length;
                        groupYesAnswers = "";
                        groupYesEveryoneAnswers = "";
                        firstInGroup = true;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return count;
        }
    }
}