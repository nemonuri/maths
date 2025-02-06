namespace Nemonuri.Maths;

public static class CompareTheory
{
    public static bool IsSatisfyingCompareConditions<T>
    (
        T subject,
        T other,
        CompareConditions compareConditions
    )
        where T : 
            IComparable<T>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
    {
        if 
        (
            (compareConditions & CompareConditions.Less) != 0 &&
            !(subject.CompareTo(other) < 0)
        )
        {
            return false;
        }

        if 
        (
            (compareConditions & CompareConditions.Equal) != 0 &&
            !(subject.CompareTo(other) == 0)
        )
        {
            return false;
        }

        if 
        (
            (compareConditions & CompareConditions.Greater) != 0 &&
            !(subject.CompareTo(other) > 0)
        )
        {
            return false;
        }

        return true;
    }

    public static bool IsBetween<T>(T subject, T other1, T other2)
        where T : 
            IComparable<T>
#if NET9_0_OR_GREATER
            , allows ref struct
#endif
    {
        return
            other1.CompareTo(other2) switch
            {
                0 => subject.CompareTo(other1) == 0,
                < 0 => 
                    (
                        other1.CompareTo(subject) >= 0 &&
                        subject.CompareTo(other2) <= 0
                    ),
                > 0 => 
                    (
                        other2.CompareTo(subject) >= 0 &&
                        subject.CompareTo(other1) <= 0
                    )
            };
    }
}