using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Day5 day5 = new Day5();
            ResultOpcode result = day5.IntcodeExtended(array, null);
            Console.WriteLine("Diagnostic code:{0}", result.output);
        }
    }
}
