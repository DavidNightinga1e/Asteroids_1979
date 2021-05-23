using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void AutoGetChildComponent<T>(this MonoBehaviour monoBehaviour, out T component) where T : Component
    {
        component = monoBehaviour.GetComponentInChildren<T>();
    }
    
    public static void AutoFindComponent<T>(this MonoBehaviour _, out T component) where T : Component
    {
        component = Object.FindObjectOfType<T>();
    }
}