using AdventOfCode2019.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
            while (!programExecuted);
            Console.ReadLine();
        }

        static private void WriteIntroduction()
        {
            Console.WriteLine("*********************************************************");
            Console.WriteLine("");
            Console.WriteLine("Choose a number to execute the puzzles of the day:");
            Console.WriteLine("");
            Console.WriteLine("1 --- Day 1: The Tyranny of the Rocket Equation ---");
            Console.WriteLine("2 --- Day 2: 1202 Program Alarm                 ---");
            Console.WriteLine("3 --- Day 3: Crossed Wires                      ---");
            Console.WriteLine("4 --- Day 4: Secure Container                   ---");
            Console.WriteLine("5 --- Day 5: Sunny with a Chance of Asteroids   ---");
            Console.WriteLine("6 --- Day 6: Universal Orbit Map                ---");
            Console.WriteLine("7 --- Day 7: Amplification Circuit              ---");
            Console.WriteLine("8 --- Day 8: Space Image Format                 ---");
            Console.WriteLine("9 --- Day 9: Sensor Boost                       ---");
            Console.WriteLine("");
            Console.WriteLine("*********************************************************");
        }

        static private bool LoadPuzzles(string option)
        {
            bool programExecuted = false;
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
                    //case "10":
                    //    break;
                    //case "11":
                    //    break;
            }
            programExecuted = true;
            return true;
        }
    }
}
