using System;

namespace UnityEngine
{
    public class ProbabilityAttribute : PropertyAttribute
    {
        public string Title { get; }
        public Type EnumType { get; }

        public ProbabilityAttribute(string title, Type enumType)
        {
            Title = title;
            EnumType = enumType;
        }
    }
}
