using System;
using System.Collections.Generic;

namespace AdventOfCode2019.classes
{
    public class IntcodeProgram
    {
        private Int64 relativeBase = 0;

        private Int64 GetValueInposition(Dictionary<string, string> array, Int64 position)
        {
            Int64 value = 0;
            if(array.ContainsKey(position.ToString()))
                Int64.TryParse(array[position.ToString()], out value);
            return value;
        }

        public Int64 Intcode(string[] programArray, List<Int64> input)
        {
            Int64 output = 0;
            int inputCounter = 0;
            try
            {
                Dictionary<string, string>  array = ArrayToDictionary(programArray);
                if (array.Count > 0 && !array["0"].Equals("99"))
                {
                    Int64 i = 0;
                    do
                    {
                        string positionValue = array[i.ToString()].PadLeft(5, '0');
                        string opcode = positionValue[3].ToString() + positionValue[4].ToString();
                        string parameter1 = positionValue[2].ToString();
                        string parameter2 = positionValue[1].ToString();
                        string parameter3 = positionValue[0].ToString();// not used as Parameters that an instruction writes to will never be in immediate mode

                        if (checkIsANumberOption(opcode))
                        {
                            int position1 = 0;
                            int.TryParse(array[(i + 1).ToString()], out position1);

                            if (opcode.Equals("01") && array.Count > (i + 3))
                            {
                                int position2 = 0;
                                int.TryParse(array[(i + 2).ToString()], out position2);
                                int position3 = 0;
                                int.TryParse(array[(i + 3).ToString()], out position3);
                                Opcode1(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("02") && array.Count > (i + 3))
                            {
                                int position2 = 0;
                                int.TryParse(array[(i + 2).ToString()], out position2);
                                int position3 = 0;
                                int.TryParse(array[(i + 3).ToString()], out position3);
                                Opcode2(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("03") && array.Count > (i + 1))
                            {
                                int? inputValue = (input == null || input.Count == 0) ? null : (int?)input[inputCounter];
                                Opcode3(ref array, position1, parameter1, inputValue);
                                i += 2;
                                inputCounter++;
                            }
                            else if (opcode.Equals("04") && array.Count > (i + 1))
                            {
                                output += Opcode4(ref array, position1, parameter1);
                                i += 2;
                            }
                            else if (opcode.Equals("05") && array.Count > (i + 2))
                            {
                                Int64 position2 = 0;
                                Int64.TryParse(array[(i + 2).ToString()], out position2);
                                Int64 position3 = 0;
                                Int64.TryParse(array[(i + 3).ToString()], out position3);
                                Int64 next = Opcode5(ref array, position1, position2, parameter1, parameter2);
                                if (next >= 0)
                                    i = next;
                                else
                                    i += 3;
                            }
                            else if (opcode.Equals("06") && array.Count > (i + 2))
                            {
                                Int64 position2 = 0;
                                Int64.TryParse(array[(i + 2).ToString()], out position2);
                                Int64 position3 = 0;
                                Int64.TryParse(array[(i + 3).ToString()], out position3);
                                Int64 next = Opcode6(ref array, position1, position2, parameter1, parameter2);
                                if (next >= 0)
                                    i = next;
                                else
                                    i += 3;
                            }
                            else if (opcode.Equals("07") && array.Count > (i + 4))
                            {
                                Int64 position2 = 0;
                                Int64.TryParse(array[(i + 2).ToString()], out position2);
                                Int64 position3 = 0;
                                Int64.TryParse(array[(i + 3).ToString()], out position3);
                                Int64 position4 = 0;
                                Int64.TryParse(array[(i + 4).ToString()], out position4);
                                Opcode7(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("08") && array.Count > (i + 4))
                            {
                                Int64 position2 = 0;
                                Int64.TryParse(array[(i + 2).ToString()], out position2);
                                Int64 position3 = 0;
                                Int64.TryParse(array[(i + 3).ToString()], out position3);
                                Int64 position4 = 0;
                                Int64.TryParse(array[(i + 4).ToString()], out position4);
                                Opcode8(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("09") && array.Count > (i + 2))
                            {
                                Opcode9(ref array, position1, parameter1);
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
                    while (array.Count > i && array[i.ToString()] != "99");
                    DictionaryToArray(ref programArray, array);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Intcode(): {0}", ex.Message);
            }
            return output;
        }

        public ResultOpcode IntcodeExtended(string[] programArray, List<Int64> input, Int64 i = 0)
        {
            ResultOpcode result = new ResultOpcode();
            bool firstinput = true;
            try
            {
                Dictionary<string, string> array = ArrayToDictionary(programArray);
                if (array.Count > 0 && !array["0"].Equals("99"))
                {
                    do
                    {
                        string positionValue = array[i.ToString()].PadLeft(5, '0');
                        string opcode = positionValue[3].ToString() + positionValue[4].ToString();
                        string parameter1 = positionValue[2].ToString();
                        string parameter2 = positionValue[1].ToString();
                        string parameter3 = positionValue[0].ToString();// not used as Parameters that an instruction writes to will never be in immediate mode

                        if (checkIsANumberOption(opcode))
                        {
                            Int64 position1 = GetValueInposition(array, i + 1);                            
                            
                            if (opcode.Equals("01") && array.Count > (i + 3))
                            {
                                Int64 position2 = GetValueInposition(array, i + 2);
                                Int64 position3 = GetValueInposition(array, i + 3);
                                Opcode1(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("02") && array.Count > (i + 3))
                            {
                                Int64 position2 = GetValueInposition(array, i + 2);                                
                                Int64 position3 = GetValueInposition(array, i + 3);
                                Opcode2(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("03") && array.Count > (i + 1))
                            {
                                Int64? inputValue = null;
                                if (firstinput)
                                    inputValue = (Int64?)input[0];
                                else
                                    inputValue = (Int64?)input[input.Count - 1];
                                Opcode3(ref array, position1, parameter1, inputValue);
                                i += 2;
                                firstinput = false;
                            }
                            else if (opcode.Equals("04") && array.Count > (i + 1))
                            {
                                Int64 outOpcode4 = Opcode4(ref array, position1, parameter1);
                                result.output = outOpcode4;
                                i += 2;
                                result.state = i;
                                DictionaryToArray(ref programArray, array);
                                break;
                            }
                            else if (opcode.Equals("05") && array.Count > (i + 2))
                            {
                                Int64 position2 = GetValueInposition(array, i + 2);                                
                                Int64 position3 = GetValueInposition(array, i + 3);
                                Int64 next = Opcode5(ref array, position1, position2, parameter1, parameter2);
                                if (next >= 0)
                                    i = next;
                                else
                                    i += 3;
                            }
                            else if (opcode.Equals("06") && array.Count > (i + 2))
                            {
                                Int64 position2 = GetValueInposition(array, i + 2);
                                Int64 position3 = GetValueInposition(array, i + 3);
                                Int64 next = Opcode6(ref array, position1, position2, parameter1, parameter2);
                                if (next >= 0)
                                    i = next;
                                else
                                    i += 3;
                            }
                            else if (opcode.Equals("07") && array.Count > (i + 4))
                            {
                                Int64 position2 = GetValueInposition(array, i + 2);
                                Int64 position3 = GetValueInposition(array, i + 3);
                                Int64 position4 = GetValueInposition(array, i + 4);
                                Opcode7(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("08") && array.Count > (i + 4))
                            {
                                Int64 position2 = GetValueInposition(array, i + 2);
                                Int64 position3 = GetValueInposition(array, i + 3);
                                Int64 position4 = GetValueInposition(array, i + 4);
                                Opcode8(ref array, position1, position2, position3, parameter1, parameter2, parameter3);
                                i += 4;
                            }
                            else if (opcode.Equals("09") && array.Count > (i + 2))
                            {
                                Opcode9(ref array, position1, parameter1);
                                i += 2;
                            }
                            else if (opcode.Equals("99"))
                            {
                                result.finished = true;
                                DictionaryToArray(ref programArray, array);
                                break;
                            }
                            else
                                i++;
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                            break;
                        }
                        result.state = i;
                    }
                    while (array.Count > i && array[i.ToString()] != "99");
                    DictionaryToArray(ref programArray, array);
                    if (array[i.ToString()] == "99")
                        result.finished = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in IntcodeExtended(): {0}", ex.Message);
            }
            return result;
        }

        private Dictionary<string, string> ArrayToDictionary(string[] array)
        {
            Dictionary<string, string> greaterArray =new Dictionary<string, string>();
            for (int i = 0; i < array.Length; i++)
            {                
                greaterArray.Add(i.ToString(), array[i]);
            }
            return greaterArray;
        }

        private void DictionaryToArray(ref string[] array, Dictionary<string, string> greaterArray)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = greaterArray[i.ToString()];
            }
        }

        private bool checkIsANumberOption(string option)
        {
            return option.Equals("01") || option.Equals("02") || option.Equals("03") || option.Equals("04") || option.Equals("05") || option.Equals("06") || option.Equals("07") || option.Equals("08") || option.Equals("09") || option.Equals("99");
        }

        private void Opcode1(ref Dictionary<string, string> array, Int64 position1, Int64 position2, Int64 position3, string parameter1, string parameter2, string parameter3)
        {
            try
            {
                Int64 number1 = position1;
                Int64 number2 = position2;
                Int64 number3 = position3;

                if (parameter1.Equals("0"))
                    number1 = GetValueInposition(array, position1);
                else if (parameter1.Equals("2"))
                    number1 = GetValueInposition(array, position1 + relativeBase);

                if (parameter2.Equals("0"))
                    number2 = GetValueInposition(array, position2);
                else if (parameter2.Equals("2"))
                    number2 = GetValueInposition(array, position2 + relativeBase);

                if (parameter3.Equals("2"))
                    number3 = position3 + relativeBase;

                if (array.ContainsKey(number3.ToString()))
                    array[number3.ToString()] = (number1 + number2).ToString();
                else
                    array.Add(number3.ToString(), (number1 + number2).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode1(): {0}", ex.Message);
            }
        }

        private void Opcode2(ref Dictionary<string, string> array, Int64 position1, Int64 position2, Int64 position3, string parameter1, string parameter2, string parameter3)
        {
            try
            {
                Int64 number1 = position1;
                Int64 number2 = position2;
                Int64 number3 = position3;

                if (parameter1.Equals("0"))
                    number1 = GetValueInposition(array, position1);
                else if (parameter1.Equals("2"))
                    number1 = GetValueInposition(array, position1 + relativeBase);

                if (parameter2.Equals("0"))
                    number2 = GetValueInposition(array, position2);
                else if (parameter2.Equals("2"))
                    number2 = GetValueInposition(array, position2 + relativeBase);

                if (parameter3.Equals("2"))
                    number3 = position3 + relativeBase;

                if (array.ContainsKey(number3.ToString()))
                    array[number3.ToString()] = (number1 * number2).ToString();
                else
                    array.Add(number3.ToString(),(number1 * number2).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode2(): {0}", ex.Message);
            }
        }

        private void Opcode3(ref Dictionary<string, string> array, Int64 position1, string parameter1, Int64? input)
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

                Int64 number1 = position1;

                if (parameter1.Equals("2"))
                    number1 = position1 + relativeBase;

                if (array.ContainsKey(number1.ToString()))
                    array[number1.ToString()] = input.ToString();
                else
                    array.Add(number1.ToString(), input.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode3(): {0}", ex.Message);
            }
        }

        private Int64 Opcode4(ref Dictionary<string, string> array, Int64 position, string parameter1)
        {
            Int64 output = 0;
            try
            {
                if (parameter1.Equals("0"))
                {
                    if (array.ContainsKey(position.ToString()))
                        Int64.TryParse(array[(position).ToString()], out output);
                    else
                        Console.WriteLine("no output");
                }
                else if (parameter1.Equals("2"))
                {
                    if (array.ContainsKey((position + relativeBase).ToString()))
                        Int64.TryParse(array[(position + relativeBase).ToString()], out output);
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

        private Int64 Opcode5(ref Dictionary<string, string> array, Int64 position1, Int64 position2, string parameter1, string parameter2)
        {
            Int64 nextPosition = -1;
            try
            {
                Int64 number1 = position1;
                Int64 number2 = position2;

                if (parameter1.Equals("0"))
                    number1 = GetValueInposition(array, position1);
                else if (parameter1.Equals("2"))
                    number1 = GetValueInposition(array, position1 + relativeBase);

                if (parameter2.Equals("0"))
                    number2 = GetValueInposition(array, position2);
                else if (parameter2.Equals("2"))
                    number2 = GetValueInposition(array, position2 + relativeBase);

                if (!number1.Equals(0))
                    nextPosition = number2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode5(): {0}", ex.Message);
            }
            return nextPosition;
        }

        private Int64 Opcode6(ref Dictionary<string, string> array, Int64 position1, Int64 position2, string parameter1, string parameter2)
        {
            Int64 nextPosition = -1;
            try
            {
                Int64 number1 = position1;
                Int64 number2 = position2;

                if (parameter1.Equals("0"))
                    number1 = GetValueInposition(array, position1);
                else if (parameter1.Equals("2"))
                    number1 = GetValueInposition(array, position1 + relativeBase);

                if (parameter2.Equals("0"))
                    number2 = GetValueInposition(array, position2);
                else if (parameter2.Equals("2"))
                    number2 = GetValueInposition(array, position2 + relativeBase);

                if (number1.Equals(0))
                    nextPosition = number2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode6(): {0}", ex.Message);
            }
            return nextPosition;
        }

        private Int64 Opcode7(ref Dictionary<string, string> array, Int64 position1, Int64 position2, Int64 position3, string parameter1, string parameter2, string parameter3)
        {
            Int64 nextPosition = 0;
            try
            {
                Int64 number1 = position1;
                Int64 number2 = position2;
                Int64 number3 = position3;

                if (parameter1.Equals("0"))
                    number1 = GetValueInposition(array, position1);
                else if (parameter1.Equals("2"))
                    number1 = GetValueInposition(array, position1 + relativeBase);

                if (parameter2.Equals("0"))
                    number2 = GetValueInposition(array, position2);
                else if (parameter2.Equals("2"))
                    number2 = GetValueInposition(array, position2 + relativeBase);

                if (parameter3.Equals("2"))
                    number3 = position3 + relativeBase;

                if (number1 < number2)
                    array[number3.ToString()] = "1";
                else
                    array[number3.ToString()] = "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode7(): {0}", ex.Message);
            }
            return nextPosition;
        }

        private Int64 Opcode8(ref Dictionary<string, string> array, Int64 position1, Int64 position2, Int64 position3, string parameter1, string parameter2, string parameter3)
        {
            Int64 nextPosition = 0;
            try
            {
                Int64 number1 = position1;
                Int64 number2 = position2;
                Int64 number3 = position3;

                if (parameter1.Equals("0"))
                    number1 = GetValueInposition(array, position1);
                else if (parameter1.Equals("2"))
                    number1 = GetValueInposition(array, position1 + relativeBase);

                if (parameter2.Equals("0"))
                    number2 = GetValueInposition(array, position2);
                else if (parameter2.Equals("2"))
                    number2 = GetValueInposition(array, position2 + relativeBase);

                if (parameter3.Equals("2"))
                    number3 = position3 + relativeBase;

                if (number1.Equals(number2))
                    array[number3.ToString()] = "1";
                else
                    array[number3.ToString()] = "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode8(): {0}", ex.Message);
            }
            return nextPosition;
        }

        private void Opcode9(ref Dictionary<string, string> array, Int64 position1, string parameter1)
        {
            try
            {
                Int64 number1 = position1;

                if (parameter1.Equals("0") && array.ContainsKey(position1.ToString()))
                {
                    if (array.ContainsKey(position1.ToString()))
                        Int64.TryParse(array[position1.ToString()], out number1);
                    else
                        number1 = 0;
                }
                else if (parameter1.Equals("2"))
                {
                    if (array.ContainsKey((position1 + relativeBase).ToString()))
                        Int64.TryParse(array[(position1 + relativeBase).ToString()], out number1);
                    else
                        number1 = 0;
                }
                
                relativeBase += number1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode9(): {0}", ex.Message);
            }
        }
    }

    public class ResultOpcode
    {
        public Int64 output = 0;
        public Int64 state = 0;
        public bool finished = false;
    }
}
