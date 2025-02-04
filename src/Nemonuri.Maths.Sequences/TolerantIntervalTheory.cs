namespace Nemonuri.Maths.Sequences;

public static class TolerantIntervalTheory
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

    public static bool TryGetNormalizedIndex<TRaw, TPseudoIndex, TArg>
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

        Func<TRaw, TArg?, TPseudoIndex> rawToNormalizedIndexMapping,
        TArg? mappingExtraArg,

        TPseudoIndex leftToleranceAlternativeIndex,
        TPseudoIndex rightToleranceAlternativeIndex,

        [NotNullWhen(true)] out TPseudoIndex? outNormalizedIndex
    )
        where TRaw : IComparable<TRaw>
        where TPseudoIndex : IComparable<TPseudoIndex>
    {
        Guard.IsNotNull(rawToNormalizedIndexMapping);

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
            outNormalizedIndex = leftToleranceAlternativeIndex;
            return true;
        }
        else if (toleranceDirection == ToleranceDirection.Right)
        {
            outNormalizedIndex = rightToleranceAlternativeIndex;
            return true;
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
            outNormalizedIndex = rawToNormalizedIndexMapping(rawValue, mappingExtraArg);
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
}