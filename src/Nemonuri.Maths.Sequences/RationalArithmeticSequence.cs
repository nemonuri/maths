namespace Nemonuri.Maths.Sequences;

#if NET7_0_OR_GREATER
public class RationalArithmeticSequence<TNumber> : IReadOnlyList<TNumber>
    where TNumber : IFloatingPoint<TNumber>
{
    public BoundableRationalArithmeticSequencePremise<TNumber> Premise {get;}

    public int Count => _innerSequence.Count;

    public TNumber this[int index] => _innerSequence[index];

    private readonly Sequence<TNumber> _innerSequence;

    public RationalArithmeticSequence
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

    public RationalArithmeticSequence
    (
        BoundableRationalArithmeticSequencePremise<TNumber> premise,

#region Tolerant Interval
        TNumber leftBoundary, 
        BoundaryClosedDirection leftBoundaryClosedDirection, 
        TNumber rightBoundary, 
        BoundaryClosedDirection rightBoundaryClosedDirection,
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