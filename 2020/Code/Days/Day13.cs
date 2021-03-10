using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aoc2020.Code.Days
{
    class Day13
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 13: Shuttle Search                ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day13-Input.txt");
            int[] firstBus = GetFirstBus(input);
            Console.WriteLine("ID of the earliest bus to take multiplied by the number of minutes to wait: {0}", firstBus[0] * firstBus[1]);
            double earliestTimestamp = GetCommonTime(input);
            Console.WriteLine("Earliest timestamp such that all of the listed bus IDs depart at offsets matching their positions in the list: {0}", earliestTimestamp);
        }

        private int[] GetFirstBus(string[] input)
        {
            int[] bus = new int[] { 0, 0 }; // represents busId, time to wait
            try
            {
                int.TryParse(input[0], out int earliest);
                int time = earliest;
                string[] lines = input[1].Split(',').Where(line => line.ToLower()!="x").ToArray();
                List<int> linesNumber = GeneralFunctions.ToIntList(lines);
                do
                {
                    for (int i = 0; i < linesNumber.Count; i++)
                    {
                        if(time % linesNumber[i] == 0)
                        {
                            bus[0] = linesNumber[i];
                            bus[1] = time - earliest;
                            break;
                        }
                    }

                    time++;
                }
                while (bus[0] == 0);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return bus;
        }

        /// <summary>
        /// this method gives correct answer but is very inefficient. Lasts more than 3 hours
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double GetCommonTime(string[] input)
        {
            double currentTime = 0;
            try
            {
                string[] lines = input[1].Split(',').Select(line => (line.ToLower() != "x" ? line : "0")).ToArray();
                List<int> linesNumber = GeneralFunctions.ToIntList(lines);
                double firstTimeCoincidence = GetFirstTimeOfCoincidence(linesNumber);
                currentTime = firstTimeCoincidence;
                bool execute = true;
                do 
                {
                    double initialTime = currentTime - linesNumber[0];
                    for (int i = 0; i < linesNumber.Count; i++)
                    {
                        if (linesNumber[i] != 0)
                        {
                            if ((initialTime + i) % linesNumber[i] != 0)
                                break;

                            if (i == (linesNumber.Count - 1))
                                execute = false;
                        }
                    }
                    if (execute)
                        currentTime += firstTimeCoincidence;
                    else
                        currentTime = initialTime;
                }
                while (execute);
                
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return currentTime;
        }

        /// <summary>
        /// TODO - fast solutuion using Chinese Remainder Theorem.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double GetCommonTimeChineseTheorem(string[] input)
        {
            return 0;
        }

        private List<int> GetConcurrentLinesInATime(int currentPosition, int currentLine, List<int> linesNumber)
        {
            List<int> concurrent = new List<int>();
            try 
            { 
                while (linesNumber.Count > currentPosition)
                {
                    concurrent.Add(linesNumber[currentPosition]);
                    currentPosition += currentLine;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return concurrent;
        }

        private double GetFirstTimeOfCoincidence(List<int> linesNumber)
        {
            double firstTimeCoincidence = 0;

            try
            {
                List<int> concurrent = new List<int>();

                for (int i = 0; i < linesNumber.Count; i++)
                {
                    if (linesNumber[i] != 0)
                    {
                        concurrent = GetConcurrentLinesInATime(i, linesNumber[i], linesNumber);
                        break;
                    }
                }

                firstTimeCoincidence = GeneralFunctions.CalculateMCM(concurrent.ToArray());
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return firstTimeCoincidence;
        }

    }
}
