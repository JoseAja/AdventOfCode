using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2020.Code.Days
{
    class Day11
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 11: Seating System               ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day11-Input.txt");
            int occupiedSeats = SimulateSeatingArea(input, true);
            Console.WriteLine("Number of occupied seats: {0}", occupiedSeats);
            occupiedSeats = SimulateSeatingArea(input, false);
            Console.WriteLine("Number of occupied seats: {0}", occupiedSeats);
        }

        private int SimulateSeatingArea(string[] input, bool adjacentSeats)
        {
            int occupiedSeats = 0;
            try
            {
                bool stabilized = false;
                while (!stabilized)
                {
                    if (adjacentSeats)
                        stabilized = !ModelPeopleRound(ref input);
                    else
                        stabilized = !ModelPeopleRoundNoAdjacent(ref input);
                }

                for (int i = 0; i < input.Length; i++)
                {
                    occupiedSeats += input[i].Count(s => s == '#');
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return occupiedSeats;
        }

        private bool ModelPeopleRound(ref string[] input)
        {
            bool change = false;
            try 
            {
                List<string> newSeats = new List<string>();
                for (int i = 0; i < input.Length; i++)
                {
                    string rowSeats = "";
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        if (!input[i][j].Equals('.'))
                        {
                            int numberOccupied = 0;
                            bool isEmpty = input[i][j].Equals('L');

                            if (i > 0) // previous row
                            {
                                if (j != 0)
                                    numberOccupied += input[i - 1][j - 1].Equals('#') ? 1 : 0;
                                numberOccupied += input[i - 1][j].Equals('#') ? 1 : 0;
                                if (j < input[i].Length - 1)
                                    numberOccupied += input[i - 1][j + 1].Equals('#') ? 1 : 0;
                            }
                            if (i < input.Length - 1) // nextrow
                            {
                                if (j != 0)
                                    numberOccupied += input[i + 1][j - 1].Equals('#') ? 1 : 0;
                                numberOccupied += input[i + 1][j].Equals('#') ? 1 : 0;
                                if (j < input[i].Length - 1)
                                    numberOccupied += input[i + 1][j + 1].Equals('#') ? 1 : 0;
                            }
                            // current row
                            if (j != 0)
                                numberOccupied += input[i][j - 1].Equals('#') ? 1 : 0;
                            if (j < input[i].Length - 1)
                                numberOccupied += input[i][j + 1].Equals('#') ? 1 : 0;

                            if (isEmpty && numberOccupied == 0)
                            {
                                rowSeats += "#";
                                change = true;
                            }
                            else if (!isEmpty && numberOccupied >= 4)
                            {
                                rowSeats += "L";
                                change = true;
                            }
                            else
                                rowSeats += input[i][j];
                        }
                        else
                            rowSeats += "."; 
                    }
                    newSeats.Add(rowSeats);
                }
                input = newSeats.ToArray();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return change;
        }

        private bool ModelPeopleRoundNoAdjacent(ref string[] input)
        {
            bool change = false;
            try
            {
                List<string> newSeats = new List<string>();
                for (int i = 0; i < input.Length; i++)
                {
                    string rowSeats = "";
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        if (!input[i][j].Equals('.'))
                        {
                            int numberOccupied = 0;
                            bool isEmpty = input[i][j].Equals('L');

                            //if (i > 0) // previous row
                            //{


                            //    if (j != 0)
                            //        numberOccupied += input[i - 1][j - 1].Equals('#') ? 1 : 0;
                            //    numberOccupied += input[i - 1][j].Equals('#') ? 1 : 0;
                            //    if (j < input[i].Length - 1)
                            //        numberOccupied += input[i - 1][j + 1].Equals('#') ? 1 : 0;
                            //}
                            //if (i < input.Length - 1) // nextrow
                            //{
                            //    if (j != 0)
                            //        numberOccupied += input[i + 1][j - 1].Equals('#') ? 1 : 0;
                            //    numberOccupied += input[i + 1][j].Equals('#') ? 1 : 0;
                            //    if (j < input[i].Length - 1)
                            //        numberOccupied += input[i + 1][j + 1].Equals('#') ? 1 : 0;
                            //}
                            //// current row
                            //if (j != 0)
                            //    numberOccupied += input[i][j - 1].Equals('#') ? 1 : 0;
                            //if (j < input[i].Length - 1)
                            //    numberOccupied += input[i][j + 1].Equals('#') ? 1 : 0;

                            if (isEmpty && numberOccupied == 0)
                            {
                                rowSeats += "#";
                                change = true;
                            }
                            else if (!isEmpty && numberOccupied >= 4)
                            {
                                rowSeats += "L";
                                change = true;
                            }
                            else
                                rowSeats += input[i][j];
                        }
                        else
                            rowSeats += ".";
                    }
                    newSeats.Add(rowSeats);
                }
                input = newSeats.ToArray();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return change;
        }
    }
}
