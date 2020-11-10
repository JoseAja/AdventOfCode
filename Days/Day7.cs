using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.Days
{
    class Day7
    {
        private int maximum = 0;
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

        private void CalculateMaximum(int input)
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
                                                ResultOpcode ResultoutputA = ExecuteAmplifier(false, (string[])originalArray.Clone(), "A", a, new List<int>() { a, input });
                                                ResultOpcode ResultoutputB = ExecuteAmplifier(false, (string[])originalArray.Clone(), "B", b, new List<int>() { b, ResultoutputA.output });
                                                ResultOpcode ResultoutputC = ExecuteAmplifier(false, (string[])originalArray.Clone(), "C", c, new List<int>() { c, ResultoutputB.output });
                                                ResultOpcode ResultoutputD = ExecuteAmplifier(false, (string[])originalArray.Clone(), "D", d, new List<int>() { d, ResultoutputC.output });
                                                ResultOpcode ResultoutputE = ExecuteAmplifier(false, (string[])originalArray.Clone(), "E", e, new List<int>() { e, ResultoutputD.output });
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

        List<int> inputarrayALoop = new List<int>();
        List<int> inputarrayBLoop = new List<int>();
        List<int> inputarrayCLoop = new List<int>();
        List<int> inputarrayDLoop = new List<int>();
        List<int> inputarrayELoop = new List<int>();

        string[] arrayALoop = null;
        string[] arrayBLoop = null;
        string[] arrayCLoop = null;
        string[] arrayDLoop = null;
        string[] arrayELoop = null;

        int stateA = 0;
        int stateB = 0;
        int stateC = 0;
        int stateD = 0;
        int stateE = 0;

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

        private void StartAmplifier(int input, int a, int b, int c, int d, int e)
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
                        inputarrayALoop = new List<int>() { a };
                        inputarrayALoop.Add(input);
                    }
                }
                else
                    inputarrayALoop = new List<int>() { input };

                ResultOpcode ResultoutputA = ExecuteAmplifier(true, arrayALoop, "A", a, inputarrayALoop, stateA);
                stateA = ResultoutputA.state;
                
                //Amplifier B
                if (FirstTime)
                {
                    arrayBLoop = (string[])originalArray.Clone();
                    if (inputarrayBLoop.Count == 0)
                    {
                        inputarrayBLoop = new List<int>() { b };
                        inputarrayBLoop.Add(ResultoutputA.output);
                    }

                }
                else
                    inputarrayBLoop = new List<int>() { ResultoutputA.output };

                ResultOpcode ResultoutputB = ExecuteAmplifier(true, arrayBLoop, "B", b, inputarrayBLoop, stateB);
                stateB = ResultoutputB.state;
                
                //Amplifier C
                if (FirstTime)
                {
                    arrayCLoop = (string[])originalArray.Clone();
                    if (inputarrayCLoop.Count == 0)
                    {
                        inputarrayCLoop = new List<int>() { c };
                        inputarrayCLoop.Add(ResultoutputB.output);
                    }
                }
                else
                    inputarrayCLoop = new List<int>() { ResultoutputB.output };

                ResultOpcode ResultoutputC = ExecuteAmplifier(true, arrayCLoop, "C", c, inputarrayCLoop, stateC);
                stateC = ResultoutputC.state;
                
                //Amplifier D
                if (FirstTime)
                {
                    arrayDLoop = (string[])originalArray.Clone();
                    if (inputarrayDLoop.Count == 0)
                    {
                        inputarrayDLoop = new List<int>() { d };
                        inputarrayDLoop.Add(ResultoutputC.output);
                    }
                }
                else
                    inputarrayDLoop = new List<int>() { ResultoutputC.output };

                ResultOpcode ResultoutputD = ExecuteAmplifier(true, arrayDLoop, "D", d, inputarrayDLoop, stateD);
                stateD = ResultoutputD.state;
                
                //Amplifier E
                if (FirstTime)
                {
                    arrayELoop = (string[])originalArray.Clone();
                    if (inputarrayELoop.Count == 0)
                    {
                        inputarrayELoop = new List<int>() { e };
                        inputarrayELoop.Add(ResultoutputD.output);
                    }
                }
                else
                    inputarrayELoop = new List<int>() { ResultoutputD.output };

                ResultOpcode ResultoutputE = ExecuteAmplifier(true, arrayELoop, "E", e, inputarrayELoop, stateE);
                stateE = ResultoutputE.state;
                FirstTime = false;
                
                if (!ResultoutputE.finished) //If last amplifier hasn't finished, continue with feedback loop
                    StartAmplifier(ResultoutputE.output, a, b, c, d, e);
                else
                {
                    FirstTime = true;

                    stateE = 0;
                    inputarrayELoop = new List<int>();

                    stateD = 0;
                    inputarrayDLoop = new List<int>();

                    stateC = 0;
                    inputarrayCLoop = new List<int>();

                    stateB = 0;
                    inputarrayBLoop = new List<int>();

                    stateA = 0;
                    inputarrayALoop = new List<int>();
                }
            }
            catch (Exception ex) { }
        }
       
        private ResultOpcode ExecuteAmplifier(bool isLoopMode, string[] array, string amplifierName, int phase, List<int> input, int state = 0)
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
                Day5 day5 = new Day5();
                result = day5.IntcodeExtended(array, input, state);
                
                if (amplifierName.Equals("E") && result.output > maximum)
                    maximum = result.output;
            }
            else
                Console.WriteLine("Wrong phase setting value");
            return result;
        }
    }
}
