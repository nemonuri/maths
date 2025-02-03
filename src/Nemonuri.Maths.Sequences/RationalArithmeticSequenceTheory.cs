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
        TPseudoIndex,
        TFormalIndex
    >
    (
        TRaw raw,
        Func<TRaw, TPseudoIndex> rawToPseudoIndexMapping,
        TPseudoIndex leftResidualToleranceBoundary,
        TPseudoIndex leftMainBoundary,
        TPseudoIndex rightMainBoundary,
        TPseudoIndex rightResidualToleranceBoundary
    )
    {
        //--- 날것 데이터를 유사 인데스로 변환 ---
        TPseudoIndex pseudoIndex = rawToPseudoIndexMapping.Invoke(raw);
        //---|

        //--- 경계선에 맞추기 ---
        //---|

        //--- 정규화(정수화) ---
        //---|

        //--- 유효성 확인(Main에 포함되는지?) ---
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
