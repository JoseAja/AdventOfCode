using Aoc2020.Code.Days;
using System;

namespace Aoc2020
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
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine("* Choose another number to execute the puzzles of the day *");
                    Console.WriteLine("***********************************************************");
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
            Console.WriteLine("1  --- Day 1:  Report Repair                          ---");
            Console.WriteLine("2  --- Day 2:  Password Philosophy                    ---");
            Console.WriteLine("3  --- Day 3:  Toboggan Trajectory                    ---");
            Console.WriteLine("4  --- Day 4:  Passport Processing                    ---");
            Console.WriteLine("5  --- Day 5:  Binary Boarding                        ---");
            Console.WriteLine("6  --- Day 6:  Custom Customs                         ---");
            Console.WriteLine("7  --- Day 7:  Handy Haversacks                       ---");
            Console.WriteLine("8  --- Day 8:  Handheld Halting                       ---");
            Console.WriteLine("9  --- Day 9:  Encoding Error                         ---");
            Console.WriteLine("10 --- Day 10: Adapter Array                         ---");
            Console.WriteLine("11 --- Day 11: Seating System                        ---");
            Console.WriteLine("12 --- Day 12: Rain Risk                             ---");
            Console.WriteLine("13 --- Day 13: Shuttle Search                        ---");
            Console.WriteLine("14 --- Day 14: Docking Data                          ---");
            Console.WriteLine("15 --- Day 15: Rambunctious Recitation               ---");
            Console.WriteLine("16 --- Day 16: Ticket Translation                    ---");
            Console.WriteLine("17 --- Day 17: Conway Cubes                          ---");
            Console.WriteLine("18 --- Day 18: Operation Order                       ---");
            Console.WriteLine("19 --- Day 19: Monster Messages                      ---");
            Console.WriteLine("20 --- Day 20: ---");
            Console.WriteLine("21 --- Day 21: ---");
            Console.WriteLine("22 --- Day 22: ---");
            Console.WriteLine("23 --- Day 23: ---");
            Console.WriteLine("24 --- Day 24: ---");
            Console.WriteLine("25 --- Day 25: ---");
            Console.WriteLine("q  --- Exit program                                  ---");
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
                case "15":
                    Day15 day15 = new Day15();
                    day15.Execute();
                    break;
                case "16":
                    Day16 day16 = new Day16();
                    day16.Execute();
                    break;
                case "17":
                    Day17 day17 = new Day17();
                    day17.Execute();
                    break;
                case "18":
                    Day18 day18 = new Day18();
                    day18.Execute();
                    break;
                case "19":
                    Day19 day19 = new Day19();
                    day19.Execute();
                    break;
                case "20":
                    Day20 day20 = new Day20();
                    day20.Execute();
                    break;
                case "21":
                    Day21 day21 = new Day21();
                    day21.Execute();
                    break;
                case "22":
                    Day22 day22 = new Day22();
                    day22.Execute();
                    break;
                case "23":
                    Day23 day23 = new Day23();
                    day23.Execute();
                    break;
                case "24":
                    Day24 day24 = new Day24();
                    day24.Execute();
                    break;
                case "25":
                    Day25 day25 = new Day25();
                    day25.Execute();
                    break;
                case "q":
                case "Q":
                    finish = true;
                    break;
                default:
                    Console.WriteLine("Incorrect option. Try again.");
                    break;
            }
            return finish;
        }
    }
}
