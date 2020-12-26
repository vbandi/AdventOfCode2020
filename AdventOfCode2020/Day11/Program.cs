using System;
using System.IO;
using System.Linq;
using System.Net;

var input = File.ReadAllText("Input.txt");
var height = input.Count(c => c == '\n') + 1;
var width = input.IndexOf('\n') - 1;
var current = input.Replace("\r\n", "");
bool dump = true;

Console.WriteLine($"Layout is {width} * {height}");

var steps = 0;

while (true)
{
    steps++;
    var next = CalculateStep(4, CalculateOccupiedNeighbors);

    if (next == current)
        break;

    current = next;
    Console.WriteLine($"---{steps}---");

    if (dump)
        for (int i = 0; i < height; i++)
            Console.WriteLine(current.Substring(i * width, width));
}

Console.WriteLine($"Number of occupied seats after {steps} steps: {current.Count(c => c == '#')}");

// PART 2
current = input.Replace("\r\n", "");
steps = 0;


while (true)
{
    steps++;
    var next = CalculateStep(5, CalculateOccupiedNeighbors2);

    if (next == current)
        break;

    current = next;
    Console.WriteLine($"---{steps}---");

    if (dump)
        for (int i = 0; i < height; i++)
            Console.WriteLine(current.Substring(i * width, width));
}

Console.WriteLine($"Part 2 - Number of occupied seats after {steps} steps: {current.Count(c => c == '#')}");


string CalculateStep(int threshold, Func<int, int, int> calculateOccupiedNeighborsFunc)
{
    var result = new char[width * height];

    for (int x = 0; x < width; x++)
    for (int y = 0; y < height; y++)
    {
        int occupiedNeighbors = calculateOccupiedNeighborsFunc(x, y);
        var stateAtNextStep = GetAt(x, y);

        switch (stateAtNextStep)
        {
            case 'L' when occupiedNeighbors == 0:
                stateAtNextStep = '#';
                break;
            case '#' when occupiedNeighbors >= threshold:
                stateAtNextStep = 'L';
                break;
        }

        result[y * width + x] = stateAtNextStep;

    }
    return new string(result);
}

int CalculateOccupiedNeighbors(int x, int y)
{
    int result = 0;

    if (GetAt(x - 1, y - 1) == '#')
        result++;
    if (GetAt(x, y - 1) == '#')
        result++;
    if (GetAt(x + 1, y - 1) == '#')
        result++;
    if (GetAt(x - 1, y) == '#')
        result++;
    if (GetAt(x + 1, y) == '#')
        result++;
    if (GetAt(x - 1, y + 1) == '#')
        result++;
    if (GetAt(x, y + 1) == '#')
        result++;
    if (GetAt(x + 1, y + 1) == '#')
        result++;

    return result;
}

int CalculateOccupiedNeighbors2(int x, int y)
{
    int result = 0;

    if (AnyNeighborsInDirection(x, y, -1, -1))
        result++;
    if (AnyNeighborsInDirection(x, y, 0, -1))
        result++;
    if (AnyNeighborsInDirection(x, y, 1, -1))
        result++;
    if (AnyNeighborsInDirection(x, y, -1, 0))
        result++;
    if (AnyNeighborsInDirection(x, y, 1, 0))
        result++;
    if (AnyNeighborsInDirection(x, y, -1, 1))
        result++;
    if (AnyNeighborsInDirection(x, y, 0, 1))
        result++;
    if (AnyNeighborsInDirection(x, y, 1, 1))
        result++;

    return result;
}

bool AnyNeighborsInDirection(int x, int y, int deltaX, int deltaY)
{
    while (x >= 0 && y >= 0 && x < width && y < height)
    {
        x += deltaX;
        y += deltaY;

        if (GetAt(x, y) == '#')
            return true;

        if (GetAt(x, y) == 'L')
            return false;
    }

    return false;
}

char GetAt(int x, int y)
{
    if (x < 0 || x >= width)
        return '.';

    if (y < 0 || y >= height)
        return '.';
    
    return current[y * width + x];
}



