using System.Collections;

namespace Nemonuri.Maths.Sequences;

public class EnumerableSuccessor<T> : IEnumerable<T>
{
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

        private T _current;

        public Enumerator(EnumerableSuccessor<T> innerSource)
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