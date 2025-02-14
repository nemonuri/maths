namespace Nemonuri.Maths.Sequences;

public static partial class TolerantIntervalTheory
{
    public static TolerantIntervalSectionKind GetSectionKind<TNumber>
    (
        TNumber value,
        TNumber leftToleranceBoundary,
        TNumber leftMainBoundary,
        TNumber rightMainBoundary,
        TNumber rightToleranceBoundary
    )
        where TNumber : IComparable<TNumber>
    {
        ThrowIfBoundaryArgumentsAreNotValid
        (
            leftToleranceBoundary, 
            leftMainBoundary,
            rightMainBoundary,
            rightToleranceBoundary
        );

        TolerantIntervalSectionKind result;
        
        if 
        (
            TryGetTolerantIntervalSectionKind
            (
                value, 
                leftToleranceBoundary, 
                TolerantIntervalSectionKind.LeftOutside, 
                TolerantIntervalSectionKind.BoundaryOfLeftOutsideAndLeftTolerance,
                out result
            ) ||
            TryGetTolerantIntervalSectionKind
            (
                value, 
                leftMainBoundary, 
                TolerantIntervalSectionKind.LeftTolerance, 
                TolerantIntervalSectionKind.BoundaryOfLeftToleranceAndMain,
                out result
            ) ||
            TryGetTolerantIntervalSectionKind
            (
                value, 
                rightMainBoundary, 
                TolerantIntervalSectionKind.Main, 
                TolerantIntervalSectionKind.BoundaryOfMainAndRightTolerance,
                out result
            ) ||
            TryGetTolerantIntervalSectionKind
            (
                value, 
                rightToleranceBoundary, 
                TolerantIntervalSectionKind.RightTolerance, 
                TolerantIntervalSectionKind.BoundaryOfRightToleranceAndRightOutside,
                out result
            )
        )
        {
            return result;
        }
        else
        {
            return TolerantIntervalSectionKind.RightOutside;
        }

        static void ThrowIfBoundaryArgumentsAreNotValid
        (
            TNumber leftResidualToleranceBoundary,
            TNumber leftMainBoundary,
            TNumber rightMainBoundary,
            TNumber rightResidualToleranceBoundary
        )
        {
            Guard.IsGreaterThanOrEqualTo(leftResidualToleranceBoundary, leftMainBoundary);
            Guard.IsGreaterThanOrEqualTo(leftMainBoundary, rightMainBoundary);
            Guard.IsGreaterThanOrEqualTo(rightMainBoundary, rightResidualToleranceBoundary);
        }

        static bool TryGetTolerantIntervalSectionKind
        (
            TNumber pseudoIndex,
            TNumber boundary,
            TolerantIntervalSectionKind lessResult,
            TolerantIntervalSectionKind equalResult,
            out TolerantIntervalSectionKind outPseudoIndexPositionKinds
        )
        {
            int compareResult = pseudoIndex.CompareTo(boundary);

            if (compareResult < 0)
            {
                outPseudoIndexPositionKinds = lessResult;
                return true;
            }
            else if (compareResult == 0)
            {
                outPseudoIndexPositionKinds = equalResult;
                return true;
            }
            else
            {
                outPseudoIndexPositionKinds = default;
                return true;
            }
        }
    }

    public static bool HasSubKind
    (
        this TolerantIntervalSectionKind kind,
        TolerantIntervalSectionSubKind subKind
    )
    {
        return ((int)kind & (int)subKind) != 0;
    }

    public static bool IsOutside(this TolerantIntervalSectionKind kind)
    {
        return
            kind == TolerantIntervalSectionKind.LeftOutside ||
            kind == TolerantIntervalSectionKind.RightOutside
            ;
    }

    public static ToleranceDirection GetToleranceDirection
    (
        this TolerantIntervalSectionKind sectionKind,
        bool isBoundaryOfLeftOutsideAndLeftToleranceClosed,
        bool isBoundaryOfLeftToleranceAndMainClosed,
        bool isBoundaryOfMainAndRightToleranceClosed,
        bool isBoundaryOfRightToleranceAndRightOutsideClosed
    )
    {
        if 
        (
            sectionKind == TolerantIntervalSectionKind.LeftOutside ||
            (
                isBoundaryOfLeftToleranceAndMainClosed &&
                sectionKind == TolerantIntervalSectionKind.BoundaryOfLeftToleranceAndMain
            ) ||
            (
                isBoundaryOfLeftOutsideAndLeftToleranceClosed &&
                sectionKind == TolerantIntervalSectionKind.BoundaryOfLeftOutsideAndLeftTolerance
            )
        )
        {
            return ToleranceDirection.Left;
        }
        else if 
        (
            sectionKind == TolerantIntervalSectionKind.RightOutside ||
            (
                isBoundaryOfMainAndRightToleranceClosed &&
                sectionKind == TolerantIntervalSectionKind.BoundaryOfMainAndRightTolerance
            ) ||
            (
                isBoundaryOfRightToleranceAndRightOutsideClosed &&
                sectionKind == TolerantIntervalSectionKind.BoundaryOfRightToleranceAndRightOutside
            )
        )
        {
            return ToleranceDirection.Right;
        }
        else
        {
            return ToleranceDirection.None;
        }
    }

