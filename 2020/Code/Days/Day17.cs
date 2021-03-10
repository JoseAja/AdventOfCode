using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Code.Days
{
    class Day17
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 17: Conway Cubes                  ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day17-Input.txt");
            //Dictionary<string, string> memory = EvaluateInput(input);
            //long sum = SumMemoryValues(memory);
            //Console.WriteLine("Sum of all values left in memory: {0}", sum);
            //validPassportsCount = CountValidPassportsAndData(input, true);
            //Console.WriteLine("Number of valid passports and data: {0}", validPassportsCount);
        }

        private void ExecuteCycles(int numberOfCycles)
        {
            try 
            {
                for (int i = 0; i < numberOfCycles; i++)
                {

                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
