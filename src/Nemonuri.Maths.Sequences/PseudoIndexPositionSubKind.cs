namespace Nemonuri.Maths.Sequences;

public enum PseudoIndexPositionSubKind : int
{
    #region Boundary, (Not Boundary)
        Boundary = 1 << 0,
    #endregion

    #region Left, Right, (Not Applicable)
        Left = 1 << 1,
        Right = 1 << 2,
    #endregion

    #region Main, ResidualTolerance, (Outside)
        Main = 1 << 3,
        ResidualTolerance = 1 << 4,
    #endregion
}
