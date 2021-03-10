using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Aoc2020.Code.Days
{
    class Day8
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 8: Handheld Halting              ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day8-Input.txt");
            int[] accumulator = getAccumulator(input);
            Console.WriteLine("Accumulator value before loop: {0}", accumulator[0]);
            int accumulatorCorrected = fixLoop(input);
            Console.WriteLine("Accumulator of corrected program: {0}", accumulatorCorrected);
        }

        private int[] getAccumulator(string[] input)
        {
            int[] accumulator = new int[] { 0, 0 };
            try
            {
                List<int> readedPositions = new List<int>();
                int currentPosition = 0;
                bool execute = true;
                while (execute)
                {
                    if (input.Length > currentPosition && !readedPositions.Contains(currentPosition))
                    {
                        readedPositions.Add(currentPosition);
                        string[] operation = input[currentPosition].Split(' ');
                        switch (operation[0])
                        {
                            case "nop":
                                currentPosition++;
                                break;
                            case "jmp":
                                int.TryParse(operation[1], out int offset);
                                currentPosition += offset;
                                break;
                            case "acc":
                                int.TryParse(operation[1], out int accumulatorChange);
                                accumulator[0] += accumulatorChange;
                                currentPosition++;
                                break;
                        }
                    }
                    else
                    {
                        execute = false;
                        accumulator[1] = readedPositions[readedPositions.Count-1];
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return accumulator;
        }

        private int fixLoop (string[] input)
        {
            int accumulator = 0;
            try 
            {
                for (int i = 0; i < input.Length; i++)
                {
                    bool fix = false;
                    string[] inputCorrected = (string[]) input.Clone();
                    if (input[i].IndexOf("nop")== 0)
                    {
                        inputCorrected[i] = inputCorrected[i].Replace("nop", "jmp");
                        fix = true;
                    }
                    else if (input[i].IndexOf("jmp") == 0)
                    {
                        inputCorrected[i] = inputCorrected[i].Replace("jmp", "nop");
                        fix = true;
                    }
                    if(fix)
                    {
                        int[] result = getAccumulator(inputCorrected);
                        if(result[1] == (input.Length - 1))
                        {
                            accumulator = result[0];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return accumulator;
        }
    }
}
