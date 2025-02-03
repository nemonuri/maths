using System.Security.Cryptography.X509Certificates;

namespace Nemonuri.Maths.Sequences;

public static class SudoIndexToIntegerTheory
{
    public static int GetUnboundedIndex<TNumber>
    (
        TNumber sudoIndex,
        SudoIndexToIntegerMapper<TNumber> mapper,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
    {
        Guard.IsNotNull(mapper);
        Guard.IsNotNull(indexCaster);

        TNumber v1 = mapper.Invoke(sudoIndex, out outResidual);
        return indexCaster.Invoke(v1);
    }

#if NET7_0_OR_GREATER
    public static TNumber GetUnboundedIndex<TNumber>
    (
        TNumber sudoIndex,
        SudoIndexToIntegerMappingKind mappingKind,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        switch (mappingKind)
        {
            case SudoIndexToIntegerMappingKind.Floor:
                {
                    var unboundedIndex = TNumber.Floor(sudoIndex);
                    outResidual = sudoIndex - unboundedIndex;
                    return unboundedIndex;
                }

            case SudoIndexToIntegerMappingKind.Ceiling:
                {
                    var unboundedIndex = TNumber.Ceiling(sudoIndex);
                    outResidual = unboundedIndex - sudoIndex;
                    return unboundedIndex;
                }
            default:
                outResidual = default!;
                return ThrowHelper.ThrowInvalidOperationException<TNumber>();
        }
    }

    public static int GetUnboundedIndex<TNumber>
    (
        TNumber sudoIndex,
        SudoIndexToIntegerMappingKind mappingKind,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        Guard.IsNotNull(indexCaster);

        var v1 = GetUnboundedIndex(sudoIndex, mappingKind, out outResidual);
        return indexCaster.Invoke(v1);
    }

    public static int GetUnboundedIndex<TNumber>
    (
        TNumber sudoIndex,
        SudoIndexToIntegerMappingKind mappingKind,
        SudoIndexToIntegerMapper<TNumber>? mapper,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        if (mappingKind == SudoIndexToIntegerMappingKind.Custom)
        {
            Guard.IsNotNull(mapper);
            return GetUnboundedIndex(sudoIndex, mapper, indexCaster, out outResidual);
        }
        else
        {
            return GetUnboundedIndex(sudoIndex, mappingKind, indexCaster, out outResidual);
        }
    }

    public static int GetUnboundedIndexInRationalNumberSystem<TNumber>
    (
        this SudoIndexToIntegerMappingUnion<TNumber> self,
        TNumber value,
        TNumber first,
        TNumber difference,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : IFloatingPoint<TNumber>
    {
        Guard.IsNotNull(indexCaster);

        TNumber sudoIndex = RationalArithmeticSequenceTheory.GetSudoIndex(value, first, difference);
        int unboundedIndex = 
            GetUnboundedIndex
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