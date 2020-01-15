using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day4
    {
        public Day4()
        { }

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 4: Secure Container ---");
            Console.WriteLine("-------------------------------");
            
            GetNumberOfMatches();
        }

        private void GetNumberOfMatches()
        {
            try
            {
                string[] limits = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day4-Input.txt").Split('-');

                int minimum = 0, maximum = 0, count = 0;

                if (int.TryParse(limits[0], out minimum) && int.TryParse(limits[1], out maximum))
                {
                    Console.WriteLine("Minimum: {0}", minimum);
                    Console.WriteLine("Maximun: {0}", maximum);

                    for (int i= minimum; i <= maximum; i++)
                    {
                        if (CheckCriteria(i))
                            count++;
                    }
                }
                Console.WriteLine("{0} different passwords match the criteria", count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error: {0}", ex.Message);
            }
        }

        private bool CheckCriteria(int number)
        {
            bool match = false;
            try
            {
                bool hasSameDigits = false;
                bool alwaysIncrease = true;
                string textNumber = number.ToString();
                char previousCharacter = '0';

                for (int i = 0; i < textNumber.Length; i++)
                {
                    if(i > 0 && previousCharacter.Equals(textNumber[i]))
                        hasSameDigits = true;

                    int previousNumber = 0, currentNumber = 0;
                    int.TryParse(previousCharacter.ToString(), out previousNumber);
                    int.TryParse(textNumber[i].ToString(), out currentNumber);
                    if (previousNumber > currentNumber)
                    {
                        alwaysIncrease = false;
                        break;
                    }
                    previousCharacter = textNumber[i];
                }

                match = (hasSameDigits && alwaysIncrease);
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in: {0}", ex.Message);
            }
            return match;
        }
    }
}
