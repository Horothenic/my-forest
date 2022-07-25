using UniRx;
using System;

namespace UniRx
{
    public static class UniRxExtensions
    {
        public static IDisposable Subscribe(this IObservable<Unit> observable, Action action)
        {
            void onNext(Unit unit)
            {
                action?.Invoke();
            }

            return observable.Subscribe(onNext);
        }

        public static void AddTo(this IDisposable disposable, CompositeDisposable compositeDisposable)
        {
            compositeDisposable.Add(disposable);
        }

        public static void OnNext<T>(this AsyncSubject<T> subject)
        {
            subject.OnNext(subject.Value);
        }
    }

    public class DataSubject<T> : ISubject<T>, IObserver<T>, IObservable<T>, IDisposable, IOptimizedObservable<T>
    {
        private Subject<T> subject = null;
        public T Value { get; private set; } = default;

        public DataSubject()
        {
            subject = new Subject<T>();
            Value = default;
        }

        public DataSubject(T value)
        {
            subject = new Subject<T>();
            Value = value;
        }

        public IObservable<T> AsObservable(bool emit = false)
        {
            if (emit)
            {
                return subject.StartWith(Value);
            }

            return subject.AsObservable();
        }

        public void Dispose()
        {
            subject.Dispose();
        }

        public bool IsRequiredSubscribeOnCurrentThread()
        {
            return subject.IsRequiredSubscribeOnCurrentThread();
        }

        public void OnCompleted()
        {
            subject.OnCompleted();
        }

        public void OnError(Exception error)
        {
            subject.OnError(error);
        }

        public void OnNext(T value)
        {
            Value = value;
            subject.OnNext(value);
        }

        public void OnNext()
        {
            subject.OnNext(Value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return subject.Subscribe(observer);
        }
    }
}
