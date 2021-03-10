using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Aoc2020.Code.Days
{
    class Day12
    {
        public static Dictionary<char, int[]> DIRECTIONS = new Dictionary<char, int[]>() {
                                                                                        {'N', new int[2] {0, 1}},
                                                                                        {'S', new int[2] {0, -1}},
                                                                                        {'E', new int[2] {1, 0}},
                                                                                        {'W', new int[2] {-1, 0}}
                                                                                      };

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 12: Rain Risk                     ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day12-Input.txt");
            int distance = GetManhattanDistance(input);
            Console.WriteLine("Manhattan distance between that location and the ship's starting position: {0}", distance);
            distance = GetManhattanDistanceWaypoint(input, new[] { 10, 1 });
            Console.WriteLine("Manhattan distance between that location and the ship's starting position with waypoint: {0}", distance);
        }

        private int GetManhattanDistance(string[] input)
        {
            int distance = 0;
            try
            {
                int[] pointInitial = new int[2] { 0, 0 };
                int[] currentFacing = DIRECTIONS['E'];
                for (int i = 0; i < input.Length; i++)
                {
                    int.TryParse(input[i].Substring(1), out int magnitude);
                    int[] pointFinal = new int[2] { 0, 0 };

                    if (input[i][0].Equals('R') || input[i][0].Equals('L'))
                    {
                        if (magnitude == 180)
                            currentFacing = new int[] { (-1) * currentFacing[0], (-1) * currentFacing[1] };
                        else if (magnitude == 90 && input[i][0].Equals('R'))
                            currentFacing = new int[] { currentFacing[1], (-1) * currentFacing[0] };
                        else if (magnitude == 90 && input[i][0].Equals('L'))
                            currentFacing = new int[] { (-1) * currentFacing[1], currentFacing[0] };
                        else if (magnitude == 270 && input[i][0].Equals('R'))
                            currentFacing = new int[] { (-1) * currentFacing[1], currentFacing[0] };
                        else if (magnitude == 270 && input[i][0].Equals('L'))
                            currentFacing = new int[] { currentFacing[1], (-1) * currentFacing[0] };
                    }
                    else
                    {
                        if (!input[i][0].Equals('F'))
                        {
                            int[] move = DIRECTIONS[input[i][0]];
                            pointFinal = new int[] { pointInitial[0] + magnitude * move[0], pointInitial[1] + magnitude * move[1] };
                        }
                        else
                            pointFinal = new int[] { pointInitial[0] + magnitude * currentFacing[0], pointInitial[1] + magnitude * currentFacing[1] };

                        pointInitial[0] = pointFinal[0];
                        pointInitial[1] = pointFinal[1];
                    }
                }

                distance = Math.Abs(pointInitial[0]) + Math.Abs(pointInitial[1]);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return distance;
        }

        private int GetManhattanDistanceWaypoint(string[] input, int[] pointInitialWayPoint)
        {
            int distance = 0;
            try
            {
                int[] pointFinalShip = new int[2] { 0, 0 };
                for (int i = 0; i < input.Length; i++)
                {
                    int.TryParse(input[i].Substring(1), out int magnitude);
                    int[] pointFinal = new int[2] { 0, 0 };

                    if (input[i][0].Equals('R') || input[i][0].Equals('L'))
                    {
                        if (magnitude == 180)
                            pointInitialWayPoint = new int[] { (-1) * pointInitialWayPoint[0], (-1) * pointInitialWayPoint[1] };
                        else if (magnitude == 90 && input[i][0].Equals('R'))
                            pointInitialWayPoint = new int[] { pointInitialWayPoint[1], (-1) * pointInitialWayPoint[0] };
                        else if (magnitude == 90 && input[i][0].Equals('L'))
                            pointInitialWayPoint = new int[] { (-1) * pointInitialWayPoint[1], pointInitialWayPoint[0] };
                        else if (magnitude == 270 && input[i][0].Equals('R'))
                            pointInitialWayPoint = new int[] { (-1) * pointInitialWayPoint[1], pointInitialWayPoint[0] };
                        else if (magnitude == 270 && input[i][0].Equals('L'))
                            pointInitialWayPoint = new int[] { pointInitialWayPoint[1], (-1) * pointInitialWayPoint[0] };
                    }
                    else
                    {
                        if (!input[i][0].Equals('F'))
                        {
                            int[] move = DIRECTIONS[input[i][0]];
                            pointFinal = new int[] { pointInitialWayPoint[0] + magnitude * move[0], pointInitialWayPoint[1] + magnitude * move[1] };
                            pointInitialWayPoint[0] = pointFinal[0];
                            pointInitialWayPoint[1] = pointFinal[1];
                        }
                        else
                            pointFinalShip = new int[] { pointFinalShip[0] + magnitude * pointInitialWayPoint[0], pointFinalShip[1] + magnitude * pointInitialWayPoint[1] };
                    }
                }

                distance = Math.Abs(pointFinalShip[0]) + Math.Abs(pointFinalShip[1]);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return distance;
        }
    }
}
