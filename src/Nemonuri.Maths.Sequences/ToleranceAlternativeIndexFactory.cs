namespace Nemonuri.Maths.Sequences;

public class ToleranceAlternativeIndexFactory<TRaw, TExtraArgument, TPseudoIndex> :
    IToleranceAlternativeIndexFactory<TRaw, TExtraArgument, TPseudoIndex>
#if NET9_0_OR_GREATER
    where TRaw : allows ref struct
    where TExtraArgument : allows ref struct
#endif
{
    public ToleranceAlternativeIndexFactory(ToleranceAlternativeIndexMode toleranceAlternativeIndexMode, TPseudoIndex? alternativeIndex, IExtraArgumentAttachedMapping<TRaw, TExtraArgument, TPseudoIndex>? alternativeMapping)
    {
        ToleranceAlternativeIndexMode = toleranceAlternativeIndexMode;
        AlternativeIndex = alternativeIndex;
        AlternativeMapping = alternativeMapping;
    }

    public ToleranceAlternativeIndexMode ToleranceAlternativeIndexMode {get;}
    public TPseudoIndex? AlternativeIndex {get;}
    public IExtraArgumentAttachedMapping<TRaw, TExtraArgument, TPseudoIndex>? AlternativeMapping {get;}
}