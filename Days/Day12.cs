using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day12
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 12: The N-Body Problem ---");
            Console.WriteLine("----------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day12-Input.txt");

            Console.WriteLine("Introduce the number of steps:");
            string inputString = Console.ReadLine().Trim();
            int inputNumber = 0;
            if (!int.TryParse(inputString, out inputNumber))
                Console.WriteLine("The value is invalid.");
            else
            {
                List<int[]> vectoresPosition = ConvertPositionsPlanetsToArrayInt(input);
                int totalEnergy = BeginTracking(inputNumber, vectoresPosition);
                Console.WriteLine("Total energy of the system: {0}", totalEnergy);
                vectoresPosition = ConvertPositionsPlanetsToArrayInt(input);
                double step = ExactPointInTime(vectoresPosition);
                long a = Int64.Parse(step.ToString());
                Console.WriteLine("Steps before a match of previous step in time:" + step.ToString("0.0"));
            }
        }

        private int BeginTracking(int steps, List<int[]> vectorsInitialPosition)
        {
            int totalEnergy = 0;
            try
            {
                List<int[]> vectorsFinalPosition = vectorsInitialPosition;
                List<int[]> arraySpeeds = new List<int[]> { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
                for (int i = 0; i < steps; i++)
                {
                    arraySpeeds = CalculateVelocity(vectorsFinalPosition, arraySpeeds);
                    vectorsFinalPosition = CalculateNewPosition(vectorsFinalPosition, arraySpeeds);
                }

                totalEnergy = CalculateTotalEnergy(vectorsFinalPosition, arraySpeeds);
            }
            catch { }
            return totalEnergy;
        }

        private List<int[]> ConvertPositionsPlanetsToArrayInt(string[] input)
        {
            List<int[]> vectorsPosition = new List<int[]>();
            try
            {
                Regex expressionPosition = new Regex("^<x=(-?[0-9]+), y=(-?[0-9]+), z=(-?[0-9]+)>$");

                for (int i = 0; i < input.Length; i++)
                {
                    int[] arrayPositions = new int[3];
                    MatchCollection coleccion = expressionPosition.Matches(input[i]);
                    if (coleccion.Count == 1 && coleccion[0].Groups.Count == 4)
                    {
                        int.TryParse(coleccion[0].Groups[1].Value, out arrayPositions[0]);
                        int.TryParse(coleccion[0].Groups[2].Value, out arrayPositions[1]);
                        int.TryParse(coleccion[0].Groups[3].Value, out arrayPositions[2]);
                        vectorsPosition.Add(arrayPositions);
                    }
                }
            }
            catch { }
            return vectorsPosition;
        }

        private List<int[]> CalculateVelocity(List<int[]> input, List<int[]> arraySpeeds)
        {
            try
            {
                for (int i = 0; i < input.Count; i++)
                {
                    List<int[]> planet = new List<int[]>() { input[i] };
                    List<int[]> otherPlanets = input.Except(planet, new SpeedComprarer()).ToList();

                    otherPlanets.ForEach(p => arraySpeeds[i][0] += GetVelocityChange(input[i][0], p[0]));
                    otherPlanets.ForEach(p => arraySpeeds[i][1] += GetVelocityChange(input[i][1], p[1]));
                    otherPlanets.ForEach(p => arraySpeeds[i][2] += GetVelocityChange(input[i][2], p[2]));
                }
            }
            catch { }
            return arraySpeeds;
        }

        private List<int[]> CalculateNewPosition(List<int[]> oldPosition, List<int[]> inputSpeed)
        {
            try
            {
                for (int i = 0; i < oldPosition.Count; i++)
                {
                    oldPosition[i][0] += inputSpeed[i][0];
                    oldPosition[i][1] += inputSpeed[i][1];
                    oldPosition[i][2] += inputSpeed[i][2];
                }
            }
            catch { }
            return oldPosition;
        }

        private int CalculateTotalEnergy(List<int[]> positions, List<int[]> speeds)
        {
            int total = 0;
            try
            {
                int counter = 0;
                foreach (int[] planetPosition in positions)
                {
                    int pot = Math.Abs(planetPosition[0]) + Math.Abs(planetPosition[1]) + Math.Abs(planetPosition[2]);
                    int kin = Math.Abs(speeds[counter][0]) + Math.Abs(speeds[counter][1]) + Math.Abs(speeds[counter][2]);
                    total += pot * kin;
                    counter++;
                };

            }
            catch { }
            return total;
        }

        private int GetVelocityChange(int planetOrigin, int PlanetDestination)
        {
            int change = 0;
            if (planetOrigin > PlanetDestination)
                change = -1;
            else if (planetOrigin < PlanetDestination)
                change = 1;
            return change;
        }

        private int ExactPointInTimeUnefficient(List<int[]> vectorsPosition)
        {
            bool positionRepeat = false;
            int counter = 0;
            List<List<int[]>> allPositions = new List<List<int[]>>();
            allPositions.Add(CloneList(vectorsPosition.ToList()));
            List<int[]> arraySpeeds = new List<int[]> { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
            do
            {
                counter++;
                arraySpeeds = CalculateVelocity(vectorsPosition, arraySpeeds);
                vectorsPosition = CalculateNewPosition(vectorsPosition, arraySpeeds);
                if (allPositions.Count == 0 || allPositions.FirstOrDefault(p => positionsContains(p, vectorsPosition)) == null)
                    allPositions.Add(CloneList(vectorsPosition.ToList()));
                else
                    positionRepeat = true;
            }
            while (!positionRepeat);
            return counter;
        }

        private bool positionsContains(List<int[]> positionInList, List<int[]> positiontoFind)
        {
            bool exist = true;
            for (int i = 0; i < positiontoFind.Count; i++)
            {
                if (positionInList[i][0] != positiontoFind[i][0] || positionInList[i][1] != positiontoFind[i][1] || positionInList[i][2] != positiontoFind[i][2])
                {
                    exist = false;
                    break;
                }
            }
            return exist;
        }

        private List<int[]> CloneList(List<int[]> original)
        {
            List<int[]> newList = new List<int[]>();

            for (int i = 0; i < original.Count; i++)
            {
                int[] o = (int[])original[i].Clone();
                newList.Add(o);
            }

            return newList;
        }

        private double ExactPointInTime(List<int[]> vectorsPosition)
        {
            double counter = 0;
            double xPeriod = 0;
            double yPeriod = 0;
            double zPeriod = 0;

            List<int> initialX = new List<int>() { vectorsPosition[0][0], vectorsPosition[1][0], vectorsPosition[2][0], vectorsPosition[3][0] };
            List<int> initialY = new List<int>() { vectorsPosition[0][1], vectorsPosition[1][1], vectorsPosition[2][1], vectorsPosition[3][1] };
            List<int> initialZ = new List<int>() { vectorsPosition[0][2], vectorsPosition[1][2], vectorsPosition[2][2], vectorsPosition[3][2] };

            List<int[]> arraySpeeds = new List<int[]> { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
            do
            {
                counter++;
                arraySpeeds = CalculateVelocity(vectorsPosition, arraySpeeds);
                vectorsPosition = CalculateNewPosition(vectorsPosition, arraySpeeds);

                if (xPeriod == 0 && 
                    (initialX[0] == vectorsPosition[0][0] && arraySpeeds[0][0] == 0) && 
                    (initialX[1] == vectorsPosition[1][0] && arraySpeeds[1][0] == 0) && 
                    (initialX[2] == vectorsPosition[2][0] && arraySpeeds[2][0] == 0) && 
                    (initialX[3] == vectorsPosition[3][0] && arraySpeeds[3][0] == 0))
                    xPeriod = counter;
                if (yPeriod == 0 && 
                    (initialY[0] == vectorsPosition[0][1] && arraySpeeds[0][1] == 0) && 
                    (initialY[1] == vectorsPosition[1][1] && arraySpeeds[1][1] == 0) && 
                    (initialY[2] == vectorsPosition[2][1] && arraySpeeds[2][1] == 0) && 
                    (initialY[3] == vectorsPosition[3][1] && arraySpeeds[3][1] == 0))
                    yPeriod = counter;
                if (zPeriod == 0 && 
                    (initialZ[0] == vectorsPosition[0][2] && arraySpeeds[0][2] == 0) && 
                    (initialZ[1] == vectorsPosition[1][2] && arraySpeeds[1][2] == 0) && 
                    (initialZ[2] == vectorsPosition[2][2] && arraySpeeds[2][2] == 0) && 
                    (initialZ[3] == vectorsPosition[3][2] && arraySpeeds[3][2] == 0))
                    zPeriod = counter;
            }
            while (xPeriod == 0 || yPeriod == 0 || zPeriod == 0);

            counter = CalculateMCM(new double[]{ xPeriod, yPeriod, zPeriod});

            return counter;
        }

        private double CalculateMCM(double[] numbers)
        {
            double result = 0;
            Dictionary<double, double> divisorsForTotal = new Dictionary<double, double>();
            for (int i = 0; i < numbers.Length; i++)
            {
                Dictionary<double, double> divisors = CalculateDivisors(numbers[i]);               

                    List<KeyValuePair<double, double>> existingElements = divisors.Where(d => divisorsForTotal.ContainsKey(d.Key)).ToList();
                    foreach (KeyValuePair<double, double> element in existingElements)
                    {
                        if (divisorsForTotal[element.Key] < element.Value)
                        {
                            divisorsForTotal.Remove(element.Key);
                            divisorsForTotal.Add(element.Key, element.Value);
                        }
                    }

                    List<KeyValuePair<double, double>> noExistingElements = divisors.Where(d => !divisorsForTotal.ContainsKey(d.Key)).ToList();
                    foreach (KeyValuePair<double, double> element in noExistingElements)
                    {
                        divisorsForTotal.Add(element.Key, element.Value);
                    }
            }

            foreach (KeyValuePair<double, double> element in divisorsForTotal)
            {
                if(result== 0)
                    result = Math.Pow(element.Key,element.Value);
                else
                    result *= Math.Pow(element.Key, element.Value);
            } 
            
            return result;
        }

        private Dictionary<double, double> CalculateDivisors(double number)
        {
            Dictionary<double, double> divisors = new Dictionary<double, double>();
            double divisor = 2;
            do
            {
                if (number % divisor == 0)
                {
                    if (divisors.ContainsKey(divisor))
                        divisors[divisor]++;
                    else
                        divisors.Add(divisor, 1);
                    number = number / divisor;
                }
                else
                    divisor++;
            }
            while (number > 1);
            return divisors;
        }
    }
    public class SpeedComprarer: IEqualityComparer<int[]>
    {
        public bool Equals (int[] a , int[] b)
        {
            return (a[0]== b[0] && a[1] == b[1] && a[2] == b[2]);
        }

        public int GetHashCode(int[] obj)
        {
            return obj[0].GetHashCode();
        }
    }
}
