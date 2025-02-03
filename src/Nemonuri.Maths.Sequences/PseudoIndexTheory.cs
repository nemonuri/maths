namespace Nemonuri.Maths.Sequences;

public static class PseudoIndexTheory
{
    public static PseudoIndexPositionKind GetPseudoIndexPositionKinds<TNumber>
    (
        TNumber pseudoIndex,
        TNumber leftResidualToleranceBoundary,
        TNumber leftMainBoundary,
        TNumber rightMainBoundary,
        TNumber rightResidualToleranceBoundary
    )
        where TNumber : IComparable<TNumber>
    {
        ThrowIfBoundaryArgumentsAreNotValid
        (
            leftResidualToleranceBoundary, 
            leftMainBoundary,
            rightMainBoundary,
            rightResidualToleranceBoundary
        );

        PseudoIndexPositionKind result;
        
        if 
        (
            TryGetPseudoIndexPositionKinds
            (
                pseudoIndex, 
                leftResidualToleranceBoundary, 
                PseudoIndexPositionKind.LeftOutside, 
                PseudoIndexPositionKind.BoundaryOfLeftOutsideAndLeftResidualTolerance,
                out result
            ) ||
            TryGetPseudoIndexPositionKinds
            (
                pseudoIndex, 
                leftMainBoundary, 
                PseudoIndexPositionKind.LeftResidualTolerance, 
                PseudoIndexPositionKind.BoundaryOfLeftResidualToleranceAndMain,
                out result
            ) ||
            TryGetPseudoIndexPositionKinds
            (
                pseudoIndex, 
                rightMainBoundary, 
                PseudoIndexPositionKind.Main, 
                PseudoIndexPositionKind.BoundaryOfMainAndRightResidualTolerance,
                out result
            ) ||
            TryGetPseudoIndexPositionKinds
            (
                pseudoIndex, 
                rightResidualToleranceBoundary, 
                PseudoIndexPositionKind.RightResidualTolerance, 
                PseudoIndexPositionKind.BoundaryOfRightResidualToleranceAndRightOutside,
                out result
            )
        )
        {
            return result;
        }
        else
        {
            return PseudoIndexPositionKind.RightOutside;
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

        static bool TryGetPseudoIndexPositionKinds
        (
            TNumber pseudoIndex,
            TNumber boundary,
            PseudoIndexPositionKind lessResult,
            PseudoIndexPositionKind equalResult,
            out PseudoIndexPositionKind outPseudoIndexPositionKinds
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

    public static bool TryGetValueIfPositionKindIsResidualToleranceLike<TNumber>
    (
        PseudoIndexPositionKind positionKind,
        TNumber leftMainBoundary,
        bool isBoundaryOfLeftResidualToleranceAndMainClosed,
        bool isBoundaryOfLeftOutsideAndLeftResidualToleranceClosed,
        TNumber rightMainBoundary,
        bool isBoundaryOfMainAndRightResidualToleranceClosed,
        bool isBoundaryOfRightResidualToleranceAndRightOutsideClosed,
        out TNumber outValue
    )
    {
        if 
        (
            positionKind == PseudoIndexPositionKind.LeftOutside ||
            (
                isBoundaryOfLeftResidualToleranceAndMainClosed &&
                positionKind == PseudoIndexPositionKind.BoundaryOfLeftResidualToleranceAndMain
            ) ||
            (
                isBoundaryOfLeftOutsideAndLeftResidualToleranceClosed &&
                positionKind == PseudoIndexPositionKind.BoundaryOfLeftOutsideAndLeftResidualTolerance
            )
        )
        {
            outValue = leftMainBoundary;
            return true;
        }
        else if 
        (
            positionKind == PseudoIndexPositionKind.RightOutside ||
            (
                isBoundaryOfMainAndRightResidualToleranceClosed &&
                positionKind == PseudoIndexPositionKind.BoundaryOfMainAndRightResidualTolerance
            ) ||
            (
                isBoundaryOfRightResidualToleranceAndRightOutsideClosed &&
                positionKind == PseudoIndexPositionKind.BoundaryOfRightResidualToleranceAndRightOutside
            )
        )
        {
            outValue = rightMainBoundary;
            return true;
        }
        else
        {
            outValue = default!;
            return false;
        }
    }

    public static bool IsPositionKindMainLike
    (
        PseudoIndexPositionKind positionKind,
        bool isBoundaryOfLeftResidualToleranceAndMainClosed,
        bool isBoundaryOfMainAndRightResidualToleranceClosed
    )
    {
        if
        (
            positionKind == PseudoIndexPositionKind.Main ||
            (
                isBoundaryOfLeftResidualToleranceAndMainClosed &&
                positionKind == PseudoIndexPositionKind.BoundaryOfLeftResidualToleranceAndMain
            ) ||
            (
                isBoundaryOfMainAndRightResidualToleranceClosed &&
                positionKind == PseudoIndexPositionKind.BoundaryOfMainAndRightResidualTolerance
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
}