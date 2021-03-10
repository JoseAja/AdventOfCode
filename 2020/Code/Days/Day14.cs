using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Code.Days
{
    class Day14
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 14: Docking Data                 ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day14-Input.txt");
            Dictionary<string, string> memory = EvaluateInput(input);
            long sum = SumMemoryValues(memory);
            Console.WriteLine("Sum of all values left in memory: {0}", sum);
            //validPassportsCount = CountValidPassportsAndData(input, true);
            //Console.WriteLine("Number of valid passports and data: {0}", validPassportsCount);
        }

        private Dictionary<string, string> EvaluateInput(string[] input)
        {
            Dictionary<string, string> memory = new Dictionary<string, string>();
            try 
            {   
                string currentMask = "";
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Contains("mask"))
                        currentMask = input[i].Split('=')[1].Trim();
                    else
                    {
                        string[] operation = input[i].Split('=');
                        string number = operation[1].Trim();
                        string binaryNumber = GetValueAfterMask(number, currentMask);
                        string memoryPosition = operation[0].Trim().Replace("mem[", "").Replace("]", "");

                        if (memory.ContainsKey(memoryPosition))
                            memory[memoryPosition] = binaryNumber;
                        else
                            memory.Add(memoryPosition, binaryNumber);
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return memory;
        }

        private long SumMemoryValues(Dictionary<string, string> memory)
        {
            long sum = 0;
            try
            {
                foreach(string key in memory.Keys)
                {
                    long value = Convert.ToInt64(memory[key], 2);
                    sum += value;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return sum;
        }

        private string GetValueAfterMask(string number, string mask)
        {
            string binary = "";
            try
            {
                int.TryParse(number, out int binaryNumber);
                binary = Convert.ToString(binaryNumber, 2).PadLeft(36, '0');
                for (int i = 0; i < mask.Length; i++)
                {
                    if (mask[i] == '1')
                        binary = binary.Remove(i, 1).Insert(i, "1");
                    else if (mask[i] == '0')
                        binary = binary.Remove(i, 1).Insert(i, "0");
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return binary;
        }

        private string GetValueAfterMaskVersion2(string number, string mask)
        {
            string binary = "";
            try
            {
                int.TryParse(number, out int binaryNumber);
                binary = Convert.ToString(binaryNumber, 2).PadLeft(36, '0');
                for (int i = 0; i < mask.Length; i++)
                {
                    if (mask[i] == '1')
                        binary = binary.Remove(i, 1).Insert(i, "1");
                    else if (mask[i] == '0')
                        binary = binary.Remove(i, 1).Insert(i, "0");
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return binary;
        }
    }
}
