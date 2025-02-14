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

#region Default Index Mapping
        Func<TRaw, TPseudoIndex>? defaultIndexMapping,
        Func<TRaw, TArg1?, TPseudoIndex>? extraArg1AddedDefaultIndexMapping,
        TArg1? extraArg1,
#endregion Default Index Mapping

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
            if (defaultIndexMapping is {} v1) 
            {
                outIndex = v1.Invoke(rawValue);
                return true;
            }
            else if (extraArg1AddedDefaultIndexMapping is {} v2)
            {
                outIndex = v2.Invoke(rawValue, extraArg1);
                return true;
            }
            else
            {
                outIndex = default;
                return false;
            }
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
#region Default Index Mapping
        IExtraArgumentAttachedMapping<TRaw, TArg1?, TPseudoIndex>? defaultIndexMapping,
#endregion Default Index Mapping
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

                defaultIndexMapping: null,
                extraArg1AddedDefaultIndexMapping: defaultIndexMapping?.Mapping,
                extraArg1: defaultIndexMapping is {} v1 ? v1.ExtraArgument : default,

                alternativeIndexMode: alternativeIndexFactory.Mode,
                alternativeIndex: alternativeIndexFactory.AlternativeIndex,
                alternativeMapping: alternativeIndexFactory.AlternativeMapping?.Mapping,
                alternativeMappingArg: alternativeIndexFactory.AlternativeMapping is {} v2 ? v2.ExtraArgument : default,

                out outIndex
            );
    }

    public static bool TryGetAlternativeIndex
    <
        TRaw, 
        TPseudoIndex,
        TArg1
    >
    (
        this IAlternativeIndexFactory<TRaw, TArg1?, TPseudoIndex> alternativeIndexFactory,
        TRaw rawValue,
#region Default Index Mapping
        Func<TRaw, TPseudoIndex>? defaultIndexMapping,
#endregion Default Index Mapping
        [NotNullWhen(true)] out TPseudoIndex? outIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        Guard.IsNotNull(alternativeIndexFactory);

        return
            TryGetAlternativeIndex<TRaw, TPseudoIndex, object, TArg1>
            (
                rawValue: rawValue,

                defaultIndexMapping: defaultIndexMapping,
                extraArg1AddedDefaultIndexMapping: null,
                extraArg1: default,

                alternativeIndexMode: alternativeIndexFactory.Mode,
                alternativeIndex: alternativeIndexFactory.AlternativeIndex,
                alternativeMapping: alternativeIndexFactory.AlternativeMapping?.Mapping,
                alternativeMappingArg: alternativeIndexFactory.AlternativeMapping is {} v2 ? v2.ExtraArgument : default,

                out outIndex
            );
    }
}