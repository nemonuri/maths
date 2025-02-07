namespace Nemonuri.Maths.Sequences;

public class Sequence<T> : IReadOnlyList<T>
{
    public IBoundableSequencePremise<T> Premise {get;}
    public T LeftBoundary {get;}
    public BoundaryClosedDirection LeftBoundaryClosedDirection {get;}
    public T RightBoundary {get;}
    public BoundaryClosedDirection RightBoundaryClosedDirection {get;}
    public T First {get;}
    
    public int Count {get;}

    public Sequence
    (
        IBoundableSequencePremise<T> premise,

#region Tolerant Interval
        T leftBoundary, 
        BoundaryClosedDirection leftBoundaryClosedDirection, 
        T rightBoundary, 
        BoundaryClosedDirection rightBoundaryClosedDirection,
#endregion Tolerant Interval

        T zeroIndex
    )
    {
        Guard.IsNotNull(premise);

        Premise = premise;

        LeftBoundary = leftBoundary;
        LeftBoundaryClosedDirection = leftBoundaryClosedDirection;
        RightBoundary = rightBoundary;
        RightBoundaryClosedDirection = rightBoundaryClosedDirection;

        First = zeroIndex;

        Count = premise.GetCount
            (
                leftBoundary,
                leftBoundaryClosedDirection,
                rightBoundary,
                rightBoundaryClosedDirection
            );
    }

    public T this[int index] => Premise.GetItem(index);
   

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Enumerator : IEnumerator<T>
    {
        private readonly Sequence<T> _innerSource;

        private T _current;

        public Enumerator(Sequence<T> innerSource)
        {
            _innerSource = innerSource;
            _current = _innerSource.First;
        }

        public T Current => _current;
        object IEnumerator.Current => Current!;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_innerSource.Premise.TryGetSuccessor(_current, out T? outSuccessor))
            {
                _current = outSuccessor;
            }
            
            return false;
        }

        public void Reset()
        {
            _current = _innerSource.First;
        }
    }
}
