namespace Nemonuri.Maths.Sequences;

public static class AlternativeIndexTheory
{
    public static bool TryGetAlternativeIndex
    <
        TRaw, 
        TPseudoIndex, 
        TArg1, 
        TArg2
    >
    (
        TRaw rawValue,
        Func<TRaw, TArg1?, TPseudoIndex>? defaultIndexMapping,
        TArg1? defaultIndexMappingArg,
        AlternativeIndexMode alternativeIndexMode,
        TPseudoIndex? alternativeIndex,
        Func<TRaw, TArg2?, TPseudoIndex>? alternativeMapping,
        TArg2? alternativeMappingArg,
        [NotNullWhen(true)] out TPseudoIndex? outIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        if (alternativeIndexMode == AlternativeIndexMode.Default)
        {
            if (defaultIndexMapping is null) 
            {
                outIndex = default;
                return false;
            }
            outIndex = defaultIndexMapping.Invoke(rawValue, defaultIndexMappingArg);
            return true;
        }
        else if (alternativeIndexMode == AlternativeIndexMode.AlternativeIndex)
        {
            Guard.IsNotNull(alternativeIndex);
            outIndex = alternativeIndex;
            return true;
        }
        else if (alternativeIndexMode == AlternativeIndexMode.AlternativeMapping)
        {
            Guard.IsNotNull(alternativeMapping);
            outIndex = alternativeMapping.Invoke(rawValue, alternativeMappingArg);
            return true;
        }
        else
        {
            outIndex = default;
            return false;
        }
    }
    
    public static bool TryGetAlternativeIndex
    <
        TRaw, 
        TPseudoIndex, 
        TArg1, 
        TArg2
    >
    (
        this IAlternativeIndexFactory<TRaw, TArg2?, TPseudoIndex> alternativeIndexFactory,
        TRaw rawValue,
        IExtraArgumentAttachedMapping<TRaw, TArg1?, TPseudoIndex>? defaultIndexMapping,
        [NotNullWhen(true)] out TPseudoIndex? outIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        Guard.IsNotNull(alternativeIndexFactory);

        return
            TryGetAlternativeIndex
            (
                rawValue: rawValue,

                defaultIndexMapping: defaultIndexMapping?.Mapping,
                defaultIndexMappingArg: defaultIndexMapping is {} v1 ? v1.ExtraArgument : default,

                alternativeIndexMode: alternativeIndexFactory.Mode,
                alternativeIndex: alternativeIndexFactory.AlternativeIndex,
                alternativeMapping: alternativeIndexFactory.AlternativeMapping?.Mapping,
                alternativeMappingArg: alternativeIndexFactory.AlternativeMapping is {} v2 ? v2.ExtraArgument : default,

                out outIndex
            );
    }
}