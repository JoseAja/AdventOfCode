using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day3
    {
        public Day3()
        { }

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 3: Crossed Wires ---");
            Console.WriteLine("----------------------------");

            string[][] wires = LoadWires();
            if (wires != null)
                CalculateCrossings(wires);
            else
                Console.WriteLine("Wrong input");
        }

        private string[][] LoadWires()
        {
            string[][] wires = null;
            string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day1-Input.txt");
            if (lines.Length == 2)
            {
                wires = new string[][] { lines[0].Split(','), lines[1].Split(',') };
            }
            return wires;
        }

        private ArrayList CalculateCrossings(string[][] wires)
        {
            ArrayList crossings = new ArrayList();

            int[] initialPointWire1 = new int[2]{ 0, 0}; 
            for (int i = 0; i < wires[0].Length; i++)
            {
                int[] vector1 = TransformToVector(wires[0][i]);

                int[] finalPointWire1 = new int[] { initialPointWire1[0] + vector1[0], initialPointWire1[1] + vector1[1] };

                int[] initialPointWire2 = new int[2] { 0, 0 };

                for (int j = 1; j < wires[1].Length; j++)
                {
                    int[] vector2 = TransformToVector(wires[0][i]);

                    int[] finalPointWire2 = new int[] { initialPointWire2[0] + vector2[0], initialPointWire2[1] + vector2[1] };
                    
                    //if()
                    //{
                    
                    //}
                    
                    initialPointWire2 = finalPointWire2;
                }

                initialPointWire1 = finalPointWire1;
            }
            return crossings;
        }

        private void MinManhattanDistance()
        {
            int distance = 0;

            if(distance > 0 )
                Console.WriteLine("Manhattan distance from the central port to the closest intersection is {0}", distance);
            else
                Console.WriteLine("No intersections");
        }

        private int[] TransformToVector(string path)
        {
            int[] point = new int[2] { 0, 0 };
            string direction = path.Substring(0,1);
            int magnitude = 0;
            int.TryParse(path.Substring(1), out magnitude);
            switch (direction)
            {
                case "U":
                    point[1] = magnitude;
                    break;
                case "D":
                    point[1] = (-1) * magnitude;
                    break;
                case "L":
                    point[0] = (-1) * magnitude;
                    break;
                case "R":
                    point[0] = magnitude;
                    break;
            }

            return point;
        }
    }
}


//https://math.stackexchange.com/questions/139600/how-do-i-calculate-euclidean-and-manhattan-distance-by-hand



