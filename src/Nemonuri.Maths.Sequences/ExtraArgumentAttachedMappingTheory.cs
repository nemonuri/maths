namespace Nemonuri.Maths.Sequences;

public static class ExtraArgumentAttachedMappingTheory
{
    public static TTarget Invoke<TSource, TExtraArgument, TTarget>
    (
        this IExtraArgumentAttachedMapping<TSource, TExtraArgument, TTarget> mapping,
        TSource source
    )
#if NET9_0_OR_GREATER
    where TSource : allows ref struct
    where TExtraArgument : allows ref struct
    where TTarget : allows ref struct
#endif
    {
        Guard.IsNotNull(mapping);

        return mapping.Mapping.Invoke(source, mapping.ExtraArgument);
    }
}