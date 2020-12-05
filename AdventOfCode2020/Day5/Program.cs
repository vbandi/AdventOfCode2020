using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var seatIDs = input.Select(GetSeatID).ToList();
            var max = seatIDs.Max();
            Console.WriteLine($"Part 1: {max}");

            var emptySeats = Enumerable.Range(0, max).Except(seatIDs);
            var mySeat = emptySeats.Where(id => seatIDs.Contains(id + 1) && seatIDs.Contains(id - 1));
            
            Console.WriteLine($"Part2: {mySeat.First()}");
        }

        private static int GetSeatID(string code)
        {
            string binary = code.Replace('F', '0').Replace('B', '1').Replace('R', '1').Replace('L', '0');
            return Convert.ToInt32(binary, 2);
        }
    }
}