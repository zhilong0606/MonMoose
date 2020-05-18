public class Singleton<T> where T : class, new()
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            CreateInstace();
            return instance;
        }
    }

    public static void CreateInstace()
    {
        if (instance == null)
        {
            instance = new T();
            (instance as Singleton<T>).Init();
        }
    }

    public static void DestroyInstance()
    {
        if (instance != null)
        {
            (instance as Singleton<T>).UnInit();
            instance = null;
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }

    protected virtual void Init()
    {

    }

    protected virtual void UnInit()
    {

    }


}
