namespace Nemonuri.Maths.Sequences;

#if NET7_0_OR_GREATER
public class RationalArithmeticSequencePremise<TNumber> : ISequencePremise<TNumber>
    where TNumber : IFloatingPoint<TNumber>
{
    public TNumber First {get;}
    public TNumber ClosedLast {get;}
    public TNumber Difference {get;}
    public TNumber LeftTolerance {get;}
    public Func<TNumber, int> IntegerizedRationalNumberToInt32Mapping {get;}
    public Func<int, TNumber> Int32ToPseudoIndexMapping {get;}

    public RationalArithmeticSequencePremise
    (
        TNumber first, 
        TNumber closedLast, 
        TNumber difference, 
        TNumber leftTolerance,
        Func<TNumber, int> integerizedRationalNumberToInt32Mapping,
        Func<int, TNumber> int32ToPseudoIndexMapping
    )
    {
        Guard.IsNotNull(integerizedRationalNumberToInt32Mapping);
        Guard.IsNotNull(int32ToPseudoIndexMapping);

        First = first;
        ClosedLast = closedLast;
        Difference = difference;
        LeftTolerance = leftTolerance;
        IntegerizedRationalNumberToInt32Mapping = integerizedRationalNumberToInt32Mapping;
        Int32ToPseudoIndexMapping = int32ToPseudoIndexMapping;

        var v1 = (closedLast - first)/difference;
        var v2 = TNumber.Floor(v1);
        Count = integerizedRationalNumberToInt32Mapping.Invoke(v2);
    }

    public int Count { get; }

    public bool TryGetItem(int index, [NotNullWhen(true)] out TNumber? outItem)
    {
        
        outItem =
            RationalArithmeticSequenceTheory.GetItem
            (
                Int32ToPseudoIndexMapping.Invoke(index),
                First,
                Difference
            );
        return true;
    }

    public bool TryGetSuccessor(TNumber? value, [NotNullWhen(true)] out TNumber? outSuccessor)
    {
        if (value is null)
        {
            outSuccessor = default;
            return false;
        }

        outSuccessor = value + Difference;
        return true;
    }
}
#endif