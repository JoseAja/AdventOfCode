using AdventOfCode2019.classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.Days
{
    class Day9
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 9: Sensor Boost      ---");
            Console.WriteLine("---------------------------------");
            string[] array = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day9-Input.txt").Split(',');

            IntcodeProgram intcodeProgram = new IntcodeProgram();
            ResultOpcode result = intcodeProgram.IntcodeExtended(array, new List<Int64> { 1});
            Console.WriteLine("Diagnostic code:{0}", result.output);

            string[] array2 = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day9-Input.txt").Split(',');
            IntcodeProgram intcodeProgram2 = new IntcodeProgram();
            ResultOpcode result2 = intcodeProgram2.IntcodeExtended(array2, new List<Int64> { 2 });
            Console.WriteLine("Diagnostic code:{0}", result2.output);
        }
    }
}
