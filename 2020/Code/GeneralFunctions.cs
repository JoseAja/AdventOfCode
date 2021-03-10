using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aoc2020.Code
{
    public static class GeneralFunctions
    {
        public static List<int> ToIntList(string[] input)
        {
            List<int> list = new List<int>();
            try
            {
                list = input.Select(Int32.Parse).ToList();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return list;
        }

        public static double CalculateMCM(Int32[] numbers)
        {
            double result = 0;
            Dictionary<Int32, Int32> divisorsForTotal = new Dictionary<Int32, Int32>();
            for (int i = 0; i < numbers.Length; i++)
            {
                Dictionary<Int32, Int32> divisors = CalculateDivisors(numbers[i]);

                List<KeyValuePair<Int32, Int32>> existingElements = divisors.Where(d => divisorsForTotal.ContainsKey(d.Key)).ToList();
                foreach (KeyValuePair<Int32, Int32> element in existingElements)
                {
                    if (divisorsForTotal[element.Key] < element.Value)
                    {
                        divisorsForTotal.Remove(element.Key);
                        divisorsForTotal.Add(element.Key, element.Value);
                    }
                }

                List<KeyValuePair<Int32, Int32>> noExistingElements = divisors.Where(d => !divisorsForTotal.ContainsKey(d.Key)).ToList();
                foreach (KeyValuePair<Int32, Int32> element in noExistingElements)
                {
                    divisorsForTotal.Add(element.Key, element.Value);
                }
            }

            foreach (KeyValuePair<Int32, Int32> element in divisorsForTotal)
            {
                if (result == 0)
                    result = Math.Pow(element.Key, element.Value);
                else
                    result *= Math.Pow(element.Key, element.Value);
            }

            return result;
        }

        public static Dictionary<Int32, Int32> CalculateDivisors(Int32 number)
        {
            Dictionary<Int32, Int32> divisors = new Dictionary<Int32, Int32>();
            Int32 divisor = 2;
            do
            {
                if (number % divisor == 0)
                {
                    if (divisors.ContainsKey(divisor))
                        divisors[divisor]++;
                    else
                        divisors.Add(divisor, 1);
                    number = number / divisor;
                }
                else
                    divisor++;
            }
            while (number > 1);
            return divisors;
        }
    }
}
