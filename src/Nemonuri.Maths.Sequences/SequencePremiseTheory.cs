namespace Nemonuri.Maths.Sequences;

public static class SequencePremiseTheory
{
    public static bool IsInfinite<T>(this ISequencePremise<T> self)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        return self.Count < 0;
    }

    public static T GetItem<T>(this ISequencePremise<T> self, int index)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);
        Guard.IsInRange(0, self.Count, index);

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

    public static int GetIndex<T>(this IIndexGetableSequencePremise<T> self, T item)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        if (self.TryGetIndex(item, out int index))
        {
            Guard.IsInRange(0, self.Count, index);
            return index;
        }
        else
        {
            return ThrowHelper.ThrowInvalidOperationException<int>();
        }
    }

    public static bool MoveNext<T>(this ISequencePremise<T> self, ref T current)
        where T : struct
    {
        Guard.IsNotNull(self);

        return self.TryGetNext(current, out current);
    }

    public static bool MoveNext<T>(this ISequencePremise<T> self, T? current)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        return self.TryGetNext(current, out _);
    }


    public static bool MoveNext<T>
    (
        this ISequencePremise<T> self, 
        ref int index,
        ref T current
    )
        where T : struct
    {
        Guard.IsNotNull(self);

        if 
        (
            index < self.Count &&
            self.TryGetNext(current, out current)
        )
        {
            SetNextIndex(ref index);

            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool MoveNext<T>
    (
        this ISequencePremise<T> self, 
        ref int index,
        T? current
    )
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
    {
        Guard.IsNotNull(self);

        if 
        (
            index < self.Count &&
            self.TryGetNext(current, out _)
        )
        {
            SetNextIndex(ref index);

            return true;
        }
        else
        {
            return false;
        }
    }


    private static void SetNextIndex(ref int index)
    {
        if (index < 0)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
    }
}