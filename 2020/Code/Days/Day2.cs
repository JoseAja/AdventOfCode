using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Aoc2020.Code.Days
{
    class Day2
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 2: Password Philosophy            ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day2-Input.txt");
            int[] result = TestPasswords(input);
            Console.WriteLine("{0} passwords are valid for first policy", result[0]);
            Console.WriteLine("{0} passwords are valid for second policy", result[1]);
        }

        private int[] TestPasswords(string[] input)
        {
            int[] result = new int[] { 0, 0 };
            try 
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (CheckPasswordForFirstPolicy(input[i]))
                        result[0]++;
                    if (CheckPasswordForSecondPolicy(input[i]))
                        result[1]++;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        private bool CheckPasswordForFirstPolicy(string policyAndPassword)
        {
            bool isValid = false;
            try 
            {
                string[] arrayPolicyPass = policyAndPassword.Split(':');
                string[] policy = arrayPolicyPass[0].Split(' ');
                string regularExpression = "^([^" + policy[1] + "]*" + policy[1] + "[^" + policy[1] + "]*){" + policy[0].Replace('-',',')+ "}$";
                Regex checkRegularExpression = new Regex(regularExpression);
                isValid = checkRegularExpression.IsMatch(arrayPolicyPass[1].Trim());
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return isValid;
        }

        private bool CheckPasswordForSecondPolicy(string policyAndPassword)
        {
            bool isValid = false;
            try
            {
                string[] arrayPolicyPass = policyAndPassword.Split(':');
                string[] policy = arrayPolicyPass[0].Split(' ');
                string[] positions = policy[0].Split('-');
                int.TryParse(positions[0], out int first);
                int.TryParse(positions[1], out int second);

                bool existinFirstPosition = arrayPolicyPass[1].Trim()[first-1].ToString().Equals(policy[1]);
                bool existinSecondPosition = arrayPolicyPass[1].Trim()[second-1].ToString().Equals(policy[1]);

                isValid = ((existinFirstPosition && !existinSecondPosition) || (!existinFirstPosition && existinSecondPosition));
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return isValid;
        }
    }
}
