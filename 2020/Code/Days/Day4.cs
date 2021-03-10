using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Aoc2020.Code.Days
{
    class Day4
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 4: Passport Processing            ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day4-Input.txt");
            int validPassportsCount = CountValidPassports(input, true);
            Console.WriteLine("Number of valid passports: {0}", validPassportsCount);
            validPassportsCount = CountValidPassportsAndData(input, true);
            Console.WriteLine("Number of valid passports and data: {0}", validPassportsCount);
        }

        private int CountValidPassports(string[] input, bool ignoreCID)
        {
            int count = 0;
            try
            {
                string data = "";
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Length != 0)
                        data += input[i] + " ";

                    if (input[i].Length == 0 || i == (input.Length - 1))
                    {
                        if (data.Contains("byr:") && data.Contains("iyr:") && data.Contains("eyr:") &&
                            data.Contains("hgt:") && data.Contains("hcl:") && data.Contains("ecl:") &&
                            data.Contains("pid:") && (ignoreCID || data.Contains("cid")))
                            count++;
                        data = "";
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return count;
        }

        private int CountValidPassportsAndData(string[] input, bool ignoreCID)
        {
            int count = 0;
            try
            {

                string data = "";
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].Length != 0)
                        data += input[i] + " ";

                    if (input[i].Length == 0 || i == (input.Length - 1))
                    {
                        Regex regularExressionBYR = new Regex("byr:(19[2-9][0-9]|200[0-2]) ");
                        Regex regularExressionIYR = new Regex("iyr:(201[0-9]|2020) ");
                        Regex regularExressionEYR = new Regex("eyr:(202[0-9]|2030) ");
                        Regex regularExressionHGT = new Regex("hgt:((1[5-8][0-9]|19[0-3])cm|(59|6[0-9]|7[0-6])in) ");
                        Regex regularExressionHCL = new Regex("hcl:#[0-9a-f]{6} ");
                        Regex regularExressionECL = new Regex("ecl:(amb|blu|brn|gry|grn|hzl|oth) ");
                        Regex regularExressionPID = new Regex("pid:[0-9]{9} ");

                        if (regularExressionBYR.IsMatch(data) &&
                            regularExressionIYR.IsMatch(data) &&
                            regularExressionEYR.IsMatch(data) &&
                            regularExressionHGT.IsMatch(data) &&
                            regularExressionHCL.IsMatch(data) &&
                            regularExressionECL.IsMatch(data) &&
                            regularExressionPID.IsMatch(data) &&
                            (ignoreCID || data.Contains("cid")))
                            count++;
                        data = "";
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return count;
        }
    }
}
