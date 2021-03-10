using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2020.Code.Days
{
    class Day7
    {

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 7: Handy Haversacks               ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day7-Input.txt");
            string[] inputFiltered = DeleteEmpty(input, "no other bag");
            List<string> containers = FindContainerBag(inputFiltered, "shiny gold bag");
            Console.WriteLine("Number of bag colors: {0}", containers.Count);
            int inside = FindBagsInside(inputFiltered, "shiny gold bag", 1);
            Console.WriteLine("Individual bags inside shiny gold bag: {0}", inside);
        }

        private string[] DeleteEmpty(string[] input, string contentToSearch)
        {
            List<string> listBaggage = new List<string>();
            Regex regularExpression = new Regex("^([a-z\\s]+ bag)s contain ([0-9]\\s{1})?(" + contentToSearch + ")s{0,1}.$");

            for (int i = 0; i < input.Length; i++)
            {
                Match hasNoBags = regularExpression.Match(input[i]);
                if (!hasNoBags.Success)
                    listBaggage.Add(input[i]);
            }

            return listBaggage.ToArray();
        }

        private List<string> FindContainerBag(string[] input, string bagToFind, List<string> containers = null)
        {
            try
            {
                if (containers == null)
                    containers = new List<string>();
                Regex regularExpression = new Regex("^[a-z\\s]+ contain (([a-z0-9,\\s]+)?(([0-9]+)\\s{1})?" + bagToFind + "[s]{0,1}(,\\s)?([a-z0-9,\\s]+)?)+.$", RegexOptions.Compiled);

                List<string> containerBags = new List<string>();
                for (int i = 0; i < input.Length; i++)
                {
                    if (regularExpression.IsMatch(input[i]))
                        containerBags.Add(input[i]);
                }
                containerBags = containerBags.Except(containers).ToList();
                if (containerBags.Count > 0)
                {
                    foreach (string containerBag in containerBags)
                    {
                        string nameContainerBag = containerBag.Substring(0, containerBag.IndexOf("s contain"));
                        if (!containers.Contains(nameContainerBag))
                        {
                            containers.Add(nameContainerBag);
                            List<string> list = FindContainerBag(input, nameContainerBag, containers);
                            containers = containers.Union(list).ToList();
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return containers; 
        }

        private int FindBagsInside(string[] input, string bagToFind, int multiplier)
        {
            int individualbags = 0;
            try
            {
                List<string> containedBags = new List<string>();
                Regex regularExpression = new Regex("^" + bagToFind + "s contain (([0-9]+)\\s{1}[a-z0-9,\\s]+)+.$", RegexOptions.Compiled);
                for (int i = 0; i < input.Length; i++)
                {
                    if (regularExpression.IsMatch(input[i]))
                        containedBags.Add(input[i]);
                }
                if (containedBags.Count > 0)
                {
                    foreach (string containedBag in containedBags)
                    {
                        string nameContainerBag = containedBag.Substring(containedBag.IndexOf("contain")+7).Replace(".","");
                        string[] contents = nameContainerBag.Split(',');
                        foreach(string content in contents)
                        {
                            string numberOfBagsString = content.Trim().Substring(0, content.Trim().IndexOf(" "));
                            string nameOfBag = content.Trim().Substring(content.Trim().IndexOf(" ")).Trim();
                            if (nameOfBag[nameOfBag.Length-1] == 's')
                                nameOfBag = nameOfBag.Substring(0, nameOfBag.Length - 1);
                            int.TryParse(numberOfBagsString, out int numberOfBags);
                            
                            individualbags += (multiplier * numberOfBags) + FindBagsInside(input, nameOfBag, numberOfBags * multiplier); ;
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return individualbags;
        }
    }
}
