using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager : Singleton<GameManager>
{
    GameObject _engineRoot;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        //加载第一个游戏界面
        SceneManager.LoadScene("StartScene");
        //初始化快速协程
        QuickCoroutine.instance.Initialize();
        //初始化游戏引擎
        if (_engineRoot == null)
        {
            _engineRoot = new GameObject("GameEngine");
            GameObject.DontDestroyOnLoad(_engineRoot);
            _engineRoot.AddComponent<GameEngine>();
        }
    }

}
