using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            CreateInstance();
            return instance;
        }
    }

    public static void CreateInstance(bool bDontDestroy = false)
    {
        if (instance == null)
        {
            GameObject go = new GameObject(typeof(T).Name);
            instance = go.AddComponent<T>();
            instance.Init();
            if (bDontDestroy)
            {
                DontDestroyOnLoad(go);
            }
        }
    }

    public static void DestroyInstance()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            instance.Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //Debug.LogError("Error: " + typeof(T).Name + " Has More than One instance!!!!");
        }
    }

    void OnDestroy()
    {
        if (instance != null)
        {
            UnInit();
            instance = null;
        }
    }

    protected virtual void Init()
    {
        
    }

    protected virtual void UnInit()
    {
        
    }
}