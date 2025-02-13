namespace Nemonuri.Maths.Sequences;

#if NET7_0_OR_GREATER
public class BoundedRationalArithmeticSequence<TNumber> : IReadOnlyList<TNumber>
    where TNumber : IFloatingPoint<TNumber>
{
    public TolerantBoundableRationalArithmeticSequencePremise<TNumber> Premise {get;}

    public int CountInMain => _innerSequence.Count;

    public TNumber this[int index] => _innerSequence[index];

    private readonly BoundedSequence<TNumber> _innerSequence;

    public BoundedRationalArithmeticSequence
    (
        TNumber first, 
        TNumber closedLast, 
        TNumber difference, 
        TNumber leftTolerance,
        Func<TNumber, int> integerizedRationalNumberToInt32Mapping,
        Func<int, TNumber> int32ToPseudoIndexMapping
    ) : this
    (
        new RationalArithmeticSequencePremise<TNumber>
        (
            first,
            closedLast,
            difference,
            leftTolerance,
            integerizedRationalNumberToInt32Mapping,
            int32ToPseudoIndexMapping
        )
    )
    {}

    public BoundedRationalArithmeticSequence
    (
        TolerantBoundableRationalArithmeticSequencePremise<TNumber> premise,

#region Tolerant Interval
        TNumber leftToleranceBoundary,
        BoundaryClosedDirection leftToleranceBoundaryClosedDirection,

        TNumber leftMainBoundary,
        BoundaryClosedDirection leftMainBoundaryClosedDirection,

        TNumber rightMainBoundary,
        BoundaryClosedDirection rightMainBoundaryClosedDirection,

        TNumber rightToleranceBoundary,
        BoundaryClosedDirection rightToleranceBoundaryClosedDirection,
#endregion Tolerant Interval
    )
    {
        Guard.IsNotNull(premise);

        Premise = premise;
        _innerSequence = new Sequence<TNumber>
            (
                premise,
                
                leftBoundary,
                leftBoundaryClosedDirection,
                rightBoundary,
                rightBoundaryClosedDirection,
                
                premise.ZeroIndex
            );
    }

    public IEnumerator<TNumber> GetEnumerator() => _innerSequence.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
#endif