using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Utils
{
    [TestFixture]
    public class SplitAtCharsTests
    {
        [Test]
        public void SimpleTests()
        {
            "".SplitAtChars("").ToList().Count.ShouldBe(0);

            var result = "aa".SplitAtChars("").ToList();
            result.ShouldBe(new [] {"aa"});
            
            result = "abab".SplitAtChars("a").ToList();
            result.ShouldBe(new [] {"bab"});

            
            result = "aabab".SplitAtChars("b").ToList();
            result.ShouldBe(new [] {"aa", "ab"});
        }
        
        [Test]
        public void VerifySplitAtChars()
        {
            
            var result = "1-3 a: abcde".SplitAtChars("- :").ToList();
            result.ShouldBe(new [] {"1", "3", "a", " abcde"});
        }
        
        
    }
}