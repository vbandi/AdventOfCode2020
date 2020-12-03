using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class Stopwatch : IDisposable
    {
        private readonly string _name;
        private System.Diagnostics.Stopwatch _stopwatch;

        public Stopwatch(string name = "")
        {
            _name = name;
            Console.WriteLine($"Starting Stopwatch {name}");
            _stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void Dispose()
        {
            Console.WriteLine($"Stopwatch {_name} took {_stopwatch.ElapsedMilliseconds} ms. ({_stopwatch.ElapsedTicks} Ticks)");
            _stopwatch.Stop();
        }
    }

    public static class Utilities
    {
        public static IEnumerable<string> SplitAtChars(this string input, IEnumerable<char> chars)
        {
            if (input.Length == 0)
                yield break;
            
            StringBuilder result = new StringBuilder();
            var index = 0;

            foreach (var c in chars)
            {
                char nextChar;

                while ((nextChar = input[index++]) != c)
                {
                    result.Append(nextChar);

                    if (index < input.Length)
                        continue;

                    if (result.Length > 0)
                        yield return result.ToString();
                    else
                        yield break;
                }

                if (result.Length > 0)
                    yield return result.ToString();

                result.Clear();
                continue;

            }

            if (index < input.Length)
            {
                yield return input.Substring(index);
            }
        }
    }
}