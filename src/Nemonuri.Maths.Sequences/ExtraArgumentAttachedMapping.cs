namespace Nemonuri.Maths.Sequences;

public class ExtraArgumentAttachedMapping<TSource, TExtraArgument, TTarget> :
    IExtraArgumentAttachedMapping<TSource, TExtraArgument, TTarget>
#if NET9_0_OR_GREATER
    where TSource : allows ref struct
    where TTarget : allows ref struct
#endif
{
    public ExtraArgumentAttachedMapping
    (
        Func<TSource, TExtraArgument?, TTarget> mapping, 
        TExtraArgument? extraArgument
    )
    {
        Guard.IsNotNull(mapping);

        Mapping = mapping;
        ExtraArgument = extraArgument;
    }

    public Func<TSource, TExtraArgument?, TTarget> Mapping {get;}

    public TExtraArgument? ExtraArgument {get;}
}