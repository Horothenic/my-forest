using UnityEngine;

using Zenject;
using UniRx;
using TMPro;

namespace MyForest
{
    public class AllTimeGrowthPointsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _dataSource = null;

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
            _dataSource.GrowthChangedObservable.Subscribe(UpdateText).AddTo(_disposables);
        }

        private void Clean()
        {
            _disposables.Dispose();
        }

        private void UpdateText(GrowthData growthData)
        {
            if (growthData == null) return;

            _text.text = growthData.AllTimeGrowth.ToString();
        }

        #endregion
    }
}
