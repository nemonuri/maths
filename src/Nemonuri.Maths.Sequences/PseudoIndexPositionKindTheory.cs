namespace Nemonuri.Maths.Sequences;

public static class PseudoIndexPositionKindTheory
{
    public static bool HasSubKind
    (
        this PseudoIndexPositionKind kind,
        PseudoIndexPositionSubKind subKind
    )
    {
        return ((int)kind & (int)subKind) != 0;
    }

    public static bool IsOutside(this PseudoIndexPositionKind kind)
    {
        return
            kind == PseudoIndexPositionKind.LeftOutside ||
            kind == PseudoIndexPositionKind.RightOutside
            ;
    }
}