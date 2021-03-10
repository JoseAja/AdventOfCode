using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Code.Days
{
    class Day10
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 10: Adapter Array                ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day10-Input.txt");
            List<int> intInput = GeneralFunctions.ToIntList(input);
            intInput.Sort();
            Console.WriteLine("Device's built-in joltage adapter is rated: {0}", intInput.Last() + 3);
            intInput.Add(intInput.Last() + 3);
            Dictionary<int, int> differences = CalculateDifferences(intInput);
            Console.WriteLine("Number of 1-jolt differences multiplied by the number of 3-jolt differences: {0}", differences[1] * differences[3]);
            //int total = PrintArrangements(intInput);
            //Console.WriteLine("Total ways: {0}", total);
            GetArrangements(intInput);
        }

        public Dictionary<int, int> CalculateDifferences(List<int> input)
        {
            Dictionary<int, int> differences = new Dictionary<int, int>();
            try
            {
                int effectiveRating = 0;
                for (int i = 0; i < input.Count; i++)
                {
                    int currentDifference = input[i] - effectiveRating;
                    if (differences.ContainsKey(currentDifference))
                        differences[currentDifference]++;
                    else
                        differences.Add(currentDifference, 1);
                    effectiveRating = input[i];
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return differences;
        }

        #region inefficient

        int totalWays = 0;
        public void PrintArrangements(List<int> input, int offset = 0, string currentArrangement = "")
        {
            try
            {
                if (offset == input.Count - 1)
                {
                    currentArrangement += string.Format("({0})", input[offset]);
                    Console.WriteLine(currentArrangement);
                    totalWays++;
                }
                else
                {
                    if (offset == 0)
                        currentArrangement += string.Format("(0), {0}, ", input[offset]);
                    else
                        currentArrangement += string.Format("{0}, ", input[offset]);
                    if ((input.Count > (offset + 3)) && input[offset + 3] - input[offset] <= 3)
                        PrintArrangements(input, offset + 3, currentArrangement);
                    if ((input.Count > (offset + 2)) && input[offset + 2] - input[offset] <= 3)
                        PrintArrangements(input, offset + 2, currentArrangement);
                    if ((input.Count > (offset + 1)) && input[offset + 1] - input[offset] <= 3)
                        PrintArrangements(input, offset + 1, currentArrangement);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        
        #endregion

        public void GetArrangements(List<int> input, int offset = 0)
        {
            try 
            {
                List<int> listHigherThan3 = new List<int>();
                int previousValue = 0;
                for (int i = 0; i < input.Count; i++)
                {
                    if ((input[i] - previousValue) >= 3)
                        listHigherThan3.Add(i);
                    previousValue = input[i];
                }

                for (int i = listHigherThan3.Count-1; i >= 0; i--)
                {
                    int lowrange = 0;
                    if(i > 0)
                        lowrange = listHigherThan3[i-1];
                    List<int> range = input.GetRange(lowrange, listHigherThan3[i] - lowrange + 1);

                    Dictionary<int, int> differences = CalculateDifferences(range);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
