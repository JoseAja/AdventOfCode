using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aoc2020.Code.Days
{
    class Day5
    {
        List<int> seatsArray = new List<int>();

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 5: Binary Boarding               ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day5-Input.txt");
            int id = CheckSeats(input);
            Console.WriteLine("Highest seat ID: {0}", id);
            int mySeatid = GetMySeat();
            Console.WriteLine("my seat ID: {0}", mySeatid);
        }

        private int CheckSeats(string[] input)
        {
            int highestId = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int currentId = GetSeat(input[i]);
                if (currentId > highestId)
                    highestId = currentId;
                seatsArray.Add(currentId);
            }
            return highestId;
        }

        private int GetSeat(string input)
        {
            int rowPatternInt = 0;
            int seatPatternInt = 0;
            try
            {
                string rowPattern = input.Substring(0, 7).Replace("B", "1").Replace("F", "0");
                rowPatternInt = Convert.ToInt32(rowPattern, 2);

                string seatPattern = input.Substring(7).Replace("R", "1").Replace("L", "0");
                seatPatternInt = Convert.ToInt32(seatPattern, 2);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return (rowPatternInt * 8) + seatPatternInt;
        }

        private int GetMySeat()
        {
            int myseat = 0;
            List<int> missingSeats = Enumerable.Range(0, 1024).Except(seatsArray).ToList();
            for (int i = 0; i < missingSeats.Count; i++)
            {
                if (seatsArray.Contains(missingSeats[i] - 1) && seatsArray.Contains(missingSeats[i] + 1))
                {
                    myseat = missingSeats[i];
                    break;
                }
            }
            return myseat;
        }
    }
}