    public static bool IsMainLike
    (
        this TolerantIntervalSectionKind sectionKind,
        bool isBoundaryOfLeftToleranceAndMainClosed,
        bool isBoundaryOfMainAndRightToleranceClosed
    )
    {
        if
        (
            sectionKind == TolerantIntervalSectionKind.Main ||
            (
                isBoundaryOfLeftToleranceAndMainClosed &&
                sectionKind == TolerantIntervalSectionKind.BoundaryOfLeftToleranceAndMain
            ) ||
            (
                isBoundaryOfMainAndRightToleranceClosed &&
                sectionKind == TolerantIntervalSectionKind.BoundaryOfMainAndRightTolerance
            )
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static IntervalSectionKind ToIntervalSectionKind(this TolerantIntervalSectionKind source) =>
        source switch
        {
            TolerantIntervalSectionKind.LeftOutside => IntervalSectionKind.LeftOutside,
            TolerantIntervalSectionKind.Main => IntervalSectionKind.Main,
            TolerantIntervalSectionKind.RightOutside => IntervalSectionKind.RightOutside,

            TolerantIntervalSectionKind.BoundaryOfLeftOutsideAndLeftTolerance or 
            TolerantIntervalSectionKind.LeftTolerance or
            TolerantIntervalSectionKind.BoundaryOfLeftToleranceAndMain =>
            IntervalSectionKind.BoundaryOfLeftOutsideAndMain,

            TolerantIntervalSectionKind.BoundaryOfRightToleranceAndRightOutside or 
            TolerantIntervalSectionKind.RightTolerance or
            TolerantIntervalSectionKind.BoundaryOfMainAndRightTolerance =>
            IntervalSectionKind.BoundaryOfRightOutsideAndMain,

            _ => IntervalSectionKind.Unknown
        };

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
        TRaw leftToleranceBoundary,
        BoundaryClosedDirection leftToleranceBoundaryClosedDirection,

        TRaw leftMainBoundary,
        BoundaryClosedDirection leftMainBoundaryClosedDirection,

        TRaw rightMainBoundary,
        BoundaryClosedDirection rightMainBoundaryClosedDirection,

        TRaw rightToleranceBoundary,
        BoundaryClosedDirection rightToleranceBoundaryClosedDirection,
#endregion Tolerant Interval

#region Default Index Mapping
        Func<TRaw, TPseudoIndex>? rawToNormalizedIndexMapping,
        Func<TRaw, TArg1?, TPseudoIndex>? extraArg1AddedRawToNormalizedIndexMapping,
        TArg1? extraArg1,
#endregion Default Index Mapping

#region Tolerance Alternative Index
        AlternativeIndexMode leftToleranceAlternativeIndexMode,
        TPseudoIndex? leftToleranceAlternativeIndex,
        Func<TRaw, TArg2?, TPseudoIndex>? leftToleranceAlternativeMapping,
        TArg2? leftToleranceAlternativeMappingArg,
        
        AlternativeIndexMode rightToleranceAlternativeIndexMode,
        TPseudoIndex? rightToleranceAlternativeIndex,
        Func<TRaw, TArg3?, TPseudoIndex>? rightToleranceAlternativeMapping,
        TArg3? rightToleranceAlternativeMappingArg,
#endregion Tolerance Alternative Index

        [NotNullWhen(true)] out TPseudoIndex? outNormalizedIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        Guard.IsNotNull(extraArg1AddedRawToNormalizedIndexMapping);

        //--- 날값의 구획상 위치 상태를 얻기 ---
        TolerantIntervalSectionKind sectionKind = 
            GetSectionKind
            (
                rawValue,
                leftToleranceBoundary,
                leftMainBoundary,
                rightMainBoundary,
                rightToleranceBoundary
            );
        //---|

        //--- 구획상 위치 상태로부터, 허용 오차 구획 여부 및 종류 얻기 ---
        ToleranceDirection toleranceDirection = 
            sectionKind.GetToleranceDirection
            (
                isBoundaryOfLeftOutsideAndLeftToleranceClosed: leftToleranceBoundaryClosedDirection == BoundaryClosedDirection.Right,
                isBoundaryOfLeftToleranceAndMainClosed: leftMainBoundaryClosedDirection == BoundaryClosedDirection.Left,
                isBoundaryOfMainAndRightToleranceClosed: rightMainBoundaryClosedDirection == BoundaryClosedDirection.Right,
                isBoundaryOfRightToleranceAndRightOutsideClosed: rightToleranceBoundaryClosedDirection == BoundaryClosedDirection.Left
            );
        //---|

        //--- 허용 오차 구획일 경우, 값 성공적으로 반환하기 ---
        if (toleranceDirection == ToleranceDirection.Left)
        {
            //--- 왼쪽 값 구하기 ---
            return
                AlternativeIndexTheory.TryGetAlternativeIndex
                (
                    leftMainBoundary,
                    rawToNormalizedIndexMapping,
                    extraArg1AddedRawToNormalizedIndexMapping,
                    extraArg1,
                    leftToleranceAlternativeIndexMode,
                    leftToleranceAlternativeIndex,
                    leftToleranceAlternativeMapping,
                    leftToleranceAlternativeMappingArg,
                    out outNormalizedIndex
                );
            //---|
        }
        else if (toleranceDirection == ToleranceDirection.Right)
        {
            //--- 오른쪽 값 구하기 ---
            return
                AlternativeIndexTheory.TryGetAlternativeIndex
                (
                    rightMainBoundary,
                    rawToNormalizedIndexMapping,
                    extraArg1AddedRawToNormalizedIndexMapping,
                    extraArg1,
                    rightToleranceAlternativeIndexMode,
                    rightToleranceAlternativeIndex,
                    rightToleranceAlternativeMapping,
                    rightToleranceAlternativeMappingArg,
                    out outNormalizedIndex
                );
            //---|
        }
        //---|

        //--- 바탕 구획일 경우, 정규화된 인덱스로 반환하기 ---
        if
        (
            sectionKind.IsMainLike
            (
                isBoundaryOfLeftToleranceAndMainClosed: leftMainBoundaryClosedDirection == BoundaryClosedDirection.Right,
                isBoundaryOfMainAndRightToleranceClosed: rightMainBoundaryClosedDirection == BoundaryClosedDirection.Left
            )
        )
        {
            outNormalizedIndex = extraArg1AddedRawToNormalizedIndexMapping(rawValue, extraArg1);
            return true;
        }
        //---|

        //--- 바깥 영역일 경우, 실패 반환하기 ---
        if (sectionKind.IsOutside())
        {
            outNormalizedIndex = default;
            return false;
        }
        //---|

        //--- 여기 도달했을 경우, 섹션 종류가 잘못되었다는 오류 던지기
        {
            outNormalizedIndex = default;
            return ThrowHelper.ThrowInvalidOperationException<bool>(/* TODO */);
        }
        //---|
    }

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

#region Default Index Mapping
        IExtraArgumentAttachedMapping<TRaw, TArg1?, TPseudoIndex> rawToNormalizedIndexMapping,
#endregion Default Index Mapping

#region Tolerance Alternative Index
        IAlternativeIndexFactory<TRaw, TArg2?, TPseudoIndex> leftToleranceAlternativeIndexFactory,
        IAlternativeIndexFactory<TRaw, TArg3?, TPseudoIndex> rightToleranceAlternativeIndexFactory,
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

                rawToNormalizedIndexMapping: null,
                extraArg1AddedRawToNormalizedIndexMapping: rawToNormalizedIndexMapping.Mapping,
                extraArg1: rawToNormalizedIndexMapping.ExtraArgument,

                leftToleranceAlternativeIndexMode: leftToleranceAlternativeIndexFactory.Mode,
                leftToleranceAlternativeIndex: leftToleranceAlternativeIndexFactory.AlternativeIndex,
                leftToleranceAlternativeMapping: leftToleranceAlternativeIndexFactory.AlternativeMapping?.Mapping,
                leftToleranceAlternativeMappingArg: leftToleranceAlternativeIndexFactory.AlternativeMapping is {} v1 ? v1.ExtraArgument : default,
                
                rightToleranceAlternativeIndexMode: rightToleranceAlternativeIndexFactory.Mode,
                rightToleranceAlternativeIndex: rightToleranceAlternativeIndexFactory.AlternativeIndex,
                rightToleranceAlternativeMapping: rightToleranceAlternativeIndexFactory.AlternativeMapping?.Mapping,
                rightToleranceAlternativeMappingArg: rightToleranceAlternativeIndexFactory.AlternativeMapping is {} v2 ? v2.ExtraArgument : default,

                out outNormalizedIndex
            );
    }

    public static bool TryGetNormalizedIndex
    <
        TRaw,
        TPseudoIndex,
        TArg2,
        TArg3
    >
    (
        TRaw rawValue,

#region Tolerant Interval
        ITolerantInterval<TRaw> tolerantInterval,
#endregion Tolerant Interval

#region Default Index Mapping
        Func<TRaw, TPseudoIndex> rawToNormalizedIndexMapping,
#endregion Default Index Mapping

#region Tolerance Alternative Index
        IAlternativeIndexFactory<TRaw, TArg2?, TPseudoIndex> leftToleranceAlternativeIndexFactory,
        IAlternativeIndexFactory<TRaw, TArg3?, TPseudoIndex> rightToleranceAlternativeIndexFactory,
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

                rawToNormalizedIndexMapping: rawToNormalizedIndexMapping,
                extraArg1AddedRawToNormalizedIndexMapping: null,
                extraArg1: default(object),

                leftToleranceAlternativeIndexMode: leftToleranceAlternativeIndexFactory.Mode,
                leftToleranceAlternativeIndex: leftToleranceAlternativeIndexFactory.AlternativeIndex,
                leftToleranceAlternativeMapping: leftToleranceAlternativeIndexFactory.AlternativeMapping?.Mapping,
                leftToleranceAlternativeMappingArg: leftToleranceAlternativeIndexFactory.AlternativeMapping is {} v1 ? v1.ExtraArgument : default,
                
                rightToleranceAlternativeIndexMode: rightToleranceAlternativeIndexFactory.Mode,
                rightToleranceAlternativeIndex: rightToleranceAlternativeIndexFactory.AlternativeIndex,
                rightToleranceAlternativeMapping: rightToleranceAlternativeIndexFactory.AlternativeMapping?.Mapping,
                rightToleranceAlternativeMappingArg: rightToleranceAlternativeIndexFactory.AlternativeMapping is {} v2 ? v2.ExtraArgument : default,

                out outNormalizedIndex
            );
    }

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

