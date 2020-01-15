using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day1
    {
        public Day1()
        { }

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 1: The Tyranny of the Rocket Equation ---");
            Console.WriteLine("-------------------------------------------------");

            Day1FirstPuzzleFileInput();
            Day1SecondPuzzle();
        }

        private void Day1FirstPuzzleFileInput()
        {
            try
            {
                double total = 0;
                string[] lines =  File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day1-Input.txt");
                for (int i = 0; i< lines.Length;  i++)
                {
                    double mass = 0;
                    if (double.TryParse(lines[i].Trim(), out mass))
                    {
                        double fuel = Math.Floor(mass / 3) - 2;
                        Console.WriteLine("");
                        Console.WriteLine("For a mass of {0}, the fuel required is {1}.", mass, fuel.ToString());
                        total += fuel;
                    }
                    else
                        Console.WriteLine("The module mass is incorrect.");
                }
                Console.WriteLine("");
                Console.WriteLine("the fuel requirements for all of the modules is {0}", total.ToString());
                Console.WriteLine("");
            }
            catch (Exception ex) 
            {
                Console.WriteLine("There was an error: {0}", ex.Message);
            }
        }

        private void Day1FirstPuzzleUserInput()
        {
            bool run = true;
            double mass = 0;
            double total = 0;
            string option = "";
            while (run)
            {
                Console.WriteLine("Please introduce module mass or press \"q\" key to exit:");
                Console.WriteLine("");
                option = Console.ReadLine().Trim();
                if (option.ToLower().Equals("q"))
                    run = false;
                else
                {
                    if (double.TryParse(option, out mass))
                    {
                        double fuel = Math.Floor(mass / 3) - 2;
                        Console.WriteLine("");
                        Console.WriteLine("For a mass of {0}, the fuel required is {1}.", mass, fuel.ToString());
                        Console.WriteLine("");
                        total += fuel;
                    }
                    else
                        Console.WriteLine("The module mass is incorrect.");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("the fuel requirements for all of the modules is {0}", total.ToString());
            Console.WriteLine("");
        }

        private void Day1SecondPuzzle()
        { 
        
        }
    }
}
