namespace Nemonuri.Maths.Sequences;

public static class SequencePremiseTheory
{

    public static T GetItem<T>(this ISequencePremise<T> self, int index)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        if (self.TryGetItem(index, out T? outItem))
        {
            return outItem;
        }
        else
        {
            ThrowHelper.ThrowInvalidOperationException();
            return default;
        }
    }

    public static int GetIndex<T>(this ISequenceItemToIndexPremise<T> self, T item)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        if (self.TryGetIndex(item, out int index))
        {
            return index;
        }
        else
        {
            return ThrowHelper.ThrowInvalidOperationException<int>();
        }
    }

}