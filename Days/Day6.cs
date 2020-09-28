using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day6
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 6: Universal Orbit Map ---");
            Console.WriteLine("---------------------------------");
            List<string[]> listOfOrbits = LoadInput();
            int orbitCount = GetOrbitCountChecksums(listOfOrbits);
            PrintOrbitCountChecksums(orbitCount);
            int transferCount = OrbitTransfer(listOfOrbits, "YOU", "SAN");
            PrintTransferCount(transferCount);
        }

        private List<string[]> LoadInput()
        {
            List<string[]> listOfOrbits = new List<string[]>();
            try
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day6-Input.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    listOfOrbits.Add(lines[i].Split(')'));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error: {0}", ex.Message);
            }
            return listOfOrbits;
        }

        private int GetOrbitCountChecksums(List<string[]> listOfOrbits)
        {
            int orbitCount = 0;
            try
            {
                List<string> astralObjects = listOfOrbits.Select(o => o[0]).Distinct().ToList();
                astralObjects.AddRange(listOfOrbits.Where(o => !astralObjects.Contains(o[1])).Select(o => o[1]).Distinct().ToList());
                
                for (int i = 0; i < astralObjects.Count; i++)
                {
                    orbitCount += CountIfOrbitSomething(listOfOrbits, astralObjects[i]);
                }
            }
            catch { Console.WriteLine("There was an error calculating Orbit Count Checksum"); }
            return orbitCount;
        }

        private int CountIfOrbitSomething(List<string[]> listOfOrbits, string astralObject)
        {
            int count = 0;
            List<string[]> list = listOfOrbits.Where(o => o[1].Equals(astralObject)).ToList();
            if(list.Count > 0)
            {
                count += list.Count;
                for(int i= 0; i< list.Count; i++)
                    count += CountIfOrbitSomething(listOfOrbits, list[i][0]);
            }
            return count;
        }

        private int OrbitTransfer(List<string[]> listOfOrbits, string origin, string destination)
        {
            int transferCount = 0;
            try
            {
                List<string[]> originPath = GetObjectOrbitsPath(listOfOrbits, "YOU"); // all orbits from You to origin
                List<string[]> santaPath = GetObjectOrbitsPath(listOfOrbits, "SAN"); // all orbits from San to origin

                for (int i = 0; i < originPath.Count; i++)
                {
                    int positionCommonSanta = santaPath.FindIndex(o => o[0] == originPath[i][0]); // find current you path object orbit in san orbits path
                    if(positionCommonSanta>=0) // if object orbits in boths path get the transfer count
                    {
                        transferCount += positionCommonSanta; // counts from santa to common object. 
                        transferCount += i; // count from you to common object
                        break;
                    }
                }
            }
            catch { Console.WriteLine("There was an error calculating transfer count"); }
            return transferCount;
        }

        private List<string[]> GetObjectOrbitsPath(List<string[]> listOfOrbits, string origin)
        {
            List<string[]> originPath = new List<string[]>();
            while (origin.Length > 0)
            {
                string[] OriginOrbit = listOfOrbits.SingleOrDefault(o => o[1].Equals(origin));
                if (OriginOrbit != null)
                {
                    origin = OriginOrbit[0];
                    originPath.Add(OriginOrbit);
                }
                else
                    origin = "";
            }
            return originPath;
        }

        private void PrintOrbitCountChecksums(int value)
        {
            Console.WriteLine("Total number of direct and indirect orbits is {0}", value);
        }
        private void PrintTransferCount(int value)
        {
            Console.WriteLine("Minimum number of orbital trasfers required is {0}", value);
        }
    }
}
