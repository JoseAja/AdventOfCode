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
            if (input.Trim().Equals("1") || input.Trim().Equals("5"))
            {
                string[] array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day5-Input.txt").Split(',');
                int output = IntcodeExtended(array, null);
                Console.WriteLine("Diagnostic code:{0}", output);
            }
        }

        public int IntcodeExtended(string[] array, List<int> input)
        {
            int output = 0;
            int inputCounter = 0;
            try
            {                
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
                                Opcode1(ref array, position1, position2, position3, parameter1, parameter2);
                                i += 4;
                            }
                            else if (opcode.Equals("02") && array.Length > (i + 3))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                Opcode2(ref array, position1, position2, position3, parameter1, parameter2);
                                i += 4;
                            }
                            else if (opcode.Equals("03") && array.Length > (i + 1))
                            {
                                int? inputValue = (input == null || input.Count == 0) ? null : (int?)input[inputCounter];
                                Opcode3(ref array, i + 1, position1, parameter1, inputValue);
                                i += 2;
                                inputCounter++;
                            }
                            else if (opcode.Equals("04") && array.Length > (i + 1))
                            {
                                output += Opcode4(ref array, position1, parameter1);
                                i += 2;
                            }
                            else if (opcode.Equals("05") && array.Length > (i + 2))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                int next = Opcode5(ref array, position1, position2, parameter1, parameter2);
                                if (next >= 0)
                                    i = next;
                                else
                                    i += 3;
                            }
                            else if (opcode.Equals("06") && array.Length > (i + 2))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                int next = Opcode6(ref array, position1, position2, parameter1, parameter2);
                                if (next >= 0)
                                    i = next;
                                else
                                    i += 3;
                            }
                            else if (opcode.Equals("07") && array.Length > (i + 4))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                int position4 = 0;
                                int.TryParse(array[i + 4], out position4);
                                Opcode7(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("08") && array.Length > (i + 4))
                            {
                                int position2 = 0;
                                int.TryParse(array[i + 2], out position2);
                                int position3 = 0;
                                int.TryParse(array[i + 3], out position3);
                                int position4 = 0;
                                int.TryParse(array[i + 4], out position4);
                                Opcode8(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
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
            return output;
        }

        private bool checkIsANumberOption(string option)
        {
            return option.Equals("01") || option.Equals("02") || option.Equals("03") || option.Equals("04") || option.Equals("05") || option.Equals("06") || option.Equals("07") || option.Equals("08");
        }

        private void Opcode1(ref string[] array, int position1, int position2, int position3, string parameter1, string parameter2)
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
        }

        private void Opcode2(ref string[] array, int position1, int position2, int position3, string parameter1, string parameter2)
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
        }

        private void Opcode3(ref string[] array, int currentPosition, int position, string parameter1, int? input)
        {
            try
            {
                if (input == null)
                {
                    Console.WriteLine("Introduce an input value:");
                    string inputString = Console.ReadLine().Trim();
                    int inputNumber = 0;
                    if (!int.TryParse(inputString, out inputNumber))
                        Console.WriteLine("The value is invalid.");
                    else
                        input = inputNumber;
                }
                if (parameter1.Equals("0"))
                {
                    if (array.Length > position)
                        array[position] = input.ToString();
                }
                else
                {
                    if (array.Length > currentPosition)
                        array[currentPosition] = input.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode3(): {0}", ex.Message);
            }
        }

        private int Opcode4(ref string[] array, int position, string parameter1)
        {
            int output = 0;
            try
            {
                if (parameter1.Equals("0"))
                {
                    if (array.Length > position)
                        int.TryParse(array[position], out output);
                    else
                        Console.WriteLine("no output");
                }
                else
                    output = position;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode4(): {0}", ex.Message);
            }
            return output;
        }

        private int Opcode5(ref string[] array, int position1, int position2, string parameter1, string parameter2)
        {
            int nextPosition = -1;
            try
            {
                int number1 = position1;
                int number2 = position2;

                if (parameter1.Equals("0"))
                    int.TryParse(array[position1], out number1);
                if (parameter2.Equals("0"))
                    int.TryParse(array[position2], out number2);

                if(!number1.Equals(0))
                    nextPosition = number2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode5(): {0}", ex.Message);
            }
            return nextPosition;
        }

        private int Opcode6(ref string[] array, int position1, int position2, string parameter1, string parameter2)
        {
            int nextPosition = -1;
            try
            {
                int number1 = position1;
                int number2 = position2;

                if (parameter1.Equals("0"))
                    int.TryParse(array[position1], out number1);
                if (parameter2.Equals("0"))
                    int.TryParse(array[position2], out number2);

                if (number1.Equals(0))
                    nextPosition = number2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode6(): {0}", ex.Message);
            }
            return nextPosition;
        }

        private int Opcode7(ref string[] array, int position1, int position2, int position3, string parameter1, string parameter2, string parameter3)
        {
            int nextPosition = 0;
            try
            {
                int number1 = position1;
                int number2 = position2;
                int number3 = position3;

                if (parameter1.Equals("0"))
                    int.TryParse(array[position1], out number1);
                if (parameter2.Equals("0"))
                    int.TryParse(array[position2], out number2);
                if (parameter3.Equals("0"))
                    int.TryParse(array[position3], out number3);

                if (number1 < number2)
                    array[position3] = "1";
                else
                    array[position3] = "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode7(): {0}", ex.Message);
            }
            return nextPosition;
        }
        
        private int Opcode8(ref string[] array, int position1, int position2, int position3, string parameter1, string parameter2, string parameter3)
        {
            int nextPosition = 0;
            try
            {
                int number1 = position1;
                int number2 = position2;
                int number3 = position3;

                if (parameter1.Equals("0"))
                    int.TryParse(array[position1], out number1);
                if (parameter2.Equals("0"))
                    int.TryParse(array[position2], out number2);
                if (parameter3.Equals("0"))
                    int.TryParse(array[position3], out number3);

                if (number1.Equals(number2))
                    array[position3] = "1";
                else
                    array[position3] = "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode8(): {0}", ex.Message);
            }
            return nextPosition;
        }
    }
}
