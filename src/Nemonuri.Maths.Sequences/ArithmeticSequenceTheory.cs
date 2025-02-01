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

    public static int GetUnboundedIndexFromSudoIndexInRationalNumberSystem<TNumber>
    (
        TNumber sudoIndex,
        Func<TNumber, int> indexCaster,
        out TNumber sudoIndexMinusUnboundedIndex
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        var unboundedIndex = TNumber.Floor(sudoIndex);
        sudoIndexMinusUnboundedIndex = sudoIndex - unboundedIndex;
        return indexCaster.Invoke(unboundedIndex);
    }

    public static int GetBoundedIndexInRationalNumberSystem<TNumber>
    (
        int unboundedIndex,
        TNumber sudoIndexMinusUnboundedIndex,
        RawStructIndexBoundary<TNumber> leftBoundary,
        RawStructIndexBoundary<TNumber> rightBoundary
    )
        where TNumber : 
            IFloatingPoint<TNumber>
    {
        //--- 왼쪽 경계 점검 ---
        if (leftBoundary.BoundaryKind == BoundaryKind.None)
        {
            goto RightBoundary;
        }

        int anchorIndexMinusOne = leftBoundary.AnchorIndex - 1;
        if (anchorIndexMinusOne < unboundedIndex)
        {
            goto RightBoundary;
        }
        else if (anchorIndexMinusOne == unboundedIndex)
        {
            goto Label1;
        }
        else if (anchorIndexMinusOne > unboundedIndex)
        {
            goto OutOfRange;
        }
        else
        {
            return ThrowHelper.ThrowInvalidOperationException<int>();
        }
        //---|

    Label1:
        //--- 왼쪽 경계 판정 ---
        if (leftBoundary.BoundaryKind == BoundaryKind.Close)
        {
            if (sudoIndexMinusUnboundedIndex < )
            {

            }
        }
        else if (leftBoundary.BoundaryKind == BoundaryKind.Open)
        {

        }
        else
        {
            //--- 오류 ---

            //---|
        }
        //---|

    RightBoundary:
        //--- 오른쪽 경계 점검 ---
        throw new NotImplementedException();
        //---|
    
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
        return GetUnboundedIndexFromSudoIndexInRationalNumberSystem(sudoIndex, indexCaster, out sudoIndexMinusUnboundedIndex);
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
