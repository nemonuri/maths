using S = Nemonuri.Maths.Sequences.TolerantIntervalSectionSubKind;

namespace Nemonuri.Maths.Sequences;

public enum TolerantIntervalSectionKind : int
{
    Unknown = 0,

    LeftOutside = S.Left,
    LeftTolerance = S.Left | S.Tolerance,
    Main = S.Main,
    RightTolerance = S.Right | S.Tolerance,
    RightOutside = S.Right,

    BoundaryOfLeftOutsideAndLeftTolerance = LeftOutside | S.Boundary | LeftTolerance,
    BoundaryOfLeftToleranceAndMain = LeftTolerance | S.Boundary | Main,
    BoundaryOfMainAndRightTolerance = Main | S.Boundary | RightTolerance,
    BoundaryOfRightToleranceAndRightOutside = RightTolerance | S.Boundary | RightOutside

}
