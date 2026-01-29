using AdventOfCode.Tools.Extensions;
using FluentAssertions;

namespace AdventOfCodeTests.Tools;

[TestClass]
public sealed class ExtensionsTests
{
    private readonly List<int> _list = [42, 19];

    [TestMethod]
    public void HashSetAddAndReturnShouldWork()
    {
        var hashSet = new HashSet<int>();
        var result = hashSet.AddAndReturn(42);
        result.Should().HaveCount(1);
        result.First().Should().Be(42);
    }

    [TestMethod]
    public void HashSetAddRangeShouldWork()
    {
        var result = new HashSet<int>();
        result.AddRange(_list);
        result.Should().HaveCount(2);
        result.First().Should().Be(42);
        result.Last().Should().Be(19);
    }

    [TestMethod]
    public void HashSetAddRangeAndReturnShouldWork()
    {
        var hashSet = new HashSet<int>();
        var result = hashSet.AddRangeAndReturn(_list);
        result.Should().HaveCount(2);
        result.First().Should().Be(42);
        result.Last().Should().Be(19);
    }
}