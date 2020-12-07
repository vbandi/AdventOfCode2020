using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("Input.txt");
            input = input.Replace("\r\n", " ").Replace("  ", "\n");
            
            var records = input.Split('\n').Select(s => s.Trim().Replace(" ", "")).ToArray();

            var result = records.Sum(s => s.Distinct().Count());
            
            Console.WriteLine($"Part 1: {result}");

            records = input.Split('\n').Select(s => s.Trim()).ToArray();
            result = records.Sum(s => s.Distinct().Count(c => s.Split(' ').All(a => a.Contains(c))));
            Console.WriteLine($"Part 2: {result}");

        }
    }
}