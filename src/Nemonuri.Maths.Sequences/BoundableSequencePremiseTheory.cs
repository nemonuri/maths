namespace Nemonuri.Maths.Sequences;

public static class BoundableSequencePremiseTheory
{
    public static int GetCount<T>
    (
        this ISequenceBoundingPremise<T> premise,
        T leftBoundary, 
        BoundaryClosedDirection leftBoundaryClosedDirection, 
        T rightBoundary, 
        BoundaryClosedDirection rightBoundaryClosedDirection,
        out int outLeastIndex,
        out int outGreatestIndex
    )
#if NET9_0_OR_GREATER
    where T : allows ref struct
#endif
    {
        Guard.IsNotNull(premise);

        outLeastIndex = premise.GetLeastIndex(leftBoundary, leftBoundaryClosedDirection);
        outGreatestIndex = premise.GetLeastIndex(rightBoundary, rightBoundaryClosedDirection);
        return GetCount(outLeastIndex, outGreatestIndex);
    }

    public static int GetCount(int leastIndex, int greatestIndex)
    {
        Guard.IsGreaterThanOrEqualTo(greatestIndex, leastIndex);
        return greatestIndex - leastIndex + 1;
    }
}
