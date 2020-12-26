using System;
using System.IO;
using System.Linq;

var lines = File.ReadAllLines("input.txt");
var adapters = lines.Select(int.Parse).ToList();
adapters.Sort();
adapters.Insert(0, 0);

var diff1 = 0;
var diff3 = 0;

for (int i = 1; i < adapters.Count; i++)
{
    var diff = adapters[i] - adapters[i - 1];

    if (diff == 1)
        diff1++;

    if (diff == 3)
        diff3++;
}

diff3++;

Console.WriteLine($"Part 1 result. diff1: {diff1}, diff3: {diff3}. Multiplied: {diff1 * diff3}");

// TODO: PART 2
var possibleNextSteps = adapters.Select(x => (x, count: adapters.Count(y => y > x && y <= x+3))).ToDictionary(a => a.x, a => a.count);
