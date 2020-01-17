using System;
using System.IO;

namespace AdventOfCode2019.Days
{
    class Day2
    {
        public Day2()
        { }

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 2: 1202 Program Alarm ---");
            Console.WriteLine("---------------------------------");

            Intcode1();
            Intcode2();
        }
        
        private void Intcode1()
        {
            try
            {
                string[] array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day2-Input.txt").Split(',');
                PrintArray("Input:", array);
                if (array.Length > 0 && !array[0].Equals("99"))
                {
                    if (array.Length >= 3)
                    {
                        array[1] = "12";
                        array[2] = "2";
                    }

                    int i = 0;
                    do
                    {
                        if ((array[i].Equals("1") || array[i].Equals("2")) && array.Length > (i + 3))
                        {
                            int position1 = 0;
                            int.TryParse(array[i + 1], out position1);
                            int position2 = 0;
                            int.TryParse(array[i + 2], out position2);
                            int position3 = 0;
                            int.TryParse(array[i + 3], out position3);

                            if (array[i].Equals("1"))
                            {
                                array = Opcode1(array, position1, position2, position3);
                                i += 4;
                            }
                            else
                            {
                                array = Opcode2(array, position1, position2, position3);
                                i += 4;
                            }
                        }
                        else if (!array[i].Equals("1") && !array[i].Equals("2"))
                        {
                            Console.WriteLine("Something went wrong");
                            break;
                        }
                        else
                            i++;
                    }
                    while (array.Length > i && array[i] != "99");

                    PrintArray("Output:", array);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Intcode(): {0}", ex.Message);
            }
        }

        private void Intcode2()
        {
            try
            {
                string[] arrayInput = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day2-Input.txt").Split(',');
                
                if (arrayInput.Length > 0 && !arrayInput[0].Equals("99"))
                {
                    if (arrayInput.Length >= 3)
                    {
                        bool finish = false;
                        for (int k = 0; k <= 99; k++)
                        {                            
                            for (int j = 0; j <= 99; j++)
                            {
                                string[] array = (string[]) arrayInput.Clone();
                                array[1] = k.ToString(); // noun
                                array[2] = j.ToString(); // verb
                                int i = 0; // instruction pointer
                                do
                                {
                                    if ((array[i].Equals("1") || array[i].Equals("2")) && array.Length > (i + 3))
                                    {
                                        int position1 = 0;
                                        int.TryParse(array[i + 1], out position1);
                                        int position2 = 0;
                                        int.TryParse(array[i + 2], out position2);
                                        int position3 = 0;
                                        int.TryParse(array[i + 3], out position3);

                                        if (array[i].Equals("1"))
                                        {
                                            array = Opcode1(array, position1, position2, position3);
                                            i += 4;
                                        }
                                        else
                                        {
                                            array = Opcode2(array, position1, position2, position3);
                                            i += 4;
                                        }
                                    }
                                    else if (!array[i].Equals("1") && !array[i].Equals("2"))
                                    {
                                        Console.WriteLine("Something went wrong");
                                        break;
                                    }
                                    else
                                        i++;

                                    if (array[0].Equals("19690720"))
                                    {
                                        string output = "";
                                        output = array[0];
                                        
                                        Console.WriteLine("Output: {0}", output);
                                        Console.WriteLine("noun: {0}", k);
                                        Console.WriteLine("verb: {0}", j);
                                        Console.WriteLine("result: {0}", ((100*k)+ j).ToString());
                                        finish = true;
                                    }
                                }
                                while (array.Length > i && array[i] != "99");

                                if (finish)
                                    break;
                            }
                            if (finish)
                                break;

                        }

                    }   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Intcode(): {0}", ex.Message);
            }
        }

        private string[] Opcode1(string[] array, int position1, int position2, int position3)
        {
            try
            {
                int number1 = 0;
                int number2 = 0;
                int.TryParse(array[position1], out number1);
                int.TryParse(array[position2], out number2);
                if(array.Length > position3)
                    array[position3] = (number1 + number2).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in Opcode1(): {0}", ex.Message);
            }
            return array;
        }

        private string[] Opcode2(string[] array, int position1, int position2, int position3)
        {
            try
            {
                int number1 = 0;
                int number2 = 0;
                int.TryParse(array[position1], out number1);
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

        private void PrintArray(string message, string[] array)
        {
            Console.WriteLine("");
            Console.WriteLine(message);
            
            int count = 0;
            for(int i = 0; i < array.Length; i++)
            {               
                if (count < 3 && !array[i].Equals("99"))
                {
                    Console.Write(array[i]);
                    if ((array.Length - 1) > i)
                        Console.Write(", ");
                    count++;
                }
                else
                {
                    if ((array.Length - 1) > i)
                        Console.WriteLine(array[i] + ", ");
                    else
                        Console.WriteLine(array[i]);
                    count = 0;
                }
                
            }
            Console.WriteLine("");
        }
    }
}
