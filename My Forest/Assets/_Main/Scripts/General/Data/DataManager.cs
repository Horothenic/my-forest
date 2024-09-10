using System;

using Cysharp.Threading.Tasks;
using Zenject;
using UniRx;

namespace MyForest
{
    public abstract partial class DataManager<T> : IInitializable
    {
        void IInitializable.Initialize()
        {
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
        private readonly DataSubject<T> _loadSubject = new DataSubject<T>();

        protected abstract string Key { get; }
        protected T Data => _loadSubject.Value;
        protected IObservable<T> LoadObservable => _loadSubject.AsObservable();

        #endregion

        #region METHODS

        private void Load()
        {
            var data = _saveSource.Load<T>(Key, new T());

            OnPreLoad(ref data);
            _loadSubject.OnNext(data);
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
                _loadSubject.OnNext(newData);
                return;
            }

            _loadSubject.OnNext();
        }

        protected virtual void OnPreLoad(ref T data){ }

        #endregion
    }
}
