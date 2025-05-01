using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace MyForest
{
    public partial class SceneManager : MonoBehaviour
    {
        private const float STANDARD_WAIT_TIME = 1f;
        
        [Header("COMPONENTS")]
        [SerializeField] private GameObject _transitionGameObject;
        [SerializeField] private CanvasGroup _transitionCanvasGroup;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private float _transitionTime = 0.35f;
        
        private async UniTask LoadSceneAsync(string sceneName)
        {
            _transitionGameObject.SetActive(true);
            await _transitionCanvasGroup.DOFade(1, _transitionTime).From(0).AsyncWaitForCompletion();

            await UniTask.Delay(TimeSpan.FromSeconds(STANDARD_WAIT_TIME));
            
            await UnitySceneManager.LoadSceneAsync(sceneName);

            await _transitionCanvasGroup.DOFade(0, _transitionTime).AsyncWaitForCompletion();
            _transitionGameObject.SetActive(false);
        }
    }
    
    public partial class SceneManager : ISceneSource
    {
        void ISceneSource.LoadScene(string sceneName)
        {
            LoadSceneAsync(sceneName).Forget();
        }
    }
}
