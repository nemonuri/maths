namespace Nemonuri.Maths.Sequences;

public readonly struct PseudoIndexPositionKindInfo
{
    public PseudoIndexPositionKindInfo(TolerantIntervalSectionKind pseudoIndexPositionKinds, PseudoIndexPositionInMainKind pseudoIndexPositionInMainKind)
    {
        PseudoIndexPositionKinds = pseudoIndexPositionKinds;
        PseudoIndexPositionInMainKind = pseudoIndexPositionInMainKind;
    }

    public TolerantIntervalSectionKind PseudoIndexPositionKinds {get;}
    public PseudoIndexPositionInMainKind PseudoIndexPositionInMainKind {get;}
}
