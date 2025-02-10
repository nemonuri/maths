using S = Nemonuri.Maths.Sequences.TolerantIntervalSectionSubKind;

namespace Nemonuri.Maths.Sequences;

public enum IntervalSectionKind : int
{
    Unknown = 0,

    LeftOutside = S.Left,
    Main = S.Main,
    RightOutside = S.Right,

    BoundaryOfLeftOutsideAndMain = LeftOutside | S.Boundary | Main,
    BoundaryOfRightOutsideAndMain = Main | S.Boundary | RightOutside
}