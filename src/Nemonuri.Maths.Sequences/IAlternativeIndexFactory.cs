namespace Nemonuri.Maths.Sequences;

public interface IAlternativeIndexFactory<TRaw, TExtraArgument, TPseudoIndex>
#if NET9_0_OR_GREATER
    where TRaw : allows ref struct
    where TExtraArgument : allows ref struct
    where TPseudoIndex : allows ref struct
#endif
{
    AlternativeIndexMode Mode {get;}
    TPseudoIndex? AlternativeIndex {get;}
    IExtraArgumentAttachedMapping<TRaw, TExtraArgument, TPseudoIndex>? AlternativeMapping {get;}
}
