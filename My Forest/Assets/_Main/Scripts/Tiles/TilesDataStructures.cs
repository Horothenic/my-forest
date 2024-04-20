using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public struct Coordinates
    {
        public int Q { get; private set; }
        public int R { get; private set; }
        
        public static implicit operator Coordinates((int x, int y) tuple) => new Coordinates(tuple.x, tuple.y);
        public static implicit operator (int x, int y)(Coordinates coordinates) => (coordinates.Q, coordinates.R);
        
        [JsonConstructor]
        public Coordinates(int q, int r)
        {
            Q = q;
            R = r;
        }
    }
}
