using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdventOfCode2019.Days
{
    class Day10
    {
        int maximumAsteroidsView = 0;
        string positionStation = "";

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 10: Monitoring Station ---");
            Console.WriteLine("----------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day10-Input.txt");
            List<Tuple<int, int>> asteroidPositions = ProcessInput(input);
            CheckViews(asteroidPositions);
            //Day5 day5 = new Day5();
            //int output = day5.IntcodeExtended(array, null);
            //Console.WriteLine("Diagnostic code:{0}", output);
        }

        private List<Tuple<int, int>> ProcessInput(string[] input)
        {
            List<Tuple<int, int>> asteroidPositions = new List<Tuple<int, int>>();
            try
            {
                int columns = input[0].Length;
                int rows = input.Length;
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        if (input[y][x] == '#')
                        {
                            asteroidPositions.Add(new Tuple<int, int>(x, y));
                            //int currentViews = CheckViews(x, y, rows, columns);
                            //if (currentViews > maximumAsteroidsView)
                            //{
                            //    maximumAsteroidsView = currentViews;
                            //    positionStation = string.Format("[{0},{1}]", x, y);
                            //}
                        }
                    }
                }
            }
            catch { }
            return asteroidPositions;
        }

        private int CheckViews(List<Tuple<int, int>> asteroidPositions)
        {
            int views = 0;
            try
            {
                int count = 0;
                for (int i = 0; i < asteroidPositions.Count; i++)
                {
                    if (asteroidPositions.Where(p => p.Item1 == asteroidPositions[i].Item1 && p.Item2 > asteroidPositions[i].Item2).Count() > 0)
                        count++;
                    if( asteroidPositions.Where(p => p.Item1 == asteroidPositions[i].Item1 && p.Item2 < asteroidPositions[i].Item2).Count() > 0)
                        count++;
                    if (asteroidPositions.Where(p => p.Item2 == asteroidPositions[i].Item2 && p.Item1 > asteroidPositions[i].Item1).Count() > 0)
                        count++;
                    if (asteroidPositions.Where(p => p.Item2 == asteroidPositions[i].Item2 && p.Item1 < asteroidPositions[i].Item1).Count() > 0)
                        count++;
                }
                ////for (int i = 0; i < rows; i++)
                ////{
                ////    for (int j = 0; j < columns; j++)
                ////    {
                ////        if (y != i && x != j)
                ////        {

                ////        }
                ////    }
                ////}

                //for (int i = position.Item1; i <= 0; i--)
                //{
                //    for (int j = (position.Item2 - 1); j <=0 ; j--)
                //    {
                //        //if (y != i && x != j)
                //        //{

                //        //}
                //    }
                //    for (int j = (position.Item2 + 1); j < columns; j++)
                //    {
                //        //if (y != i && x != j)
                //        //{

                //        //}
                //    }
                //}
                //for (int i = (position.Item1 + 1); i < rows; i++)
                //{
                //    //for (int j = 0; j < columns; j++)
                //    //{
                //    //    if (y != i && x != j)
                //    //    {

                //    //    }
                //    //}
                //}
            }
            catch { }
            return views;
        }
    }
}
