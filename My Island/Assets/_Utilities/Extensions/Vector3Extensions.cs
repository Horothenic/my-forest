using Newtonsoft.Json;
using System;

namespace UnityEngine
{
    public static class Vector3Extensions
    {
        public static Vector3 Deserialize(this SerializedVector3 serializedVector3)
        {
            return new Vector3(serializedVector3.X, serializedVector3.Y, serializedVector3.Z);
        }

        public static SerializedVector3 Serialize(this Vector3 vector3)
        {
            return new SerializedVector3(vector3);
        }

        public static Vector3 SetX(this Vector3 vector, float x)
        {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 SetY(this Vector3 vector, float y)
        {
            return new Vector3(vector.x, y, vector.z);
        }

        public static Vector3 SetZ(this Vector3 vector, float z)
        {
            return new Vector3(vector.x, vector.y, z);
        }
    }

    [Serializable]
    public struct SerializedVector3
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }

        [JsonConstructor]
        public SerializedVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public SerializedVector3(Vector3 vector3)
        {
            X = vector3.x;
            Y = vector3.y;
            Z = vector3.z;
        }

        public static implicit operator Vector3(SerializedVector3 serializedVector) => serializedVector.Deserialize();
        public static implicit operator SerializedVector3(Vector3 vector) => vector.Serialize();
    }
}
