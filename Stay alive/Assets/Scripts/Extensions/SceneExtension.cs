using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneExtension
{
    public static T GetRoot<T>(this Scene scene) where T : MonoBehaviour
    {
        var rootObjects = scene.GetRootGameObjects();
        
        T result = default;
        foreach (var gameObject in rootObjects)
        {
            if (gameObject.TryGetComponent(out result))
            {
                break;
            }
        }
        
        return result;
    }
}
