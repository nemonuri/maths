namespace Nemonuri.Maths.Sequences;

public static partial class TolerantIntervalTheory
{
    public static bool TryGetNormalizedIndex
    <
        TRaw,
        TPseudoIndex,
        TArg1,
        TArg2,
        TArg3
    >
    (
        TRaw rawValue,

#region Tolerant Interval
        ITolerantInterval<TRaw> tolerantInterval,
#endregion Tolerant Interval

        IExtraArgumentAttachedMapping<TRaw, TArg1?, TPseudoIndex> rawToNormalizedIndexMapping,

#region Tolerance Alternative Index
        IToleranceAlternativeIndexFactory<TRaw, TArg2?, TPseudoIndex> leftToleranceAlternativeIndexFactory,
        IToleranceAlternativeIndexFactory<TRaw, TArg2?, TPseudoIndex> rightToleranceAlternativeIndexFactory,
#endregion Tolerance Alternative Index

        [NotNullWhen(true)] out TPseudoIndex? outNormalizedIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        return
            TryGetNormalizedIndex
            (
                rawValue: rawValue,

                leftToleranceBoundary: tolerantInterval.LeftTolerance.Anchor,
                leftToleranceBoundaryClosedDirection: tolerantInterval.LeftTolerance.ClosedDirection,
                leftMainBoundary: tolerantInterval.LeftMain.Anchor,
                leftMainBoundaryClosedDirection: tolerantInterval.LeftMain.ClosedDirection,
                rightMainBoundary: tolerantInterval.RightMain.Anchor,
                rightMainBoundaryClosedDirection: tolerantInterval.RightMain.ClosedDirection,
                rightToleranceBoundary: tolerantInterval.RightTolerance.Anchor,
                rightToleranceBoundaryClosedDirection: tolerantInterval.RightTolerance.ClosedDirection,

                rawToNormalizedIndexMapping: rawToNormalizedIndexMapping.Mapping,
                rawToNormalizedIndexMappingArg: rawToNormalizedIndexMapping.ExtraArgument,

                
            )
    }
}
