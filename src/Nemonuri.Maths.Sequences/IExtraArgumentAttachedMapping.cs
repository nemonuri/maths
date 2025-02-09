namespace Nemonuri.Maths.Sequences;

public interface IExtraArgumentAttachedMapping<TSource, TExtraArgument, TTarget>
#if NET9_0_OR_GREATER
    where TSource : allows ref struct
    where TExtraArgument : allows ref struct
    where TTarget : allows ref struct
#endif
{
    Func<TSource, TExtraArgument?, TTarget> Mapping {get;}
    TExtraArgument? ExtraArgument {get;}
}
