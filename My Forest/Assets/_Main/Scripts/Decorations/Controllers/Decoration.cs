using DG.Tweening;
using UnityEngine;

using Zenject;

namespace MyForest
{
    public class Decoration : MonoBehaviour
    {
        #region FIELDS
        
        [Inject] private IObjectPoolSource _objectPoolSource = null;
        
        private GameObject _currentDecoration = null;
        private Tween _scaleTween = null;
        
        #endregion

        #region METHODS

        public void Initialize(DecorationData decorationData, bool withEntryAnimation)
        {
            SetDecoration(decorationData);
            
            if (withEntryAnimation)
            {
                TriggerEntryAnimation();
            }
        }
        
        private void SetDecoration(DecorationData decorationData)
        {
            _objectPoolSource.Return(_currentDecoration);
            _currentDecoration = _objectPoolSource.Borrow(decorationData.Configuration.GetVariation(decorationData.Variation));
            _currentDecoration.SetLocal(Vector3.zero, Vector3.up * decorationData.Rotation, transform);
        }
        
        private void TriggerEntryAnimation()
        {
            _scaleTween?.Kill();

            var currentScale = _currentDecoration.transform.localScale;
            var startScale = currentScale * Constants.ForestElements.START_SCALE_FACTOR;
            
            _scaleTween = _currentDecoration.transform.DOScale(currentScale, Constants.ForestElements.SCALE_TRANSITION_TIME).From(startScale).SetEase(Ease.OutBounce);
        }

        #endregion
    }
}
