using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day13
    {
        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 13: Care Package       ---");
            Console.WriteLine("----------------------------------");
            string[] input = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day13-Input.txt").Split(',');
            
        }

        private void Draw(string[] input)
        {
            ArrayList list = new ArrayList();

            int maximumX = 0;
            int maximumY = 0;


            //int count = 0;
            //while (count < input.Length)
            //{
            //    input[count];
            //    input[count+2];
            //    string character = GetCaracter(input[count+2]);

            //    list

            //    count += 3;

            //    File.
            //}
        }


        private string GetCaracter(string option)
        {
            string character = " ";
            switch(option)
            {
                case "0":
                    character = " ";
                    break;
                case "1":
                    character = ((char)176).ToString();
                    break;
                case "2":
                    character = ((char)219).ToString();
                    break;
                case "3":
                    character = ((char)205).ToString();
                    break;
                case "4":
                    character = "o";
                    break;
            }
            return character;
        }
    }
}
