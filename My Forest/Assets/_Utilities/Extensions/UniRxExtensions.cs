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
}
