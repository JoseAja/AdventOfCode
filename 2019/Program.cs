using AdventOfCode2019.Days;
using System;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteIntroduction();
            bool programExecuted = false;
            string option = "";
            do
            {
                option = Console.ReadLine();
                programExecuted = LoadPuzzles(option);
                if (!programExecuted)
                {
                    Console.WriteLine("");
                    Console.WriteLine("*********************************************************");
                    Console.WriteLine("Choose another number to execute the puzzles of the day");
                    Console.WriteLine("*********************************************************");
                }
            }
            while (!programExecuted);
        }

        static private void WriteIntroduction()
        {
            Console.WriteLine("*********************************************************");
            Console.WriteLine("");
            Console.WriteLine("Choose a number to execute the puzzles of the day:");
            Console.WriteLine("");
            Console.WriteLine("1  --- Day 1: The Tyranny of the Rocket Equation ---");
            Console.WriteLine("2  --- Day 2: 1202 Program Alarm                 ---");
            Console.WriteLine("3  --- Day 3: Crossed Wires                      ---");
            Console.WriteLine("4  --- Day 4: Secure Container                   ---");
            Console.WriteLine("5  --- Day 5: Sunny with a Chance of Asteroids   ---");
            Console.WriteLine("6  --- Day 6: Universal Orbit Map                ---");
            Console.WriteLine("7  --- Day 7: Amplification Circuit              ---");
            Console.WriteLine("8  --- Day 8: Space Image Format                 ---");
            Console.WriteLine("9  --- Day 9: Sensor Boost                       ---");
            Console.WriteLine("10 --- Day 10: Monitoring Station                ---");
            Console.WriteLine("11 --- Day 11: Space Police                      ---");
            Console.WriteLine("12 --- Day 12: The N-Body Problem                ---");
            Console.WriteLine("13 --- Day 13: Care Package                      ---");
            Console.WriteLine("14 --- Day 14: Space Stoichiometry               ---");
            Console.WriteLine("q  --- Exit program                              ---");
            Console.WriteLine("");
            Console.WriteLine("*********************************************************");
        }

        static private bool LoadPuzzles(string option)
        {
            bool finish = false;
            switch (option)
            {
                case "1":
                    Day1 day1 = new Day1();
                    day1.Execute();
                    break;
                case "2":
                    Day2 day2 = new Day2();
                    day2.Execute();
                    break;
                case "3":
                    Day3 day3 = new Day3();
                    day3.Execute();
                    break;
                case "4":
                    Day4 day4 = new Day4();
                    day4.Execute();
                    break;
                case "5":
                    Day5 day5 = new Day5();
                    day5.Execute();
                    break;
                case "6":
                    Day6 day6 = new Day6();
                    day6.Execute();
                    break;
                case "7":
                    Day7 day7 = new Day7();
                    day7.Execute();
                    break;
                case "8":
                    Day8 day8 = new Day8();
                    day8.Execute();
                    break;
                case "9":
                    Day9 day9 = new Day9();
                    day9.Execute();
                    break;
                case "10":
                    Day10 day10 = new Day10();
                    day10.Execute();
                    break;
                case "11":
                    Day11 day11 = new Day11();
                    day11.Execute();
                    break;
                case "12":
                    Day12 day12 = new Day12();
                    day12.Execute();
                    break;
                case "13":
                    Day13 day13 = new Day13();
                    day13.Execute();
                    break;
                case "14":
                    Day14 day14 = new Day14();
                    day14.Execute();
                    break;
                case "q":
                case "Q":
                    finish = true;
                    break;
            }
            return finish;
        }
    }
}
