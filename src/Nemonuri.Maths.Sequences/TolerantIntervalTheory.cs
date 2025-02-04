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

    public static bool TryGetValueIfPositionKindIsResidualToleranceLike<TNumber>
    (
        TolerantIntervalSectionKind positionKind,
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
            positionKind == TolerantIntervalSectionKind.LeftOutside ||
            (
                isBoundaryOfLeftResidualToleranceAndMainClosed &&
                positionKind == TolerantIntervalSectionKind.BoundaryOfLeftToleranceAndMain
            ) ||
            (
                isBoundaryOfLeftOutsideAndLeftResidualToleranceClosed &&
                positionKind == TolerantIntervalSectionKind.BoundaryOfLeftOutsideAndLeftTolerance
            )
        )
        {
            outValue = leftMainBoundary;
            return true;
        }
        else if 
        (
            positionKind == TolerantIntervalSectionKind.RightOutside ||
            (
                isBoundaryOfMainAndRightResidualToleranceClosed &&
                positionKind == TolerantIntervalSectionKind.BoundaryOfMainAndRightTolerance
            ) ||
            (
                isBoundaryOfRightResidualToleranceAndRightOutsideClosed &&
                positionKind == TolerantIntervalSectionKind.BoundaryOfRightToleranceAndRightOutside
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
}