using LessonsProject;
using Xunit;

namespace UnitTests;

public class TripletsFinderTest
{
    [Fact]
    public void FindTriplets_EmptyList_ReturnsEmpty()
    {
        List<List<int>> result = Program.FindTriplets([], 0);

        Assert.Empty(result);
    }

    [Fact]
    public void FindTriplets_LessThanThreeNumbers_ReturnsEmpty()
    {
        List<List<int>> result = Program.FindTriplets([1, 2], 3);

        Assert.Empty(result);
    }

    [Fact]
    public void FindTriplets_NoMatch_ReturnsEmpty()
    {
        List<List<int>> result = Program.FindTriplets([1, 2, 3], 100);

        Assert.Empty(result);
    }

    [Fact]
    public void FindTriplets_SingleMatchingTriplet_ReturnsIt()
    {
        List<List<int>> result = Program.FindTriplets([1, 2, 3], 6);

        Assert.Equal(new List<int> { 1, 2, 3 }, Assert.Single(result));
    }

    [Fact]
    public void FindTriplets_MultipleMatchingTriplets_ReturnsThem()
    {
        List<List<int>> result = Program.FindTriplets([1, 2, 3, 2, 2, 2], 6);

        Assert.Equal(2, result.Count);
        Assert.Equal(new List<int> { 1, 2, 3 }, result[0]);
        Assert.Equal(new List<int> { 2, 2, 2 }, result[1]);
    }

    [Fact]
    public void FindTriplets_MoreThanThreeMatches_ReturnsAtMostThree()
    {
        List<List<int>> result = Program.FindTriplets([1, 2, 3, 1, 2, 3, 1, 2, 3], 6);

        Assert.Equal(3, result.Count);
    }
}
