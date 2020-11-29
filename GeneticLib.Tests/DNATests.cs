using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GeneticLib;
using Xunit;

namespace GeneticLib.Tests
{
    public class DNATests
    {
        [Theory]
        [InlineData("Hey", "Hello", 0.4)]
        [InlineData("BLA", "BLA", 1)]
        public void Should_CalcFitness(string str1, string str2, double expected)
        {
            DNA element = new DNA(str2.Length);
            element.Word = new StringBuilder(str1);
            double actual = element.CalcFitness(str2);

            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData("popaaaa", "aaacorn", "popcorn")]
        [InlineData("123456", "789100", "123100")]
        public void Should_CrossOver(string str1, string str2, string expected)
        {
            DNA parent1 = new DNA(str1.Length);
            parent1.Word = new StringBuilder(str1);
            DNA parent2 = new DNA(str2.Length);
            parent2.Word = new StringBuilder(str2);

            DNA child = parent1.CrossOver(parent2);
            Assert.Equal(child.Word.ToString(), expected);
        }
    }
}
