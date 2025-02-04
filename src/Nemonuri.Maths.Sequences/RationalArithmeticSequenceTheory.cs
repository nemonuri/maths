namespace Nemonuri.Maths.Sequences;

public static partial class RationalArithmeticSequenceTheory
{
#if NET7_0_OR_GREATER
    public static TNumber GetPseudoIndex<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        var v1 = (value - first)/difference;
        var v2 = TNumber.One / (TNumber.One + TNumber.One);
        return v1 - v2;
    }

    public static bool TryGetNormalizedIndex
    <
        TRaw,
        TPseudoIndex
    >
    (
        TRaw rawValue,

#region Section Data
        TRaw leftToleranceBoundary,
        BoundaryClosedDirection leftToleranceBoundaryClosedDirection,

        TRaw leftMainBoundary,
        BoundaryClosedDirection leftMainBoundaryClosedDirection,

        TRaw rightMainBoundary,
        BoundaryClosedDirection rightMainBoundaryClosedDirection,

        TRaw rightToleranceBoundary,
        BoundaryClosedDirection rightToleranceBoundaryClosedDirection,
#endregion Section Data

        Func<TRaw, TPseudoIndex> rawToNormalizedIndexMapping,

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
            TolerantIntervalTheory.GetSectionKind
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
            outNormalizedIndex = rawToNormalizedIndexMapping(rawValue);
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

    public static int GetBoundedIndex<TNumber>
    (
        int unboundedIndex,
        TNumber residual,
        RawStructIndexBoundary<TNumber> leftBoundary,
        RawStructIndexBoundary<TNumber> rightBoundary
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        //--- 왼쪽 경계 점검 ---
        switch (leftBoundary.GetHavingState(unboundedIndex, residual, BoundaryDirection.Left))
        {
            case BoundaryHavingState.Main:
                break;
            case BoundaryHavingState.ResidualTolerance:
                return leftBoundary.AnchorIndex;
            default:
                return ThrowHelper.ThrowInvalidOperationException<int>();
        }
        //---|

        //--- 오른쪽 경계 점검 ---
        switch (rightBoundary.GetHavingState(unboundedIndex, residual, BoundaryDirection.Right))
        {
            case BoundaryHavingState.Main:
                return unboundedIndex;
            case BoundaryHavingState.ResidualTolerance:
                return rightBoundary.AnchorIndex;
            default:
                return ThrowHelper.ThrowInvalidOperationException<int>();
        }
        //---|
    }

    public static int GetUnboundedIndex<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference,
        Func<TNumber, int> indexCaster,
        out TNumber residual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        var sudoIndex = GetPseudoIndex(value, first, difference);
        return GetUnboundedIndexUsingFloor(sudoIndex, indexCaster, out residual);
    }

    public static int GetBoundedIndex<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference,
        Func<TNumber, int> indexCaster,
        RawStructIndexBoundary<TNumber> leftBoundary,
        RawStructIndexBoundary<TNumber> rightBoundary
    )
        where TNumber : 
            IFloatingPoint<TNumber>



    public static TNumber GetIndex<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference,
        TNumber leftTolerance,
        TNumber rightTolerance,
        BoundaryKind leftBoundaryKind = BoundaryKind.Close,
        BoundaryKind rightBoundaryKind = BoundaryKind.Open
    )
        where TNumber : IFloatingPoint<TNumber>
    {
        var v1 = (value - first)/difference;
        var v2 = TNumber.One / (TNumber.One + TNumber.One);
        var middleValue = v1 - v2;

        TNumber leftValue, rightValue, flooredValue;

        flooredValue = TNumber.Floor(middleValue);

        //--- underflow
        if (flooredValue < TNumber.Zero)
        {

        }
        //---|

        //--- overflow
        //else if ()
        //---|
        
    }
#endif
}
