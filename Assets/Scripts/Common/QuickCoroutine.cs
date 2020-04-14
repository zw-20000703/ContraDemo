using System.Collections;
using UnityEngine;

/// <summary>
/// 快速协程
/// </summary>
class QuickCoroutine : Singleton<QuickCoroutine>
{
    /// <summary>
    /// 这个不用管
    /// </summary>
    public class Mono : MonoBehaviour { };

    GameObject _coroutineRoot;

    MonoBehaviour _coroutineMono;

    /// <summary>
    /// 在GameManager中初始化快速协程
    /// </summary>
    public void Initialize()
    {
        _coroutineRoot = new GameObject("QuickCoroutine");
        GameObject.DontDestroyOnLoad(_coroutineRoot);
        _coroutineMono = _coroutineRoot.AddComponent<Mono>();
    }

    /// <summary>
    /// 使用协程就调用这个方法
    /// </summary>
    /// <param name="coroutine">不用管返回值，直接调用就可以了</param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return _coroutineMono.StartCoroutine(coroutine);
    }

}
