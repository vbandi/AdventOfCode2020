using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

var input = File.ReadAllLines("Input.txt").Select(s => long.Parse(s)).ToArray();

for (var i = 25; i < input.Length; i++)
{
    if (!IsValid(input[i], input.Skip(i - 25).Take(25)))
    {
        Console.WriteLine($"Bad number: {input[i]}");

        using (new Stopwatch("Brute force with span"))
        {
            FindContinuousSetThatSumsUp(input[i]);
        }

        using (new Stopwatch("Slightly less brute force"))
        {
            FindContinuousSetThatSumsUp2(input[i]);
        }
 
        using (new Stopwatch("Optimized"))
        {
            FindContinuousSetThatSumsUp3(input[i]);
        }
    }
}

bool IsValid(long number, IEnumerable<long> preamble)
{
    return preamble.Any(num1 => preamble.Contains(number - num1));
}


void FindContinuousSetThatSumsUp(long num)
{
    long sum;
    for (int i = 0; i < input.Length-1; i++)
    {
        int len = 2;

        do
        {
            var slice = new Span<long>(input, i, len).ToArray();
            sum = slice.Sum();
        
            if (sum == num)
            {
                Console.WriteLine($"Part 2 result: {slice.Min() + slice.Max()}");
                return;
            }
        
            len++;
        } while (sum < num);
    }
}

void FindContinuousSetThatSumsUp2(long num)
{
    for (int i = 0; i < input.Length-1; i++)
    {
        int len = 2;
        long sum = input[i] + input[i + 1];
        
        while (sum <= num)
        {
            if (sum == num)
            {
                var slice = input.Skip(i).Take(len-1).ToArray();
                Console.WriteLine($"Part 2 result: {slice.Min() + slice.Max()}");
                return;
            }
        
            sum += input[i + len++];
        } 
    }
}

void FindContinuousSetThatSumsUp3(long num)
{
    int i = 0;
    long sum = input[0] + input[1];
    int len = 2;

    while (i < input.Length)
    {
        while (sum <= num)
        {
            if (sum == num)
            {
                var slice = input.Skip(i).Take(len-1).ToArray();
                Console.WriteLine($"Part 2 result: {slice.Min() + slice.Max()}");
                return;
            }
        
            sum += input[i + len++];
        }

        while (sum > num)
        {
            sum -= input[i++];
            len--;
        }
    }
}
