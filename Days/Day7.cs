using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day7
    {
        private int maximum = 0;
        private string PhasesA = "";
        private string PhasesB = "";
        private string PhasesC = "";
        private string PhasesD = "";
        private string PhasesE = "";

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 7: Amplification Circuit ---");
            Console.WriteLine("------------------------------------");
            CalculateMaximum(false, 0);
            Console.WriteLine("Highest signal that can be sent to the thrusters (no feedback loop): {0}", maximum);
            //Initialize();
            //CalculateMaximum(true, 0);
            //Console.WriteLine("Highest signal that can be sent to the thrusters (with feedback loop): {0}", maximum);

            //For testing
            //string[] array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
            //int output = ExecuteAmplifier(false, array, "A", 4, 0);
            //array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
            //output = ExecuteAmplifier(false, array, "B", 3, output);
            //array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
            //output = ExecuteAmplifier (false, array, "C", 2, output);
            //array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
            //output = ExecuteAmplifier(false, array, "D", 1, output);
            //array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
            //output = ExecuteAmplifier(false, array, "E", 0, output);
            //Console.WriteLine("Highest signal that can be sent to the thrusters (with feedback loop): {0}", maximum);
        }

        private void Initialize()
        {
            maximum = 0;
            PhasesA = "";
            PhasesB = "";
            PhasesC = "";
            PhasesD = "";
            PhasesE = "";
        }

        private void CalculateMaximum(bool isLoopMode, int input)
        {
            int minPhase = 0;
            int maxPhase = 5;

            //if(isLoopMode)
            //{
            //    minPhase = 5;
            //    maxPhase = 10;
            //}

            string[] originalArray = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
            //List<int> inputarrayA = new List<int>();
            //List<int> inputarrayB = new List<int>();
            //List<int> inputarrayC = new List<int>();
            //List<int> inputarrayD = new List<int>();
            //List<int> inputarrayE = new List<int>();
            for (int a = minPhase; a < maxPhase; a++)
            {
                string[] arrayA = (string[])originalArray.Clone();
                if (!PhasesA.Contains(a.ToString()))
                {
                    //if (inputarrayA.Count == 0)
                    //    inputarrayA = new List<int>() { a };
                    
                    //inputarrayA.Add(input);
                    int outputA = ExecuteAmplifier(isLoopMode, arrayA, "A", a, input);
                    for (int b = minPhase; b < maxPhase; b++)
                    {
                        if (b != a )//&& !PhasesB.Contains(b.ToString()))
                        {
                            string[] arrayB = (string[])arrayA.Clone();

                            //string[] arrayB = (string[])originalArray.Clone();
                            //if (inputarrayB.Count == 0)
                            //    inputarrayB = new List<int>() { b };

                            //inputarrayB.Add(outputA);
                            int outputB = ExecuteAmplifier(isLoopMode, arrayB, "B", b, outputA);
                            for (int c = minPhase; c < maxPhase; c++)
                            {
                                if (c != a && c != b)//&& !PhasesC.Contains(c.ToString()))
                                {
                                    string[] arrayC = (string[])arrayB.Clone();
                                    //string[] arrayC = (string[])originalArray.Clone();
                                    //if (inputarrayC.Count == 0)
                                    //    inputarrayC = new List<int>() { c };

                                    //inputarrayC.Add(outputB);
                                    int outputC = ExecuteAmplifier(isLoopMode, arrayC, "C", c, outputB);
                                    for (int d = minPhase; d < maxPhase; d++)
                                    {
                                        if (d != a && d != b && d != c)// && !PhasesD.Contains(d.ToString()))
                                        {
                                            string[] arrayD = (string[])arrayC.Clone();
                                            //string[] arrayD = (string[])originalArray.Clone();
                                            //if (inputarrayD.Count == 0)
                                            //    inputarrayD = new List<int>() { d };

                                            //inputarrayD.Add(outputC);
                                            int outputD = ExecuteAmplifier(isLoopMode, arrayD, "D", d, outputC);
                                            for (int e = minPhase; e < maxPhase; e++)
                                            {
                                                if (e != a && e != b && e != c && e != d)// && !PhasesE.Contains(e.ToString()))
                                                {
                                                    string[] arrayE = (string[])arrayD.Clone();
                                                    //string[] arrayE = (string[])originalArray.Clone();
                                                    //if (inputarrayE.Count == 0)
                                                    //    inputarrayE = new List<int>() { e };

                                                    //inputarrayE.Add(outputD);
                                                    int outputE = ExecuteAmplifier(isLoopMode, arrayE, "E", e, outputD);
                                                    if (isLoopMode)
                                                        PhasesE += e.ToString();
                                                }
                                            }
                                            if (isLoopMode)
                                                PhasesD += d.ToString();
                                        }
                                    }
                                    if (isLoopMode)
                                        PhasesC += c.ToString();
                                }
                            }
                            if (isLoopMode)
                                PhasesB += b.ToString();
                        }
                    }
                    if (isLoopMode)
                        PhasesA += a.ToString();
                }
            }
        }
         
        private int ExecuteAmplifier(bool isLoopMode, string[] array, string amplifierName, int phase, int input)
        {
            int Output = 0;
            bool isPhaseNumber = true;
            if (phase < 0)
            {
                Console.WriteLine("Define phase setting for amplifier " + amplifierName + ":");
                string phaseSettingString = Console.ReadLine();
                isPhaseNumber = int.TryParse(phaseSettingString, out phase);
            }
            if (isPhaseNumber && ((isLoopMode && (phase < 10) && (phase >= 5)) || (!isLoopMode && (phase < 5) && (phase >= 0))))
            {
                List<int> inputarray = new List<int>() { phase, input };
                Day5 day5 = new Day5();
                Output = day5.IntcodeExtended(array, inputarray);
                //Console.WriteLine("Output from amplifier " + amplifierName + ": {0}", Output);
                if (Output > maximum)
                    maximum = Output;
            }
            else
                Console.WriteLine("Wrong phase setting value");
            return Output;
        }
    }    
}
