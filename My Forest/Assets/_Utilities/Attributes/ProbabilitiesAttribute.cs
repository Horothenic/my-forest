using System;

namespace UnityEngine
{
    public class ProbabilitiesAttribute : PropertyAttribute
    {
        public Type Type { get; }

        public ProbabilitiesAttribute(Type type)
        {
            Type = type;
        }
    }
}
