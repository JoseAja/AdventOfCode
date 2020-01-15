using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day5
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 5: Sunny with a Chance of Asteroids ---");
            Console.WriteLine("---------------------------------");

            ManageID();
        }

        private void ManageID()
        {
            Console.WriteLine("Introduce the ID of the system to test:");
            string input = Console.ReadLine().Trim();
            if(input.Trim().Equals("1"))
                IntcodeExtended();
        }

        private void IntcodeExtended()
        {
            try
            {
                string[] array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day5-Input.txt").Split(',');
                
                if (array.Length > 0 && !array[0].Equals("99"))
                {
                    int i = 0;
                    do
                    {
                        string positionValue = array[i].PadLeft(5,'0');
                        string opcode = positionValue[3].ToString() + positionValue[4].ToString();
                        string parameter1 = positionValue[2].ToString();
                        string parameter2 = positionValue[1].ToString();
                        string parameter3 = positionValue[0].ToString();// not used as Parameters that an instruction writes to will never be in immediate mode

                        if (checkIsANumberOption(opcode))
                        {
                            int position1 = 0;
                            int.TryParse(array[i + 1], out position1);

                            if (opcode.Equals("01") && array.Length > (i + 3))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                array = Opcode1(array, position1, position2, position3, parameter1, parameter2);
                                i += 4;
                            }
                            else if (opcode.Equals("02") && array.Length > (i + 3))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                array = Opcode2(array, position1, position2, position3, parameter1, parameter2);
                                i += 4;
                            }
                            else if (opcode.Equals("03") && array.Length > (i + 1))
                            {
                                array = Opcode3(array, position1);
                                i += 2;
                            }
                            else if (opcode.Equals("04") && array.Length > (i + 1))
                            {
                                array = Opcode4(array, position1);
                                i += 2;
                            }
                            else
                                i++;
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                            break;
                        }
                    }
                    while (array.Length > i && array[i] != "99");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in IntcodeExtended(): {0}", ex.Message);
            }
        }

        private bool checkIsANumberOption(string option)
        {
            return option.Equals("01") || option.Equals("02") || option.Equals("03") || option.Equals("04");
        }

        private string[] Opcode1(string[] array, int position1, int position2, int position3, string parameter1, string parameter2)
        {
            try
            {
                int number1 = position1;
                int number2 = position2;

                if(parameter1.Equals("0"))
                    int.TryParse(array[position1], out number1);
                if (parameter2.Equals("0"))
                    int.TryParse(array[position2], out number2);

                if (array.Length > position3)
                    array[position3] = (number1 + number2).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode1(): {0}", ex.Message);
            }
            return array;
        }

        private string[] Opcode2(string[] array, int position1, int position2, int position3, string parameter1, string parameter2)
        {
            try
            {
                int number1 = position1;
                int number2 = position2;

                if (parameter1.Equals("0"))
                    int.TryParse(array[position1], out number1);
                if (parameter2.Equals("0"))
                    int.TryParse(array[position2], out number2);

                if (array.Length > position3)
                    array[position3] = (number1 * number2).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode2(): {0}", ex.Message);
            }
            return array;
        }

        private string[] Opcode3(string[] array, int position)
        {
            try
            {
                Console.WriteLine("Introduce an input value:");
                string input = Console.ReadLine().Trim();
                int numberInput = 0;
                if (!int.TryParse(input, out numberInput))
                    Console.WriteLine("The value is invalid.");
                if (array.Length > position)
                    array[position] = numberInput.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode3(): {0}", ex.Message);
            }
            return array;
        }

        private string[] Opcode4(string[] array, int position)
        {
            try
            {
                if (array.Length > position)
                    Console.WriteLine("The output for position {0} is {1}", position, array[position]);
                else
                    Console.WriteLine("no output");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode4(): {0}", ex.Message);
            }
            return array;
        }
    }
}
