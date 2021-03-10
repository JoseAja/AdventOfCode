using AdventOfCode2019.classes;
using System;
using System.IO;

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
                IntcodeProgram intcodeProgram = new IntcodeProgram();
                Int64 output = intcodeProgram.Intcode(array, null);
                Console.WriteLine("Diagnostic code:{0}", output);
            }
        }

    }

}