using System;

using Cysharp.Threading.Tasks;
using Zenject;
using UniRx;

namespace MyIsland
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
        protected abstract SaveStyle SaveStyle { get; }
        
        protected T Data => _loadSubject.Value;
        protected IObservable<T> DataObservable => _loadSubject.AsObservable();

        #endregion

        #region METHODS

        private void Load()
        {
            var data = SaveStyle switch
            {
                SaveStyle.Json => _saveSource.LoadJson(Key, new T()),
                SaveStyle.File => _saveSource.LoadFile(Key, new T()),
            };

            OnPreLoad(ref data);
            _loadSubject.OnNext(data);
        }

        protected void Save()
        {
            switch (SaveStyle)
            {
                case SaveStyle.Json:
                    _saveSource.SaveJson(Key, Data);
                    break;
                case SaveStyle.File:
                    _saveSource.SaveFile(Key, Data);
                    break;
            }
        }

        private void Reset()
        {
            _disposables.Dispose();
            _disposables = new CompositeDisposable();

            switch (SaveStyle)
            {
                case SaveStyle.Json:
                    _saveSource.DeleteJson(Key);
                    break;
                case SaveStyle.File:
                    _saveSource.DeleteFile(Key);
                    break;
            }
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

        protected void SaveAndEmit()
        {
            Save();
            EmitData();
        }

        protected virtual void OnPreLoad(ref T data){ }

        #endregion
    }
}
