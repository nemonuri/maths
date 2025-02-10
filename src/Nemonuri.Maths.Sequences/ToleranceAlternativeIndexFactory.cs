namespace Nemonuri.Maths.Sequences;

public class ToleranceAlternativeIndexFactory<TRaw, TExtraArgument, TPseudoIndex> :
    IAlternativeIndexFactory<TRaw, TExtraArgument, TPseudoIndex>
#if NET9_0_OR_GREATER
    where TRaw : allows ref struct
    where TExtraArgument : allows ref struct
#endif
{
    public ToleranceAlternativeIndexFactory(AlternativeIndexMode toleranceAlternativeIndexMode, TPseudoIndex? alternativeIndex, IExtraArgumentAttachedMapping<TRaw, TExtraArgument, TPseudoIndex>? alternativeMapping)
    {
        Mode = toleranceAlternativeIndexMode;
        AlternativeIndex = alternativeIndex;
        AlternativeMapping = alternativeMapping;
    }

    public AlternativeIndexMode Mode {get;}
    public TPseudoIndex? AlternativeIndex {get;}
    public IExtraArgumentAttachedMapping<TRaw, TExtraArgument, TPseudoIndex>? AlternativeMapping {get;}
}