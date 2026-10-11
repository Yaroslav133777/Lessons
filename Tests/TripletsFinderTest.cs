using LessonsProject;
using Xunit;

namespace Tests;

public class TripletsFinderTest
{
    [Fact]
    public void FindTriplets_ExactlyThreeTriplets_ReturnsAll()
    {
        List<int> numbers = [1, 2, 3, 1, 2, 3, 1, 2, 3];

        List<List<int>> result = TripletsFinder.FindTriplets(numbers, 6);

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void FindTriplets_MatchingTriplets_ReturnsThem()
    {
        List<int> numbers = [1, 2, 3, 2, 2, 2];

        List<List<int>> result = TripletsFinder.FindTriplets(numbers, 6);

        Assert.Equal(2, result.Count);
        Assert.Equal([1, 2, 3], result[0]);
        Assert.Equal([2, 2, 2], result[1]);
    }

    [Fact]
    public void FindTriplets_LessThanThreeNumbers_ReturnsEmpty()
    {
        List<int> numbers = [1, 2];

        List<List<int>> result = TripletsFinder.FindTriplets(numbers, 3);

        Assert.Empty(result);
    }

    [Fact]
    public void FindTriplets_NoMatch_ReturnsEmpty()
    {
        List<int> numbers = [1, 2, 3];

        List<List<int>> result = TripletsFinder.FindTriplets(numbers, 100);

        Assert.Empty(result);
    }
}