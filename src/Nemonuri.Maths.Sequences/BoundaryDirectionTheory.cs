namespace Nemonuri.Maths.Sequences;

public static class BoundaryDirectionTheory
{
    public static CompareConditions ToCompareConditions(this BoundaryDirection direction) =>
        direction switch
        {
            BoundaryDirection.Left => CompareConditions.Greater,
            BoundaryDirection.Right => CompareConditions.Less,
            _ => ThrowHelper.ThrowInvalidOperationException<CompareConditions>()
        };
} 