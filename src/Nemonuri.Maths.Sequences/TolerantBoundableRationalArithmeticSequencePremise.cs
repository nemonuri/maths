namespace Nemonuri.Maths.Sequences;

#if NET7_0_OR_GREATER
public class TolerantBoundableRationalArithmeticSequencePremise<TNumber> : 
    ISequencePremise<TNumber>,
    ISequenceBoundingPremise<TNumber>,
    ISequenceItemToIndexPremise<TNumber>,
    IInvertibleMappingPremise<TNumber, int>,
    IPseudoIndexNormalizingPremise<TNumber>,
    IToleranceAlternativeIndexFactoryPairProvider<TNumber, object, object>

    where TNumber : IFloatingPoint<TNumber>
{
    public TNumber ZeroIndex {get;}
    public TNumber Difference {get;}
    public ITolerantInterval<TNumber> TolerantInterval {get;}
    private readonly IPseudoIndexNormalizingPremise<TNumber> _pseudoIndexNormalizingPremise;
    private readonly IInvertibleMappingPremise<TNumber, int> _innerIndexMappingPremise;
    private readonly IToleranceAlternativeIndexFactoryPairProvider<TNumber, object, object> _innerToleranceAlternativeIndexFactoryPairProvider;

    public TolerantBoundableRationalArithmeticSequencePremise
    (
        TNumber zeroIndex,  
        TNumber difference,

        ITolerantInterval<TNumber> tolerantInterval,
        IPseudoIndexNormalizingPremise<TNumber> pseudoIndexNormalizingPremise,
        IInvertibleMappingPremise<TNumber, int> innerIndexMappingPremise,
        IToleranceAlternativeIndexFactoryPairProvider<TNumber, object, object> innerToleranceAlternativeIndexFactoryPairProvider
    )
    {
        Guard.IsNotNull(tolerantInterval);
        Guard.IsNotNull(pseudoIndexNormalizingPremise);
        Guard.IsNotNull(innerToleranceAlternativeIndexFactoryPairProvider);
        Guard.IsNotNull(innerIndexMappingPremise);

        ZeroIndex = zeroIndex;
        Difference = difference;
        TolerantInterval = tolerantInterval;
        _pseudoIndexNormalizingPremise = pseudoIndexNormalizingPremise;
        _innerIndexMappingPremise = innerIndexMappingPremise;
        _innerToleranceAlternativeIndexFactoryPairProvider = innerToleranceAlternativeIndexFactoryPairProvider; 
    }

    public IAlternativeIndexFactory<TNumber, object?, TNumber> LeftToleranceAlternativeIndexFactory => _innerToleranceAlternativeIndexFactoryPairProvider.LeftToleranceAlternativeIndexFactory;
    public IAlternativeIndexFactory<TNumber, object?, TNumber> RightToleranceAlternativeIndexFactory => _innerToleranceAlternativeIndexFactoryPairProvider.RightToleranceAlternativeIndexFactory;

    public bool TryGetItem(int index, [NotNullWhen(true)] out TNumber? outItem)
    {
        outItem =
            RationalArithmeticSequenceTheory.GetItem
            (
                this.InverseMap(index),
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
        result = this.Map(v2);
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
        result = this.Map(v2);
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
                NormalizePseudoIndex,
                LeftToleranceAlternativeIndexFactory,
                RightToleranceAlternativeIndexFactory,

                out TNumber? outNormalizedIndex
            )
        )
        {
            outIndex = this.Map(outNormalizedIndex);
            return true;
        }
        else
        {
            outIndex = default;
            return false;
        }
    }

    public bool TryInverseMap(int item, [NotNullWhen(true)] out TNumber? outResult)
    {
        return _innerIndexMappingPremise.TryInverseMap(item, out outResult);
    }

    public bool TryMap(TNumber item, [NotNullWhen(true)] out int outResult)
    {
        return _innerIndexMappingPremise.TryMap(item, out outResult);
    }

    public TNumber NormalizePseudoIndex(TNumber pseudoIndex)
    {
        return _pseudoIndexNormalizingPremise.NormalizePseudoIndex(pseudoIndex);
    }
}
#endif