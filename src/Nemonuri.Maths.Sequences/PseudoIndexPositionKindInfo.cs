namespace Nemonuri.Maths.Sequences;

public readonly struct PseudoIndexPositionKindInfo
{
    public PseudoIndexPositionKindInfo(PseudoIndexPositionKind pseudoIndexPositionKinds, PseudoIndexPositionInMainKind pseudoIndexPositionInMainKind)
    {
        PseudoIndexPositionKinds = pseudoIndexPositionKinds;
        PseudoIndexPositionInMainKind = pseudoIndexPositionInMainKind;
    }

    public PseudoIndexPositionKind PseudoIndexPositionKinds {get;}
    public PseudoIndexPositionInMainKind PseudoIndexPositionInMainKind {get;}
}
