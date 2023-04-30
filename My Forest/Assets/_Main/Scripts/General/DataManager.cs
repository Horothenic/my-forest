using System;
using Cysharp.Threading.Tasks;
using Zenject;
using UniRx;

namespace MyForest
{
    public abstract class DataManager<T> where T : class, new()
    {
        #region FIELDS

        [Inject] protected ISaveSource _saveSource = null;

        private DataSubject<T> _dataSubject = new DataSubject<T>();

        protected abstract string Key { get; }
        protected T Data => _dataSubject.Value;
        protected IObservable<T> DataObservable => _dataSubject.AsObservable(true);

        #endregion

        #region METHODS

        protected void Load()
        {
            var data = _saveSource.Load<T>(Key, new T());

            OnLoadReady(ref data);
            _dataSubject.OnNext(data);
        }

        protected void Save()
        {
            _saveSource.Save(Key, Data);
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
                _dataSubject.OnNext(newData);
                return;
            }

            _dataSubject.OnNext();
        }

        protected virtual void OnLoadReady(ref T data) { }

        #endregion
    }
}
