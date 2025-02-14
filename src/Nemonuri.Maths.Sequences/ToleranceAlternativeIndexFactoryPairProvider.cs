namespace Nemonuri.Maths.Sequences;

public class ToleranceAlternativeIndexFactoryPairProvider<TNumber, TArg1, TArg2> : IToleranceAlternativeIndexFactoryPairProvider<TNumber, TArg1, TArg2>
#if NET9_0_OR_GREATER
    where TNumber : allows ref struct
    where TArg1 : allows ref struct
    where TArg2 : allows ref struct
#endif
{
    public ToleranceAlternativeIndexFactoryPairProvider
    (
        IAlternativeIndexFactory<TNumber, TArg1?, TNumber> leftToleranceAlternativeIndexFactory, 
        IAlternativeIndexFactory<TNumber, TArg2?, TNumber> rightToleranceAlternativeIndexFactory
    )
    {
        Guard.IsNotNull(leftToleranceAlternativeIndexFactory);
        Guard.IsNotNull(rightToleranceAlternativeIndexFactory);

        LeftToleranceAlternativeIndexFactory = leftToleranceAlternativeIndexFactory;
        RightToleranceAlternativeIndexFactory = rightToleranceAlternativeIndexFactory;
    }

    public IAlternativeIndexFactory<TNumber, TArg1?, TNumber> LeftToleranceAlternativeIndexFactory { get; }
    public IAlternativeIndexFactory<TNumber, TArg2?, TNumber> RightToleranceAlternativeIndexFactory { get; }
}