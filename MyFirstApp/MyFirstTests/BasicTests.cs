using MyFirstLib;
using System;
using Xunit;

namespace MyFirstTests
{
    public class BasicTests
    {
        [Fact]
        public void TrueIsTrue()
        {
            Assert.True(true);
        }

        [Fact]
        public void GetWordsWorks()
        {
            Assert.Equal(2, Helpers.GetWords("Hello world!").Count);
        }

        [Fact]
        public void GetWordsThrowsForNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>  Helpers.GetWords(null));
        }
    }
}