#region Interval
        TRaw leftBoundary,
        BoundaryClosedDirection leftBoundaryClosedDirection,

        TRaw rightBoundary,
        BoundaryClosedDirection rightBoundaryClosedDirection,
#endregion Interval

#region Default Index Mapping
        Func<TRaw, TPseudoIndex>? rawToNormalizedIndexMapping,
        Func<TRaw, TArg1?, TPseudoIndex> extraArg1AddedRawToNormalizedIndexMapping,
        TArg1? extraArg1,
#endregion Default Index Mapping

        [NotNullWhen(true)] out TPseudoIndex? outNormalizedIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        return
            TryGetNormalizedIndex
            (
                rawValue: rawValue,

                leftToleranceBoundary: leftBoundary,
                leftToleranceBoundaryClosedDirection: leftBoundaryClosedDirection,
                leftMainBoundary: leftBoundary,
                leftMainBoundaryClosedDirection: leftBoundaryClosedDirection,
                rightMainBoundary: rightBoundary,
                rightMainBoundaryClosedDirection: rightBoundaryClosedDirection,
                rightToleranceBoundary: rightBoundary,
                rightToleranceBoundaryClosedDirection: rightBoundaryClosedDirection,

                rawToNormalizedIndexMapping: rawToNormalizedIndexMapping,
                extraArg1AddedRawToNormalizedIndexMapping: extraArg1AddedRawToNormalizedIndexMapping,
                extraArg1: extraArg1,

                leftToleranceAlternativeIndexMode: AlternativeIndexMode.Default,
                leftToleranceAlternativeIndex: default,
                leftToleranceAlternativeMapping: null,
                leftToleranceAlternativeMappingArg: default(TArg2),

                rightToleranceAlternativeIndexMode: AlternativeIndexMode.Default,
                rightToleranceAlternativeIndex: default,
                rightToleranceAlternativeMapping: null,
                rightToleranceAlternativeMappingArg: default(TArg3),

                out outNormalizedIndex
            );
    }
}
