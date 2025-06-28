#if UNITY_IOS || UNITY_ANDROID
namespace UnityEngine.Advertisements
{
    public enum AdShowStatus
    {
        Pending,
        Started,
        Clicked,
        Skipped,
        Failed,
        Completed,
        NotInitialized,
        Unknown
    }
    
    public enum AdLoadStatus
    {
        Pending,
        Loaded,
        Failed,
        NotInitialized
    }
}
#endif
