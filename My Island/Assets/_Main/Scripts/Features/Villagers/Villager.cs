using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace MyIsland
{
    public class Villager : MonoBehaviour
    {
        #region FIELDS

        private const int MAX_TRIES_FOR_RANDOM_POINT = 30;
        private const float MIN_DISTANCE_TO_TARGET = 0.5f;

        [Header("COMPONENTS")]
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private float _waitTime = 3f;
        [SerializeField] private float _radius = 30f;
        
        #endregion
        
        #region METHODS

        private void Start()
        {
            WanderLoop().Forget();
        }

        private async UniTaskVoid WanderLoop()
        {
            _navMeshAgent.ResetPath();
            _navMeshAgent.SetDestination(GetRandomNavMeshPoint());

            await UniTask.WaitUntil(
                () => !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= MIN_DISTANCE_TO_TARGET,
                cancellationToken: this.GetCancellationTokenOnDestroy()
            );
            
            await UniTask.Delay(
                TimeSpan.FromSeconds(_waitTime),
                cancellationToken: this.GetCancellationTokenOnDestroy());
            
            WanderLoop().Forget();
        }
        
        private Vector3 GetRandomNavMeshPoint()
        {
            for (var i = 0; i < MAX_TRIES_FOR_RANDOM_POINT; i++)
            {
                var randomDirection = transform.position + UnityEngine.Random.insideUnitSphere * _radius;

                if (!NavMesh.SamplePosition(randomDirection, out var hit, 5f, NavMesh.AllAreas)) continue;
                
                var path = new NavMeshPath();
                if (_navMeshAgent.CalculatePath(hit.position, path) && path.status == NavMeshPathStatus.PathComplete) return hit.position;
            }

            return transform.position;
        }
        
        #endregion
    }
}
