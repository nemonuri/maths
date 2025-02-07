namespace Nemonuri.Maths.Sequences;

public class BoundedSequence<T> : IReadOnlyList<T>
{
    public IBoundableSequencePremise<T> Premise {get;}
    
    public T LeftBoundary {get;}
    public BoundaryClosedDirection LeftBoundaryClosedDirection {get;}
    public T RightBoundary {get;}
    public BoundaryClosedDirection RightBoundaryClosedDirection {get;}

    public T ZeroIndex {get;}
    
    public int Count {get;}

    public BoundedSequence
    (
        IBoundableSequencePremise<T> premise,

#region Tolerant Interval
        T leftToleranceBoundary,
        BoundaryClosedDirection leftToleranceBoundaryClosedDirection,

        T leftMainBoundary,
        BoundaryClosedDirection leftMainBoundaryClosedDirection,

        T rightMainBoundary,
        BoundaryClosedDirection rightMainBoundaryClosedDirection,

        T rightToleranceBoundary,
        BoundaryClosedDirection rightToleranceBoundaryClosedDirection,
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

        ZeroIndex = zeroIndex;

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
        private readonly BoundedSequence<T> _innerSource;

        private T _current;

        public Enumerator(BoundedSequence<T> innerSource)
        {
            _innerSource = innerSource;
            _current = _innerSource.ZeroIndex;
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
            _current = _innerSource.ZeroIndex;
        }
    }
}
