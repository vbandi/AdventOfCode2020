using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("Input.txt");
var earliest = int.Parse(input[0]);
var buses = input[1].Split(',').Where(s => s != "x").Select(int.Parse).ToList();

var results = buses.Select(b => GetFirst(earliest, b)).OrderBy(b => b.time);
var earliestBusAndTime = results.First();

Console.WriteLine($"Part 1: earliest bus is {earliestBusAndTime.busID}, at {earliestBusAndTime.time}");
Console.WriteLine($"Part 1: result is {earliestBusAndTime.busID * (earliestBusAndTime.time - earliest)}");

// Part 2

// var schedule = "1789,37,47,1889".Replace('x', '1').Split(',').Select(long.Parse).ToList();
var schedule = input[1].Replace('x', '1').Split(',').Select(long.Parse).ToList();

var values = schedule.Select((value, index) => (value, index)).ToList().OrderByDescending(x => x.value).ToList();
var largestValue = values.First();
var offsetValues = values.Select(x => (x.value, Index: x.index - largestValue.index)).ToList();

var step = largestValue.value;

var i = 100000000000000 - 100000000000000 % largestValue.value;

var soFarSoGood = true;

do
{
    i += step;

    soFarSoGood = true;
    for (int j = 1; j < offsetValues.Count; j++)
    {
        var x = offsetValues[j];

        if ((i + x.Index) % x.value != 0)
        {
            soFarSoGood = false;
            break;
        }
        else if (step % x.value != 0) 
            step *= x.value;
    }
} while (!soFarSoGood);

Console.WriteLine($"Part 2: {i - largestValue.index}");




(int busID, int time) GetFirst(int earliest, int busID)
{
    int x = earliest / busID;

    if (x * busID < earliest)
        x++;

    return (busID: busID, time: x * busID);
}