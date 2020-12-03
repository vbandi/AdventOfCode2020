using System;
using System.IO;
using System.Linq;
using Utils;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = File.ReadAllLines("Input.Txt");
            int resultPart1 = 0;
            int resultPart2 = 0;

            using (new Stopwatch())
            {
                foreach (var line in items)
                {
                    var split = line.SplitAtChars("- :").ToList();
                    var num1 = int.Parse(split[0]);
                    var num2 = int.Parse(split[1]);
                    var c = split[2][0];
                    var pwd = split[3];

                    if (Part1Validate(num1, num2, c, pwd))
                        resultPart1++;

                    if (Part2Validate(num1, num2, c, pwd))
                        resultPart2++;
                }
            }

            Console.WriteLine($"Part 1: {resultPart1} password are valid");
            Console.WriteLine($"Part 2: {resultPart2} password are valid");
        }

        private static bool Part2Validate(int num1, int num2, char c, string pwd)
        {
            return pwd[num1] == c ^ pwd[num2] == c;
        }
        
        
        private static bool Part1Validate(int num1, int num2, char c, string pwd)
        {
            var cCount = pwd.Count(ch => ch == c);
            return cCount >= num1 && cCount <= num2;
        }
    }
}