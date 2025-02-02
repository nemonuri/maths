namespace Nemonuri.Maths.Sequences;

public static class ArithmeticSequenceTheory
{
#if NET7_0_OR_GREATER
    public static TNumber GetSudoIndexInRationalNumberSystem<TNumber>
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

    public static int GetUnboundedIndexUsingFloorInRationalNumberSystem<TNumber>
    (
        TNumber sudoIndex,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        var unboundedIndex = TNumber.Floor(sudoIndex);
        outResidual = sudoIndex - unboundedIndex;
        return indexCaster.Invoke(unboundedIndex);
    }

    public static int GetUnboundedIndexUsingCeilingInRationalNumberSystem<TNumber>
    (
        TNumber sudoIndex,
        Func<TNumber, int> indexCaster,
        out TNumber outResidual
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        var unboundedIndex = TNumber.Ceiling(sudoIndex);
        outResidual = unboundedIndex - sudoIndex;
        return indexCaster.Invoke(unboundedIndex);
    }

    public static int GetBoundedIndexInRationalNumberSystem<TNumber>
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
        {
            if (leftBoundary.BoundaryKind == BoundaryKind.None)
            {
                goto RightBoundary;
            }

            int anchorIndex = leftBoundary.AnchorIndex;
            if (anchorIndex < unboundedIndex)
            {
                goto RightBoundary;
            }
            else if (anchorIndex >= unboundedIndex)
            {
                goto Label1;
            }
            else
            {
                return ThrowHelper.ThrowInvalidOperationException<int>();
            }
        }
        //---|

    Label1:
        {
            //--- 왼쪽 경계 판정 ---
            bool isResidualInTolerance;

            if (leftBoundary.BoundaryKind == BoundaryKind.Close)
            {
                isResidualInTolerance = residual <= leftBoundary.ResidualTolerance;
            }
            else if (leftBoundary.BoundaryKind == BoundaryKind.Open)
            {
                isResidualInTolerance = residual < leftBoundary.ResidualTolerance;
            }
            else
            {
                return ThrowHelper.ThrowInvalidOperationException<int>();
            }
            //---|

            //--- 왼쪽 경계로 값 맞추기 ---
            if (isResidualInTolerance)
            {
                return leftBoundary.AnchorIndex;
            }
            else
            {
                goto OutOfRange;
            }
            //---|
        }


    RightBoundary:
        //--- 오른쪽 경계 점검 ---
        {
            if (rightBoundary.BoundaryKind == BoundaryKind.None)
            {
                return unboundedIndex;
            }

            int anchorIndex = rightBoundary.AnchorIndex;
            if (anchorIndex < unboundedIndex)
            {
                return unboundedIndex;
            }
            else if (anchorIndex >= unboundedIndex)
            {
                goto Label2;
            }
            else
            {
                return ThrowHelper.ThrowInvalidOperationException<int>();
            }
        }
        //---|

    Label2:
        {
            //--- 오른쪽 경계 판정 ---
            bool isResidualInTolerance;

            if (rightBoundary.BoundaryKind == BoundaryKind.Close)
            {
                isResidualInTolerance = residual <= rightBoundary.ResidualTolerance;
            }
            else if (rightBoundary.BoundaryKind == BoundaryKind.Open)
            {
                isResidualInTolerance = residual < rightBoundary.ResidualTolerance;
            }
            else
            {
                return ThrowHelper.ThrowInvalidOperationException<int>();
            }
            //---|

            //--- 오른쪽 경계로 값 맞추기 ---
            if (isResidualInTolerance)
            {
                return rightBoundary.AnchorIndex;
            }
            else
            {
                goto OutOfRange;
            }
            //---|
        }
    
    OutOfRange:
        //--- 벗어남 판정 ---
        throw new NotImplementedException();
        //---|
    }




    public static int GetUnboundedIndexInRationalNumberSystem<TNumber>
    (
        TNumber value,
        TNumber first,
        TNumber difference,
        Func<TNumber, int> indexCaster,
        out TNumber sudoIndexMinusUnboundedIndex
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        var sudoIndex = GetSudoIndexInRationalNumberSystem(value, first, difference);
        return GetUnboundedIndexUsingFloorInRationalNumberSystem(sudoIndex, indexCaster, out sudoIndexMinusUnboundedIndex);
    }



    public static TNumber GetIndexInRationalNumberSystem<TNumber>
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
