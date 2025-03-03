using Nemonuri.Maths.Permutations;
using Xunit.Abstractions;

namespace Nemonuri.Maths.Permutations.Tests;

public class PermutationTheoryTest
{
    private readonly ITestOutputHelper _outputHelper;

    public PermutationTheoryTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Theory]
    [ClassData(typeof(Data1))]
    public void NormalizedPermutationGroup__GetInverseNormalizedPermutationGroup__InverseNormalizedPermutationGroup_Is_Expected
    (
        int[] normalizedPermutationGroup,
        int[] expectedInverseNormalizedPermutationGroup,
        bool expectedResult
    )
    {
        //Model
        Span<int> actualInverseNormalizedPermutationGroup = stackalloc int[normalizedPermutationGroup.Length];

        //Act
        PermutationTheory.GetInverseNormalizedPermutationGroup
        (
            normalizedPermutationGroup,
            actualInverseNormalizedPermutationGroup
        );

        bool actualResult = expectedInverseNormalizedPermutationGroup.AsSpan().SequenceEqual(actualInverseNormalizedPermutationGroup);

        //Assert
        _outputHelper.WriteLine
        (
$"""
normalizedPermutationGroup: {LogTheory.ConvertSpanToLogString<int>(normalizedPermutationGroup)}
expectedInverseNormalizedPermutationGroup: {LogTheory.ConvertSpanToLogString<int>(expectedInverseNormalizedPermutationGroup)}
actualInverseNormalizedPermutationGroup: {LogTheory.ConvertSpanToLogString<int>(actualInverseNormalizedPermutationGroup)}
expectedResult: {expectedResult}
actualResult: {actualResult}

"""
        );
        Assert.Equal(expectedResult, actualResult);
    }

    public class Data1 : TheoryData<int[], int[], bool>
    {
        public Data1()
        {
            Add([0,1,2], [0,1,2], true);
            Add([1,4,3,2,0], [4,0,3,2,1], true);
            Add([1,0,2], [1,0,2], true);
        }
    }


    [Theory]
    [ClassData(typeof(Data2))]
    public void Source_ProjectionIndexes__ApplyMultiProjection__Destination_Is_Expected
    (
        int[] source,
        int[] projectionIndexes,
        int[] expectedDestination,
        bool expectedResult
    )
    {
        //Model
        Span<int> actualDestination = stackalloc int[expectedDestination.Length];

        //Act
        PermutationTheory.ApplyMultiProjection
        (
            source,
            projectionIndexes,
            actualDestination
        );

        bool actualResult = expectedDestination.AsSpan().SequenceEqual(actualDestination);

        //Assert
        _outputHelper.WriteLine
        (
$"""
source: {LogTheory.ConvertSpanToLogString<int>(source)}
projectionIndexes: {LogTheory.ConvertSpanToLogString<int>(projectionIndexes)}
expectedDestination: {LogTheory.ConvertSpanToLogString<int>(expectedDestination)}
actualDestination: {LogTheory.ConvertSpanToLogString<int>(actualDestination)}
expectedResult: {expectedResult}
actualResult: {actualResult}

"""
        );
        Assert.Equal(expectedResult, actualResult);
    }

    public class Data2 : TheoryData<int[], int[], int[], bool>
    {
        public Data2()
        {
            Add([0,1,2], [0,1,2], [0,1,2], true);
            Add([3,5,7], [1,0,2], [5,3,7], true);
            Add([3,5,7], [0,1,1,0,2], [3,5,5,3,7], true);
        }
    }


    [Theory]
    [ClassData(typeof(Data3))]
    public void Source_NormalizedPermutationGroup__ApplyMultiProjection_And_Inverse__Source_And_FinalDestination_Are_Same
    (
        int[] source,
        int[] normalizedPermutationGroup
    )
    {
        //Model
        Span<int> firstDestination = stackalloc int[source.Length];
        Span<int> finalDestination = stackalloc int[source.Length];
        Span<int> inverseNormalizedPermutationGroup = stackalloc int[normalizedPermutationGroup.Length];
        PermutationTheory.GetInverseNormalizedPermutationGroup
        (
            normalizedPermutationGroup,
            inverseNormalizedPermutationGroup
        );

        //Act
        PermutationTheory.ApplyMultiProjection
        (
            source,
            normalizedPermutationGroup,
            firstDestination
        );
        PermutationTheory.ApplyMultiProjection
        (
            firstDestination,
            inverseNormalizedPermutationGroup,
            finalDestination
        );

        //Assert
        _outputHelper.WriteLine
        (
$"""
source: {LogTheory.ConvertSpanToLogString<int>(source)}
normalizedPermutationGroup: {LogTheory.ConvertSpanToLogString<int>(normalizedPermutationGroup)}
inverseNormalizedPermutationGroup: {LogTheory.ConvertSpanToLogString<int>(inverseNormalizedPermutationGroup)}
firstDestination: {LogTheory.ConvertSpanToLogString<int>(firstDestination)}
finalDestination: {LogTheory.ConvertSpanToLogString<int>(finalDestination)}

"""
        );
        Assert.Equal(source, finalDestination);
    }

    public class Data3 : TheoryData<int[], int[]>
    {
        public Data3()
        {
            Add([0,1,2], [0,1,2]);
            Add([0,1,2], [2,1,0]);
            Add([3,5,4], [1,0,2]);
            Add([3,5,4,9,10], [1,0,2,4,3]);
            Add([3,5,4,9,10], [1,4,2,0,3]);
        }
    }
}
