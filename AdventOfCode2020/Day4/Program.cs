using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] eyeColors = "amb blu brn gry grn hzl oth".Split(' ');
            
            var input = String.Join("\r\n", File.ReadAllLines("Input.txt").Select(s => s.Trim()));
//             
//             
//
//             input = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
// byr:1937 iyr:2017 cid:147 hgt:183cm
//
// iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
// hcl:#cfa07d byr:1929
//
// hcl:#ae17e1 iyr:2013
// eyr:2024
// ecl:brn pid:760753108 byr:1931
// hgt:179cm
//
// hcl:#cfa07d eyr:2025 pid:166559648
// iyr:2011 ecl:brn hgt:59in";
            
            input = input.Replace("\r\n", " ").Replace("  ", "\r\n");

            // Console.WriteLine(input);
            
            var records = input.Split('\n').Select(s => s.Trim()).ToArray();
            Console.WriteLine($"Checking {records.Length} records...");

            var requiredFields = new Dictionary<string, Func<string, bool>>
            {
                {"byr", s => s.Length == 4 && int.Parse(s).IsInRange(1920, 2002)},
                {"iyr", s => s.Length == 4 && int.Parse(s).IsInRange(2010, 2020)},
                {"eyr", s => s.Length == 4 && int.Parse(s).IsInRange(2020, 2030)},
                {
                    "hgt", s => (s.EndsWith("cm") && int.Parse(s.Substring(0, 3)).IsInRange(150, 193)) || 
                                (s.EndsWith("in") && int.Parse(s.Substring(0, 2)).IsInRange(59, 76))
                },
                {"hcl", s => s[0] == '#' && s.Substring(1).All(c => ((int)c).IsInRange('0', '9') || ((int)c).IsInRange('a', 'f'))},
                {"ecl", s => eyeColors.Contains(s)},
                {"pid", s => s.Length == 9 && s.All(c => ((int)c).IsInRange('0', '9'))}
            };


            var part1Result = 0;
            var part2Result = 0;

            foreach (var record in records)
            {
                var parsed = Parse(record);

                if (requiredFields.Keys.All(field => parsed.Keys.Contains(field)))
                {
                    part1Result++;

                    try
                    {
                        if (requiredFields.All(rule => rule.Value(parsed[rule.Key])))
                            part2Result++;

                    }
                    catch
                    {}
                }
            }

            Console.WriteLine($"Part1 result: {part1Result}");
            Console.WriteLine($"Part2 result: {part2Result}");
        }
        
        private static Dictionary<string, string> Parse(string record)
        {
            var items = record.Split(' ');
            var result = items.Select(item => item.Split(':'))
                .ToDictionary(splitItem => splitItem[0], splitItem => splitItem[1]);

            // Console.WriteLine($"Parsed {record} to: {string.Join(", ", result.Keys)}");

            if (result.Count != record.Count(c => c == ':'))
                Console.WriteLine($"*********************************Parsed {record} to: {string.Join(", ", result.Keys)}");
                
            
            return result;
        }
    }
}