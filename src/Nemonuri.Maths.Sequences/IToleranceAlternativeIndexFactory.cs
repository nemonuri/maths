namespace Nemonuri.Maths.Sequences;

public interface IToleranceAlternativeIndexFactory<TRaw, TExtraArgument, TPseudoIndex>
#if NET9_0_OR_GREATER
    where TRaw : allows ref struct
    where TExtraArgument : allows ref struct
    where TPseudoIndex : allows ref struct
#endif
{
    ToleranceAlternativeIndexMode Mode {get;}
    TPseudoIndex? AlternativeIndex {get;}
    IExtraArgumentAttachedMapping<TRaw, TExtraArgument, TPseudoIndex>? AlternativeMapping {get;}
}
