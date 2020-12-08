using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Utils;
using System.Linq;

namespace Day7
{
    class Program
    {
        private static Dictionary<string, List<(int count, string color)>> _rules;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("Input.txt");
            var colors = new List<string>();

//             var example = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
// dark orange bags contain 3 bright white bags, 4 muted yellow bags.
// bright white bags contain 1 shiny gold bag.
// muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
// shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
// dark olive bags contain 3 faded blue bags, 4 dotted black bags.
// vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
// faded blue bags contain no other bags.
// dotted black bags contain no other bags.".Split('\n');
//
//             ParseRules(example).Dump();

            _rules = ParseRules(input);
            
            Console.WriteLine($"{DirectHolders("shiny gold").Count()} colors can hold shiny gold directly");
            Console.WriteLine($"Part 1 result: {AllHolders("shiny gold").Count()}");
            Console.WriteLine($"Part 2 result: {CountContainedBags("shiny gold")}");
        }

        private static int CountContainedBags(string color)
        {
            return _rules[color].Sum(rule => rule.count * (1 + CountContainedBags(rule.color)));
        }
        
        private static IEnumerable<string> DirectHolders(string color)
        {
            var holders = _rules.Where(rule => rule.Value.Any(v => v.color == color)).Select(x => x.Key);
            return holders;
        }

        private static IEnumerable<string> AllHolders(string color)
        {
            var result = new List<string>(DirectHolders(color));
            var hasNew = false;

            do
            {
                var newItems = new List<string>();
                var count = result.Count;
                
                foreach (var knownHolder in result)
                    newItems.AddRange(DirectHolders(knownHolder));

                result.AddRange(newItems.Except(result));
                hasNew = count < result.Count;
            } while (hasNew);

            return result;
        }

        static Dictionary<string, List<(int count, string color)>> ParseRules(string[] lines)
        {
            var result = new Dictionary<string, List<(int count, string color)>>();  // key: container color

            foreach (var line in lines)
            {
                // var match = Regex.Match(line, "(((.+)) {1,2})(bags contain) (((\\d+) (((.+) )*)\\w*(, )?)*)");
                var match = Regex.Match(line, "(\\w* \\w*) bags contain (.*)");
                
                var color = match.Groups[1].ToString().Trim();
                
                if (result.ContainsKey(color))
                    throw new Exception("OOOOPPPS");
                
                result[color] = new List<(int count, string color)>();

                var contentsStr = match.Groups[2].ToString();
                var contents = Regex.Matches(contentsStr, "(\\d+) ((\\w*) (\\w*))");

                foreach (Match content in contents)
                    result[color].Add((int.Parse(content.Groups[1].Value), content.Groups[2].Value));
            }

            return result;
        }
    }
}