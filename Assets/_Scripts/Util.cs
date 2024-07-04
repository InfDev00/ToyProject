using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    public static T GetOrAddComponent<T>(this Object obj) where T : Component
    {
        var go = obj.GameObject();
        if (go == null) return null;

        var comp = go.GetComponent<T>();
        if (comp == null) go.AddComponent<T>();

        return comp;
    }

    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * timeScale;
    }
}