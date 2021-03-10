using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Aoc2020.Code.Days
{
    class Day9
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 9: Encoding Error                 ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day9-Input.txt");
            ResultXMASEncrypted firstInvalidNumber = GetFirstInvalid(input, 25);
            Console.WriteLine("First number that is not a sum: {0}", firstInvalidNumber.firstInvalidNumber);
            double sum = GetWeakness(input, firstInvalidNumber);
            Console.WriteLine("The encryption weakness is: {0}", sum);
        }

        private ResultXMASEncrypted GetFirstInvalid(string[] input, int numberOfElements)
        {
            ResultXMASEncrypted firstInvalidNumber = new ResultXMASEncrypted();
            try 
            {
                for (int i = numberOfElements; i < input.Length; i++)
                {
                    int.TryParse(input[i], out int currentNumber);
                    bool isSum = false;
                    int firstOperandPosition = i-1;
                    while (firstOperandPosition > i - numberOfElements)
                    {
                        int.TryParse(input[firstOperandPosition], out int firstOperand);

                        int secondOperandPosition = firstOperandPosition - 1;
                        while (secondOperandPosition >= i - numberOfElements)
                        {
                            int.TryParse(input[secondOperandPosition], out int secondOperand);
                            if ((firstOperand + secondOperand) == currentNumber)
                            {
                                isSum = true;
                                break;
                            }
                            else
                                secondOperandPosition--;
                        }
                        if (isSum)
                            break;
                        else
                            firstOperandPosition--;
                    }
                    if (!isSum)
                    {
                        firstInvalidNumber.firstInvalidNumber = currentNumber.ToString();
                        firstInvalidNumber.firstInvalidPosition = i;
                        break;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return firstInvalidNumber;
        }

        private double GetWeakness(string[] input, ResultXMASEncrypted firstInvalidNumber)
        {
            double sum = 0;
            try 
            {
                List<double> operands = new List<double>();
                double.TryParse(firstInvalidNumber.firstInvalidNumber, out double firstInvalid);
                for (int i = firstInvalidNumber.firstInvalidPosition - 1; i >= 0; i--)
                {
                    operands = new List<double>();
                    double.TryParse(input[i], out double currentSum);
                    operands.Add(currentSum);
                    int currentOperand = i - 1;
                    while (currentSum < firstInvalid)
                    {
                        double.TryParse(input[currentOperand], out double currentOperandValue);
                        currentSum += currentOperandValue;
                        operands.Add(currentOperandValue);
                        currentOperand--;
                    }
                    if (currentSum == firstInvalid) 
                        break;
                }

                if (operands.Count >= 2)
                {
                    operands.Sort();
                    sum = operands[0] + operands[operands.Count - 1];
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return sum;
        }
    }

    class ResultXMASEncrypted
    {
        public string firstInvalidNumber = "0";
        public int firstInvalidPosition = -1;
    }
}
