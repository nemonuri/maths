namespace Nemonuri.Maths.Sequences;

public interface IToleranceAlternativeIndexFactoryPairProvider<TNumber, TArg1, TArg2>
#if NET9_0_OR_GREATER
    where TNumber : allows ref struct
    where TArg1 : allows ref struct
    where TArg2 : allows ref struct
#endif
{
    IAlternativeIndexFactory<TNumber, TArg1?, TNumber> LeftToleranceAlternativeIndexFactory {get;}
    IAlternativeIndexFactory<TNumber, TArg2?, TNumber> RightToleranceAlternativeIndexFactory {get;}
}
