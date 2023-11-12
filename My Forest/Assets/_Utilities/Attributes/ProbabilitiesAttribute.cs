using System;

namespace UnityEngine
{
    public class ProbabilitiesAttribute : PropertyAttribute
    {
        public string Title { get; }
        public Type EnumType { get; }

        public ProbabilitiesAttribute(string title, Type enumType)
        {
            Title = title;
            EnumType = enumType;
        }
    }
}
