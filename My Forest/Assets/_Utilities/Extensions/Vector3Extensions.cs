using System;

namespace UnityEngine
{
    public static class Vector3Extensions
    {
        public static Vector3 Deserialize(this SerializedVector3 serializedVector3)
        {
            return new Vector3(serializedVector3.x, serializedVector3.y, serializedVector3.z);
        }

        public static SerializedVector3 Serialize(this Vector3 vector3)
        {
            return new SerializedVector3(vector3);
        }
    }

    [Serializable]
    public class SerializedVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializedVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public SerializedVector3(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        public static implicit operator Vector3(SerializedVector3 serializedVector) => serializedVector.Deserialize();
        public static implicit operator SerializedVector3(Vector3 vector) => vector.Serialize();
    }
}
