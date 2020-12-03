using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading input...");
            var numbers = File.ReadAllLines("Input.txt").Select(s => int.Parse(s)).ToList();
            Console.WriteLine($"Loaded {numbers.Count} rows");

            Part1(numbers);
            Part2(numbers);
        }

        private static void Part1(List<int> numbers)
        {
            using (new Utils.Stopwatch("Brute force"))
            {
                foreach (var num1 in numbers)
                {
                    if (numbers.Contains(2020 - num1))
                    {
                        Console.WriteLine($"Result: {num1 * (2020 - num1)}");
                        break;
                    }
                }
            }

            using (new Utils.Stopwatch("SearchForSum"))
            {
                Console.WriteLine($"Result: {SearchForSum(numbers, 2020)}");
            }

            using (new Utils.Stopwatch("Brute force LINQ"))
            {
                foreach (var num1 in numbers.Where(num1 => numbers.Contains(2020 - num1)))
                {
                    Console.WriteLine($"Result: {num1 * (2020 - num1)}");
                    break;
                }
            }

            using (new Utils.Stopwatch("Parallel"))
            {
                Parallel.ForEach(numbers, num1 =>
                {
                    if (numbers.Contains(2020 - num1))
                    {
                        Console.WriteLine($"Result: {num1 * (2020 - num1)}");
                    }
                });
            }
        }

        private static void Part2(List<int> numbers)
        {
            using (new Utils.Stopwatch("Part 2 - Brute force"))
            {
                foreach (var num1 in numbers)
                {
                    var partialResult = SearchForSum(numbers, 2020 - num1);

                    if (partialResult != -1)
                    {
                        Console.WriteLine($"Result: {partialResult * num1}");
                        break;
                    }
                }
            }
        }
        
        private static int SearchForSum(List<int> numbers, int requestedSum)
        {
            foreach (var num1 in numbers)
            {
                if (numbers.Contains(requestedSum - num1))
                {
                    return num1 * (requestedSum - num1);
                }
            }

            return -1;
        }
    }
}