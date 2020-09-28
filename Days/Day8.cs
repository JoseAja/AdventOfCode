using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days
{
    class Day8
    {
        private int Xpixel = 0;
        private int Ypixel = 0;

        public void Execute()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Day 8: Space Image Format    ---");
            Console.WriteLine("------------------------------------");
            AskForDimension();
            string image = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Files\\Day8-Input.txt");
            ProcessImage(image);
            string imageDecoded = DecodeImage(image);
            PrintImage(imageDecoded);
        }

        private void AskForDimension()
        {
            Console.WriteLine("Introduce the width in pixels:");
            string input = Console.ReadLine().Trim();
            if (int.TryParse(input, out Xpixel))
            {
                Console.WriteLine("Introduce the height in pixels:");
                input = Console.ReadLine().Trim();
                if (!int.TryParse(input, out Ypixel))
                   Console.WriteLine("Incorrect value");
            }
            else
                Console.WriteLine("Incorrect value");
        }

        private void ProcessImage(string image)
        {
            try
            {
                int layerSize = Xpixel* Ypixel;
                int minimumZeros = -1;
                int numberOfOnes = 0;
                int numberOfTwos = 0;
                
                int startIndex = 0;
                while (startIndex < image.Length)
                {
                    string layerContent = image.Substring(startIndex, layerSize);
                    int currentZeros = layerContent.Count(c => c=='0');
                    if ((minimumZeros > currentZeros) || (minimumZeros == -1))
                    {
                        minimumZeros = currentZeros;
                        numberOfOnes = layerContent.Count(c => c == '1');
                        numberOfTwos = layerContent.Count(c => c == '2');
                    }
                    startIndex += layerSize;
                }
                Console.WriteLine("Number of 1 digits multiplied by the number of 2 digits: {0}", numberOfOnes * numberOfTwos);

            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in ProcessImage(): {0}", ex.Message);
            }
        }

        // 0 = black; 1=white; 2= transparent
        private string DecodeImage(string image) 
        {
            string result = "";
            try
            {
                int layerSize = Xpixel * Ypixel;
                int layersNumber = image.Length / layerSize;
                for (int j = 0; j < layerSize; j++)
                {
                    char colorOfPixel = '2';
                    for (int i = 0; i < layersNumber; i++)
                    {
                        if (!image[j + (i* layerSize)].Equals('2'))
                        {
                            colorOfPixel = image[j + (i * layerSize)];
                            break;
                        }
                    }
                    result += colorOfPixel;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error in DecodeImage(): {0}", ex.Message);
            }
            return result;
        }

        private void PrintImage(string image)
        {
            for (int i = 0; i < Ypixel; i++)
            {               
                for (int j = 0; j < Xpixel; j++)
                {
                    Console.Write(image[j + (i* Xpixel)]=='0'? " ": "*");
                }
                Console.WriteLine();
            }
        }
    }
}
