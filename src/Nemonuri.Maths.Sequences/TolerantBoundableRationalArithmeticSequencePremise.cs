namespace Nemonuri.Maths.Sequences;

public interface IInvertibleMappingPremise<TDomain, TCodomain>
{
    Func<TDomain, int> IntegerizedRationalNumberToInt32Mapping {get;}
    Func<int, TDomain> Int32ToPseudoIndexMapping {get;}


}

#if NET7_0_OR_GREATER
public class TolerantBoundableRationalArithmeticSequencePremise<TNumber> : 
    ISequencePremise<TNumber>,
    ISequenceBoundingPremise<TNumber>,
    ISequenceItemToIndexPremise<TNumber>

    where TNumber : IFloatingPoint<TNumber>
{
    public TNumber ZeroIndex {get;}
    public TNumber Difference {get;}
    public Func<TNumber, int> IntegerizedRationalNumberToInt32Mapping {get;}
    public Func<int, TNumber> Int32ToPseudoIndexMapping {get;}
    public ITolerantInterval<TNumber> TolerantInterval {get;}
    public IExtraArgumentAttachedMapping<TNumber, object?, TNumber> NormalizedIndexMapping {get;}
    public IAlternativeIndexFactory<TNumber, object?, TNumber> LeftToleranceAlternativeIndexFactory {get;}
    public IAlternativeIndexFactory<TNumber, object?, TNumber> RightToleranceAlternativeIndexFactory {get;}

    public TolerantBoundableRationalArithmeticSequencePremise
    (
        TNumber zeroIndex,  
        TNumber difference,

        ITolerantInterval<TNumber> tolerantInterval,
        IExtraArgumentAttachedMapping<TNumber, object?, TNumber> normalizedIndexMapping,
        IAlternativeIndexFactory<TNumber, object?, TNumber> leftToleranceAlternativeIndexFactory,
        IAlternativeIndexFactory<TNumber, object?, TNumber> rightToleranceAlternativeIndexFactory,

        Func<TNumber, int> integerizedRationalNumberToInt32Mapping,
        Func<int, TNumber> int32ToPseudoIndexMapping
    )
    {
        Guard.IsNotNull(tolerantInterval);
        Guard.IsNotNull(normalizedIndexMapping);
        Guard.IsNotNull(leftToleranceAlternativeIndexFactory);
        Guard.IsNotNull(rightToleranceAlternativeIndexFactory);
        Guard.IsNotNull(integerizedRationalNumberToInt32Mapping);
        Guard.IsNotNull(int32ToPseudoIndexMapping);

        ZeroIndex = zeroIndex;
        Difference = difference;
        IntegerizedRationalNumberToInt32Mapping = integerizedRationalNumberToInt32Mapping;
        Int32ToPseudoIndexMapping = int32ToPseudoIndexMapping;
        TolerantInterval = tolerantInterval;
        NormalizedIndexMapping = normalizedIndexMapping;
        LeftToleranceAlternativeIndexFactory = leftToleranceAlternativeIndexFactory;
        RightToleranceAlternativeIndexFactory = rightToleranceAlternativeIndexFactory;
    }

    public bool TryGetItem(int index, [NotNullWhen(true)] out TNumber? outItem)
    {
        outItem =
            RationalArithmeticSequenceTheory.GetItem
            (
                Int32ToPseudoIndexMapping.Invoke(index),
                ZeroIndex,
                Difference
            );

        return true;
    }

    public int GetLeastIndex(TNumber leftBoundary, BoundaryClosedDirection leftBoundaryClosedDirection)
    {
        int result;

        var v1 = RationalArithmeticSequenceTheory.GetPseudoIndex(leftBoundary, ZeroIndex, Difference);
        var v2 = TNumber.Ceiling(v1);
        result = IntegerizedRationalNumberToInt32Mapping.Invoke(v2);
        if 
        (
            leftBoundaryClosedDirection == BoundaryClosedDirection.Left &&
            v1 == v2
        )
        {
            result += 1;
        }
        
        return result;
    }

    public int GetGreatestIndex(TNumber rightBoundary, BoundaryClosedDirection rightBoundaryClosedDirection)
    {
        int result;
        
        var v1 = RationalArithmeticSequenceTheory.GetPseudoIndex(rightBoundary, ZeroIndex, Difference);
        var v2 = TNumber.Floor(v1);
        result = IntegerizedRationalNumberToInt32Mapping.Invoke(v2);
        if 
        (
            rightBoundaryClosedDirection == BoundaryClosedDirection.Right &&
            v1 == v2
        )
        {
            result -= 1;
        }
        
        return result;
    }

    public bool TryGetIndex(TNumber item, out int outIndex)
    {
        if
        (
            TolerantIntervalTheory.TryGetNormalizedIndex
            (
                item,

                TolerantInterval,
                NormalizedIndexMapping,
                LeftToleranceAlternativeIndexFactory,
                RightToleranceAlternativeIndexFactory,

                out TNumber? outNormalizedIndex
            )
        )
        {
            outIndex = IntegerizedRationalNumberToInt32Mapping.Invoke(outNormalizedIndex);
            return true;
        }
        else
        {
            outIndex = default;
            return false;
        }
    }
}
#endif