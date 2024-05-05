using System;

namespace UnityEngine
{
    [Serializable]
    public class Spline
    {
        [SerializeField] private AnimationCurve _map;
        [SerializeField] private Vector2 _evaluationRange;
        
        public float Evaluate(float t)
        {
            return Mathf.Lerp(_evaluationRange.x, _evaluationRange.y, _map.Evaluate(t));
        }
    }
}
