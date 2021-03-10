using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aoc2020.Code.Days
{
    class Day16
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 16: Ticket Translation            ---");
            Console.WriteLine("---------------------------------------------");
            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Files\\Day16-Input.txt");
            List<Limit> rules = GetRules(input);
            TicketsValidation validation = CheckNearByTickets(input, rules);
            Console.WriteLine("Ticket scanning error rate: {0}", validation.ErrorRate);
            List<KeyValuePair<string, int>> fields = DetermineFields(validation.ValidTickets, rules);
            double multiplication = CheckTicket(input, rules.Count, fields);
            Console.WriteLine("multiply departure values together: {0}", multiplication);
        }

        private List<Limit> GetRules(string[] input)
        {
            List<Limit> limits = new List<Limit>();
            try 
            { 
                for(int i= 0; i < input.Length; i++)
                {
                    if (input[i].Length != 0)
                    {
                        string[] rule = input[i].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

                        string[] ruleLimits = rule[1].Split(new string[] { " or ", "-" }, StringSplitOptions.RemoveEmptyEntries);

                        int.TryParse(ruleLimits[0], out int currentLimitINF1);
                        int.TryParse(ruleLimits[1], out int currentLimitMAX1);
                        int.TryParse(ruleLimits[2], out int currentLimitINF2);
                        int.TryParse(ruleLimits[3], out int currentLimitMAX2);

                        limits.Add(new Limit() { Name = rule[0], LimitINF1 = currentLimitINF1, LimitMAX1 = currentLimitMAX1, LimitINF2 = currentLimitINF2, LimitMAX2 = currentLimitMAX2 });
                    }
                    else
                        break;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return limits;
        }

        private double CheckTicket(string[] input, int totalFields, List<KeyValuePair<string, int>> fields)
        {
            double multiplication = 1;
            try 
            {
                string[] currentTicket = input[totalFields + 2].Split(',');
                List<int> currentTicketInt = GeneralFunctions.ToIntList(currentTicket);

                foreach (KeyValuePair<string, int> field in fields)
                {
                    if (field.Key.IndexOf("departure") == 0)
                        multiplication *= currentTicketInt[field.Value];
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); multiplication = 0; }
            return multiplication;
        }

        private TicketsValidation CheckNearByTickets(string[] input, List<Limit> rules)
        {
            TicketsValidation validation = new TicketsValidation();
            try
            {
                for (int i = (rules.Count + 5); i < input.Length; i++)
                {
                    string[] currentTicket = input[i].Split(',');
                    List<int> fields = GeneralFunctions.ToIntList(currentTicket);
                    int currentError = isTicketInvalid(fields, rules);
                    if (currentError > 0)
                        validation.ErrorRate += currentError;
                    else
                        validation.ValidTickets.Add(GeneralFunctions.ToIntList(currentTicket));
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return validation;
        }

        private int isTicketInvalid(List<int> fields, List<Limit> rules)
        {
            int field = 0;
            try 
            {
                foreach (Limit limit in rules)
                {
                    fields.RemoveAll(f => (f >= limit.LimitINF1 && f <= limit.LimitMAX1) || (f >= limit.LimitINF2 && f <= limit.LimitMAX2));
                    if (fields.Count == 0)
                        break;
                }
                field = fields.Sum();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return field;
        }

        private List<KeyValuePair<string, int>> DetermineFields(List<List<int>> ValidTickets, List<Limit> rules)
        {
            List<KeyValuePair<string, int>> columnPositions = new List<KeyValuePair<string, int>>();
            try 
            {
                List<KeyValuePair<string, int>> validPositions = new List<KeyValuePair<string, int>>();
                for (int i = 0; i < rules.Count; i++)
                {
                    int position = -1;
                    for (int j = 0; j < rules.Count; j++)
                    {
                        position = j;
                        bool meetRule = true;
                        for (int k = 0; k < ValidTickets.Count; k++)
                        {
                            if ((rules[i].LimitINF1 > ValidTickets[k][j] || rules[i].LimitMAX1 < ValidTickets[k][j]) && (rules[i].LimitINF2 > ValidTickets[k][j] || rules[i].LimitMAX2 < ValidTickets[k][j]))
                            {
                                meetRule = false;
                                break;
                            }
                        }
                        if (meetRule)
                            validPositions.Add(new KeyValuePair<string, int>(rules[i].Name, position));
                    }
                }

                for (int i = 0; i < rules.Count; i++)
                {
                    IEnumerable<IGrouping<string, KeyValuePair<string, int>>> f = validPositions.GroupBy(v => v.Key).Where(g => g.Count() == 1); // group the rules and  take the one than only has one posibility. that has the field and its position
                    KeyValuePair<string, int> currentPosition =  f.First().First(); // Take the element from the grouping
                    validPositions = validPositions.Where(c => c.Value != currentPosition.Value).ToList(); // Remove the rules that has that column as a possibility
                    columnPositions.Add(currentPosition); // add the field and its position to a list
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return columnPositions;
        }
    }

    public class Limit
    {
        private string _Name = "";
        private int _limitINF1 = 0;
        private int _limitMAX1 = 0;
        private int _limitINF2 = 0;
        private int _limitMAX2 = 0;
        private int _fieldOrder = 0;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int LimitINF1
        {
            get { return _limitINF1; }
            set { _limitINF1 = value; }
        }

        public int LimitMAX1
        {
            get { return _limitMAX1; }
            set { _limitMAX1 = value; }
        }

        public int LimitINF2
        {
            get { return _limitINF2; }
            set { _limitINF2 = value; }
        }

        public int LimitMAX2
        {
            get { return _limitMAX2; }
            set { _limitMAX2 = value; }
        }

        public int FieldOrder
        {
            get { return _fieldOrder; }
            set { _fieldOrder = value; }
        }
    }

    public class TicketsValidation
    {
        private int _errorRate = 0;
        private List<List<int>> _validTickets = new List<List<int>>();

        public int ErrorRate
        {
            get { return _errorRate; }
            set { _errorRate = value; }
        }

        public List<List<int>> ValidTickets
        {
            get { return _validTickets; }
            set { _validTickets = value; }
        }
    }
}
