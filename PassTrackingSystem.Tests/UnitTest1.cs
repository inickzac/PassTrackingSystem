using PassTrackingSystem.Infrastructure;
using System;
using Xunit;

namespace PassTrackingSystem.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            RandomDataGenerator generator = new RandomDataGenerator(null);
            generator.CreateVisitors(1);
        }
    }
}
