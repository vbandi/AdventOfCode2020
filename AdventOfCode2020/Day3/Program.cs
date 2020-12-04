using System;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = File.ReadAllLines("Input.txt");
            Console.WriteLine($"Part 1 result: {CountTreesBySlope(map, (3, 1))}");
            
            // Day 2
            long result = 1;
            result *= CountTreesBySlope(map, (1, 1));
            result *= CountTreesBySlope(map, (3, 1));
            result *= CountTreesBySlope(map, (5, 1));
            result *= CountTreesBySlope(map, (7, 1));
            result *= CountTreesBySlope(map, (1, 2));
            
            Console.WriteLine($"Part 2 result: {result}");
        }

        private static int CountTreesBySlope(string[] map, (int right, int down) slope)
        {
            int xPos = 0;
            var treeCount = 0;

            var height = map.Length;
            var width = map[0].Length;

            for (int i = 0; i < height; i += slope.down)
            {
                if (map[i][xPos % width] == '#')
                    treeCount++;

                xPos += slope.right;
            }

            Console.WriteLine($"For slope {slope}, you meet {treeCount} trees.");
            return treeCount;
        }
    }
}