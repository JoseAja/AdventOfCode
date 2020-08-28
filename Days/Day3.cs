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
        public static Dictionary<string, int[]> DIRECTIONS = new Dictionary<string, int[]>() {
                                                                                        {"U", new int[2] {0, 1}},
                                                                                        {"D", new int[2] {0, -1}},
                                                                                        {"R", new int[2] {1, 0}},
                                                                                        {"L", new int[2] {-1, 0}}
                                                                                      };

        List<int> steps = new List<int>();

        public Day3()
        { }

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 3: Crossed Wires ---");
            Console.WriteLine("----------------------------");

            string[][] wires = LoadWires();
            if (wires != null && wires.Length == 2)
            {
                int distance = CalculateMinManhattanDistance(wires);
                PrintManhattanDistance(distance);
                PrintBestSteps();
            }
            else
                Console.WriteLine("Wrong input");
        }

        private string[][] LoadWires()
        {
            string[][] wires = null;
            string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day3-Input.txt");
            if (lines.Length == 2)
                wires = new string[][] { lines[0].Split(','), lines[1].Split(',') };

            return wires;
        }

        // https://math.stackexchange.com/questions/139600/how-do-i-calculate-euclidean-and-manhattan-distance-by-hand
        private int CalculateMinManhattanDistance(string[][] wires)
        {
            int minDistance = -1;

            List<int[]> pointsWire1 = TransformToArrayOfPoints(wires[0]);
            List<int[]> pointsWire2 = TransformToArrayOfPoints(wires[1]);
            List<int[]> crossings = GetCrossings(pointsWire1, pointsWire2);
            foreach (int[] point in crossings)
            {
                int actualDistance = Math.Abs(point[0]) + Math.Abs(point[1]); 
                if (actualDistance < minDistance || (minDistance < 0))
                    minDistance = actualDistance;
            }

            return minDistance;
        }

        private List<int[]> GetCrossings(List<int[]> pointsWire1, List<int[]> pointsWire2)
        {
            List<int[]> crossings = new List<int[]>();
            try
            {
                for (int i = 0; i < pointsWire1.Count; i++)
                {
                    for (int j = 0; j < pointsWire2.Count; j++)
                    {
                        if (pointsWire1[i][0] == pointsWire2[j][0] && pointsWire1[i][1] == pointsWire2[j][1])
                        {
                            crossings.Add(pointsWire1[i]);
                            steps.Add(i+j+2);
                        }
                    }
                }
            }
            catch { }
            return crossings;
        }

        private void PrintManhattanDistance(int distance)
        {
            if (distance > 0)
                Console.WriteLine("Manhattan distance from the central port to the closest intersection is {0}", distance);
            else
                Console.WriteLine("No intersections");
        }

        private void PrintBestSteps()
        {
            if (steps.Count>0)
                Console.WriteLine("fewest combined steps are {0}", steps.Min());
            else
                Console.WriteLine("No steps");
        }

        private List<int[]> TransformToArrayOfPoints(string[] wire)
        {
            List<int[]> ListPoints = new List<int[]>();
            int[] pointInitial = new int[2] { 0, 0 };

            for (int i = 0; i < wire.Length; i++)
            {                
                string direction = wire[i].Substring(0, 1);
                int magnitude = 0;
                int.TryParse(wire[i].Substring(1), out magnitude);
                for (int j = 0; j < magnitude; j++)
                {
                    int[] pointFinal = new int[2] { 0, 0 };
                    int[] a = DIRECTIONS[direction];
                    pointFinal[0] = pointInitial[0] + a[0];
                    pointFinal[1] = pointInitial[1] + a[1];

                    pointInitial[0] = pointFinal[0];
                    pointInitial[1] = pointFinal[1];
                    ListPoints.Add(pointFinal);
                }                
            }

            return ListPoints;
        }
    }
}