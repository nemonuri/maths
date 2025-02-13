namespace Nemonuri.Maths.Sequences;

public class EnumerableSuccessor<T> : IEnumerable<T>
{
    [NotNull]
    public T First {get;}
    public ISuccessorPremise<T> Premise {get;}

    public EnumerableSuccessor(T first, ISuccessorPremise<T> premise)
    {
        Guard.IsNotNull(first);
        Guard.IsNotNull(premise);

        First = first;
        Premise = premise;
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class Enumerator : IEnumerator<T>
    {
        private readonly EnumerableSuccessor<T> _innerSource;

        private bool _inited;
        private T _current;

        public Enumerator(EnumerableSuccessor<T> innerSource)
        {
            _innerSource = innerSource;
            Reset();
        }

        public T Current => _current;
        object IEnumerator.Current => Current!;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (!_inited) 
            {
                _inited = true;
                return true;
            }

            if (_innerSource.Premise.TryGetSuccessor(_current, out T? outSuccessor))
            {
                _current = outSuccessor;
            }
            
            return false;
        }

        [MemberNotNull(nameof(_current))]
        public void Reset()
        {
            _inited = false;
            _current = _innerSource.First;
        }
    }
}