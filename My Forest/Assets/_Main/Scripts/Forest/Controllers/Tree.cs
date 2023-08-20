using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class Tree : MonoBehaviour
    {
        #region FIELDS
        
        private const string APPEAR_STATE_NAME = "Appear";
        private const string IDLE_STATE_NAME = "Idle";

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        private TreeData _treeData = null;
        private TreeConfiguration.TreeConfigurationLevel _currentLevel = null;
        private GameObject _currentTree = null;
        private Vector3 _currentTreeBaseSize = default;

        #endregion

        #region METHODS

        public void Initialize(TreeData treeData, bool withEntryAnimation)
        {
            _treeData = treeData;
            _growthDataSource.GrowthChangedObservable.Subscribe(OnGrowthChanged).AddTo(this);
            
            OnGrowthChanged(_growthDataSource.GrowthData);

            if (withEntryAnimation)
            {
                StartAppearAnimation();
            }
            else
            {
                StartIdleAnimation();
            }
        }

        private void StartIdleAnimation()
        {
            var currentTreeAnimator = _currentTree.GetComponentInChildren<Animator>();
                
            if (currentTreeAnimator == null) return;
                
            currentTreeAnimator.speed = Random.Range(0.8f, 1.2f);
            currentTreeAnimator.Play(IDLE_STATE_NAME, 0, Random.value);
        }

        private void StartAppearAnimation()
        {
            var currentTreeAnimator = _currentTree.GetComponentInChildren<Animator>();
                
            if (currentTreeAnimator == null) return;
                
            currentTreeAnimator.Play(APPEAR_STATE_NAME);
        }

        private void OnGrowthChanged(GrowthData growthData)
        {
            var age = growthData.CurrentGrowth - _treeData.CreationGrowth;
            var currentLevel = _treeData.Configuration.GetConfigurationLevelByAge(age);

            if (currentLevel == null) return;

            if (currentLevel == _currentLevel)
            {
                OnTreeSizeChanged(age);
                return;
            }

            OnTreeLevelChanged(currentLevel);
        }

        private void OnTreeSizeChanged(int age)
        {
            var steps = age - _currentLevel.GrowthNeeded;

            if (steps == 0) return;

            if (_currentLevel.HasMaxSteps)
            {
                steps = Mathf.Clamp(steps, 0, _currentLevel.MaxSizeSteps);
            }

            _currentTree.transform.localScale = (_currentTreeBaseSize + Vector3.one * steps * _currentLevel.SizeStep) * _treeData.SizeVariance;
        }

        private void OnTreeLevelChanged(TreeConfiguration.TreeConfigurationLevel newLevel)
        {
            _currentLevel = newLevel;
            _objectPoolSource.Return(_currentTree);
            _currentTree = _objectPoolSource.Borrow(_currentLevel.Prefab);
            _currentTree.SetLocal(Vector3.zero, transform);

            _currentTreeBaseSize = _currentLevel.Prefab.transform.localScale;
            _currentTree.transform.localScale = _currentTreeBaseSize;

            StartAppearAnimation();
        }

        #endregion
    }
}
