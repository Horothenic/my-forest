using System;

namespace UnityEngine
{
    public static class Repeater
    {
        public static void Repeat(int times, Action action)
        {
            for (var i = 0; i < times; i ++)
            {
                action?.Invoke();
            }
        }
    }
}