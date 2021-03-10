using System;
using System.Diagnostics;
using System.IO;

namespace Aoc2020.Code.Days
{
    class Day3
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 3: Toboggan Trajectory            ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day3-Input.txt");
            ulong result = FindTreesInTrajectory(input, 3, 1);
            Console.WriteLine("Trees in tobbogan trajectory: {0}", result);
            ulong result2 = MultipleSlopesTrajectory(input);
            Console.WriteLine("Trees in tobbogan trajectory: {0}", result2.ToString());
        }

        private ulong FindTreesInTrajectory(string[] input, int xSlope, int ySlope)
        {
            ulong trees = 0;
            try {
                int xPosition = xSlope; //zero based positions
                int yPosition = ySlope;
                int rowLenght = input[0].Length;

                while (yPosition < input.Length)
                {
                    if(input[yPosition][xPosition % rowLenght] == '#')
                        trees++;
                    xPosition += xSlope;
                    yPosition += ySlope;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return trees;
        }

        private ulong MultipleSlopesTrajectory(string[] input)
        {
            ulong result = 0;
            try
            {
                ulong slope1 = FindTreesInTrajectory(input, 1, 1);
                ulong slope2 = FindTreesInTrajectory(input, 3, 1);
                ulong slope3 = FindTreesInTrajectory(input, 5, 1);
                ulong slope4 = FindTreesInTrajectory(input, 7, 1);
                ulong slope5 = FindTreesInTrajectory(input, 1, 2);
                result = slope1 * slope2 * slope3 * slope4 * slope5;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }
    }
}
