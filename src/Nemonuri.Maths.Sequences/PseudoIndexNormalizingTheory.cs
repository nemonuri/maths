namespace Nemonuri.Maths.Sequences;

public static class PseudoIndexNormalizingTheory
{
    public static int Normalize<TNumber>
    (
        TNumber pseudoIndex,
        PseudoIndexNormalizer<TNumber> normalizer,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
    {
        Guard.IsNotNull(normalizer);
        Guard.IsNotNull(indexCaster);

        TNumber v1 = normalizer.Invoke(pseudoIndex, out outResidual);
        return indexCaster.Invoke(v1);
    }

#if NET7_0_OR_GREATER
    public static TNumber Normalize<TNumber>
    (
        TNumber pseudoIndex,
        PseudoIndexNormalizingMethodKind methodKind,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        switch (methodKind)
        {
            case PseudoIndexNormalizingMethodKind.Floor:
                {
                    var unboundedIndex = TNumber.Floor(pseudoIndex);
                    outResidual = pseudoIndex - unboundedIndex;
                    return unboundedIndex;
                }

            case PseudoIndexNormalizingMethodKind.Ceiling:
                {
                    var unboundedIndex = TNumber.Ceiling(pseudoIndex);
                    outResidual = unboundedIndex - pseudoIndex;
                    return unboundedIndex;
                }
            default:
                outResidual = default!;
                return ThrowHelper.ThrowInvalidOperationException<TNumber>();
        }
    }

    public static TNumber Normalize<TNumber>
    (
        TNumber pseudoIndex,
        PseudoIndexNormalizingMethodKind methodKind,
        PseudoIndexNormalizer<TNumber>? mapper,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        if (methodKind == PseudoIndexNormalizingMethodKind.Custom)
        {
            Guard.IsNotNull(mapper);
            return mapper.Invoke(pseudoIndex, out outResidual);
        }
        else
        {
            return Normalize(pseudoIndex, methodKind, out outResidual);
        }
    }

    public static TNumber GetNormalizedIndexInRationalNumberSystem<TNumber>
    (
        this PseudoIndexNormalizingMethodUnion<TNumber> self,
        TNumber value,
        TNumber first,
        TNumber difference,
        out TNumber outResidual
    )
        where TNumber : IFloatingPoint<TNumber>
    {
        TNumber pseudoIndex = RationalArithmeticSequenceTheory.GetPseudoIndex(value, first, difference);
        TNumber normalizedIndex = 
            Normalize
            (
                pseudoIndex, 
                self.MappingKind, 
                self.CustomMapper,
                out outResidual
            );
        return normalizedIndex;
    }

#endif
}