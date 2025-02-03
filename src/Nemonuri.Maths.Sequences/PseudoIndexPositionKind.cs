using S = Nemonuri.Maths.Sequences.PseudoIndexPositionSubKind;

namespace Nemonuri.Maths.Sequences;

public enum PseudoIndexPositionKind : int
{
    Unknown = 0,

    LeftOutside = S.Left,
    LeftResidualTolerance = S.Left | S.ResidualTolerance,
    Main = S.Main,
    RightResidualTolerance = S.Right | S.ResidualTolerance,
    RightOutside = S.Right,

    BoundaryOfLeftOutsideAndLeftResidualTolerance = LeftOutside | S.Boundary | LeftResidualTolerance,
    BoundaryOfLeftResidualToleranceAndMain = LeftResidualTolerance | S.Boundary | Main,
    BoundaryOfMainAndRightResidualTolerance = Main | S.Boundary | RightResidualTolerance,
    BoundaryOfRightResidualToleranceAndRightOutside = RightResidualTolerance | S.Boundary | RightOutside

}
