using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aoc2020.Code.Days
{
    class Day1
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 1: Report Repair                  ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day1-Input.txt");
            int result = Find2020TwoNumbers(input);
            Console.WriteLine("The multiplication result is for two numbers is: {0}", result);
            result = Find2020ThreeNumbers(input);
            Console.WriteLine("The multiplication result is for three numbers is: {0}", result);
        }

        private int Find2020TwoNumbers(string[] input)
        {
            int result = 0;
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    int.TryParse(input[i], out int currentNumber);
                    int searchNumber = 2020 - currentNumber;
                    string secondNumber = input.FirstOrDefault(n => n == searchNumber.ToString());
                    if (secondNumber != null && secondNumber.Length > 0)
                    {
                        result = searchNumber * currentNumber;
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        private int Find2020ThreeNumbers(string[] input)
        {
            int result = 0;
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    int.TryParse(input[i], out int currentNumber);

                    for (int j = (i+1); j < input.Length; j++)
                    {
                        int.TryParse(input[j], out int secondCurrentNumber);
                        int searchNumber = 2020 - currentNumber - secondCurrentNumber;
                        string thirdNumber = input.FirstOrDefault(n => n == searchNumber.ToString());
                        if (thirdNumber != null && thirdNumber.Length > 0)
                        {
                            result = searchNumber * currentNumber * secondCurrentNumber;
                            break;
                        }
                    }
                    if (result > 0)
                        break;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }
    }
}
