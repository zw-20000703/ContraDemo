using System;

/// <summary>
/// 单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new ()
{
    private static T _instance;

    static Singleton()
    {
        _instance = new T();
    }

    public static T instance
    {
        get
        {
            return _instance;
        }
    }

}
