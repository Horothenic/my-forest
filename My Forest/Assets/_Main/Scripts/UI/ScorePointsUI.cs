using UnityEngine;
using System;

using Zenject;
using UniRx;
using TMPro;

namespace MyForest
{
    public interface IScorePointsUIDataSource
    {
        IObservable<ScoreData> ScoreChangedObservable { get; }
    }

    public class ScorePointsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IScorePointsUIDataSource _dataSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _text = null;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Clean();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _dataSource.ScoreChangedObservable.Subscribe(UpdateText).AddTo(_disposables);
        }

        private void Clean()
        {
            _disposables.Dispose();
        }

        private void UpdateText(ScoreData scoreData)
        {
            if (scoreData == null) return;

            _text.text = scoreData.Score.ToString();
        }

        #endregion
    }
}
