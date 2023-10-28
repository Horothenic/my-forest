using System;

using Cysharp.Threading.Tasks;
using Zenject;
using UniRx;

namespace MyForest
{
    public abstract partial class DataManager<T> : IInitializable
    {
        [Inject] private Debug.IGameDebugSource _gameDebugSource = null;

        void IInitializable.Initialize()
        {
            _gameDebugSource.OnResetGameObservable.Subscribe(Reset);
            Load();
            Initialize();
        }

        protected virtual void Initialize() { }
    }

    public abstract partial class DataManager<T> where T : class, new()
    {
        #region FIELDS

        [Inject] protected ISaveSource _saveSource = null;

        protected CompositeDisposable _disposables = new CompositeDisposable();
        private readonly DataSubject<T> _preLoadSubject = new DataSubject<T>();
        private readonly Subject<T> _postLoadSubject = new Subject<T>();

        protected abstract string Key { get; }
        protected T Data => _preLoadSubject.Value;
        protected IObservable<T> PreLoadObservable => _preLoadSubject.AsObservable();
        protected IObservable<T> PostLoadObservable => _postLoadSubject.AsObservable();

        #endregion

        #region METHODS

        private void Load()
        {
            var data = _saveSource.Load<T>(Key, new T());

            OnPreLoad(ref data);
            _preLoadSubject.OnNext(data);
            OnPostLoad(data);
            _postLoadSubject.OnNext(data);
        }

        protected void Save()
        {
            _saveSource.Save(Key, Data);
        }

        private void Reset()
        {
            _disposables.Dispose();
            _disposables = new CompositeDisposable();

            _saveSource.Delete(Key);
            Load();
        }

        protected void EmitData(T newData = null)
        {
            EmitDataAsync(newData).Forget();
        }

        private async UniTaskVoid EmitDataAsync(T newData = null)
        {
            await UniTask.NextFrame();

            if (newData != null)
            {
                _preLoadSubject.OnNext(newData);
                return;
            }

            _preLoadSubject.OnNext();
        }

        protected virtual void OnPreLoad(ref T data){ }

        protected virtual void OnPostLoad(T data) { }

        #endregion
    }
}
