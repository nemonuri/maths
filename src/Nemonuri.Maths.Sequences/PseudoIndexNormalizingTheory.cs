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
        PseudoIndexNormalizingMethodKind normalizingMethodKind,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        switch (normalizingMethodKind)
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

    public static int GetIndex<TNumber>
    (
        TNumber sudoIndex,
        PseudoIndexNormalizingMethodKind mappingKind,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        Guard.IsNotNull(indexCaster);

        var v1 = Normalize(sudoIndex, mappingKind, out outResidual);
        return indexCaster.Invoke(v1);
    }

    public static int GetIndex<TNumber>
    (
        TNumber sudoIndex,
        PseudoIndexNormalizingMethodKind mappingKind,
        PseudoIndexNormalizer<TNumber>? mapper,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        if (mappingKind == PseudoIndexNormalizingMethodKind.Custom)
        {
            Guard.IsNotNull(mapper);
            return Normalize(sudoIndex, mapper, indexCaster, out outResidual);
        }
        else
        {
            return GetIndex(sudoIndex, mappingKind, indexCaster, out outResidual);
        }
    }

    public static int GetIndexInRationalNumberSystem<TNumber>
    (
        this PseudoIndexNormalizingMethodUnion<TNumber> self,
        TNumber value,
        TNumber first,
        TNumber difference,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : IFloatingPoint<TNumber>
    {
        Guard.IsNotNull(indexCaster);

        TNumber sudoIndex = RationalArithmeticSequenceTheory.GetPseudoIndex(value, first, difference);
        int unboundedIndex = 
            GetIndex
            (
                sudoIndex, 
                self.MappingKind, 
                self.CustomMapper, 
                indexCaster, 
                out outResidual
            );
        return unboundedIndex;
    }

#endif
}