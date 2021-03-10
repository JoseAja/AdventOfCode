using AdventOfCode2019.classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.Days
{
    class Day7
    {
        private Int64 maximum = 0;
        bool FirstTime = true;

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 7: Amplification Circuit ---");
            Console.WriteLine("------------------------------------");
            CalculateMaximum(0);
            Console.WriteLine("Highest signal that can be sent to the thrusters (no feedback loop): {0}", maximum);
            Initialize();
            CalculateMaximumWithLoop();
            Console.WriteLine("Highest signal that can be sent to the thrusters (with feedback loop): {0}", maximum);
        }

        private void Initialize()
        {
            maximum = 0;
        }

        private void CalculateMaximum(Int64 input)
        {
            int minPhase = 0;
            int maxPhase = 5;

            string[] originalArray = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');

            for (int a = minPhase; a < maxPhase; a++)
            {
                for (int b = minPhase; b < maxPhase; b++)
                {
                    if (b != a)
                    {
                        for (int c = minPhase; c < maxPhase; c++)
                        {
                            if (c != a && c != b)
                            {
                                for (int d = minPhase; d < maxPhase; d++)
                                {
                                    if (d != a && d != b && d != c)
                                    {
                                        for (int e = minPhase; e < maxPhase; e++)
                                        {
                                            if (e != a && e != b && e != c && e != d)
                                            {
                                                ResultOpcode ResultoutputA = ExecuteAmplifier(false, (string[])originalArray.Clone(), "A", a, new List<Int64>() { a, input });
                                                ResultOpcode ResultoutputB = ExecuteAmplifier(false, (string[])originalArray.Clone(), "B", b, new List<Int64>() { b, ResultoutputA.output });
                                                ResultOpcode ResultoutputC = ExecuteAmplifier(false, (string[])originalArray.Clone(), "C", c, new List<Int64>() { c, ResultoutputB.output });
                                                ResultOpcode ResultoutputD = ExecuteAmplifier(false, (string[])originalArray.Clone(), "D", d, new List<Int64>() { d, ResultoutputC.output });
                                                ResultOpcode ResultoutputE = ExecuteAmplifier(false, (string[])originalArray.Clone(), "E", e, new List<Int64>() { e, ResultoutputD.output });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        List<Int64> inputarrayALoop = new List<Int64>();
        List<Int64> inputarrayBLoop = new List<Int64>();
        List<Int64> inputarrayCLoop = new List<Int64>();
        List<Int64> inputarrayDLoop = new List<Int64>();
        List<Int64> inputarrayELoop = new List<Int64>();

        string[] arrayALoop = null;
        string[] arrayBLoop = null;
        string[] arrayCLoop = null;
        string[] arrayDLoop = null;
        string[] arrayELoop = null;

        Int64 stateA = 0;
        Int64 stateB = 0;
        Int64 stateC = 0;
        Int64 stateD = 0;
        Int64 stateE = 0;

        private void CalculateMaximumWithLoop()
        {
            try
            {
                int minPhase = 5;
                int maxPhase = 10;
                for (int a = minPhase; a < maxPhase; a++)
                {
                    for (int b = minPhase; b < maxPhase; b++)
                    {
                        if (b != a)
                        {
                            for (int c = minPhase; c < maxPhase; c++)
                            {
                                if (c != a && c != b)
                                {
                                    for (int d = minPhase; d < maxPhase; d++)
                                    {
                                        if (d != a && d != b && d != c)
                                        {
                                            for (int e = minPhase; e < maxPhase; e++)
                                            {
                                                if (e != a && e != b && e != c && e != d)
                                                {
                                                    StartAmplifier(0, a, b, c, d, e);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void StartAmplifier(Int64 input, int a, int b, int c, int d, int e)
        {
            try
            {
                string[] originalArray = null;

                //Amplifier A
                if (FirstTime)
                {
                    originalArray = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt").Split(',');
                    arrayALoop = (string[])originalArray.Clone();
                    if (inputarrayALoop.Count == 0)
                    {
                        inputarrayALoop = new List<Int64>() { a };
                        inputarrayALoop.Add(input);
                    }
                }
                else
                    inputarrayALoop = new List<Int64>() { input };

                ResultOpcode ResultoutputA = ExecuteAmplifier(true, arrayALoop, "A", a, inputarrayALoop, stateA);
                stateA = ResultoutputA.state;
                
                //Amplifier B
                if (FirstTime)
                {
                    arrayBLoop = (string[])originalArray.Clone();
                    if (inputarrayBLoop.Count == 0)
                    {
                        inputarrayBLoop = new List<Int64>() { b };
                        inputarrayBLoop.Add(ResultoutputA.output);
                    }

                }
                else
                    inputarrayBLoop = new List<Int64>() { ResultoutputA.output };

                ResultOpcode ResultoutputB = ExecuteAmplifier(true, arrayBLoop, "B", b, inputarrayBLoop, stateB);
                stateB = ResultoutputB.state;
                
                //Amplifier C
                if (FirstTime)
                {
                    arrayCLoop = (string[])originalArray.Clone();
                    if (inputarrayCLoop.Count == 0)
                    {
                        inputarrayCLoop = new List<Int64>() { c };
                        inputarrayCLoop.Add(ResultoutputB.output);
                    }
                }
                else
                    inputarrayCLoop = new List<Int64>() { ResultoutputB.output };

                ResultOpcode ResultoutputC = ExecuteAmplifier(true, arrayCLoop, "C", c, inputarrayCLoop, stateC);
                stateC = ResultoutputC.state;
                
                //Amplifier D
                if (FirstTime)
                {
                    arrayDLoop = (string[])originalArray.Clone();
                    if (inputarrayDLoop.Count == 0)
                    {
                        inputarrayDLoop = new List<Int64>() { d };
                        inputarrayDLoop.Add(ResultoutputC.output);
                    }
                }
                else
                    inputarrayDLoop = new List<Int64>() { ResultoutputC.output };

                ResultOpcode ResultoutputD = ExecuteAmplifier(true, arrayDLoop, "D", d, inputarrayDLoop, stateD);
                stateD = ResultoutputD.state;
                
                //Amplifier E
                if (FirstTime)
                {
                    arrayELoop = (string[])originalArray.Clone();
                    if (inputarrayELoop.Count == 0)
                    {
                        inputarrayELoop = new List<Int64>() { e };
                        inputarrayELoop.Add(ResultoutputD.output);
                    }
                }
                else
                    inputarrayELoop = new List<Int64>() { ResultoutputD.output };

                ResultOpcode ResultoutputE = ExecuteAmplifier(true, arrayELoop, "E", e, inputarrayELoop, stateE);
                stateE = ResultoutputE.state;
                FirstTime = false;
                
                if (!ResultoutputE.finished) //If last amplifier hasn't finished, continue with feedback loop
                    StartAmplifier(ResultoutputE.output, a, b, c, d, e);
                else
                {
                    FirstTime = true;

                    stateE = 0;
                    inputarrayELoop = new List<Int64>();

                    stateD = 0;
                    inputarrayDLoop = new List<Int64>();

                    stateC = 0;
                    inputarrayCLoop = new List<Int64>();

                    stateB = 0;
                    inputarrayBLoop = new List<Int64>();

                    stateA = 0;
                    inputarrayALoop = new List<Int64>();
                }
            }
            catch (Exception ex) { }
        }
       
        private ResultOpcode ExecuteAmplifier(bool isLoopMode, string[] array, string amplifierName, int phase, List<Int64> input, Int64 state = 0)
        {
            ResultOpcode result = new ResultOpcode();
            bool isPhaseNumber = true;
            if (phase < 0)
            {
                Console.WriteLine("Define phase setting for amplifier " + amplifierName + ":");
                string phaseSettingString = Console.ReadLine();
                isPhaseNumber = int.TryParse(phaseSettingString, out phase);
            }
            if (isPhaseNumber && ((isLoopMode && (phase < 10) && (phase >= 5)) || (!isLoopMode && (phase < 5) && (phase >= 0))))
            {
                IntcodeProgram intcodeProgram = new IntcodeProgram();
                result = intcodeProgram.IntcodeExtended(array, input, state);
                
                if (amplifierName.Equals("E") && result.output > maximum)
                    maximum = result.output;
            }
            else
                Console.WriteLine("Wrong phase setting value");
            return result;
        }
    }
}
