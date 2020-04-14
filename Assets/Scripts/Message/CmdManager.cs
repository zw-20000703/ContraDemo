using System;
using System.Collections.Generic;
using UnityEngine;

class CmdManager : Singleton<CmdManager>
{

    /// <summary>
    /// 消息解析函数
    /// </summary>
    private Dictionary<Type, Action<Cmd>> _parse = new Dictionary<Type, Action<Cmd>>();

    /// <summary>
    /// 消息缓存
    /// </summary>
    private List<Cmd> _cache = new List<Cmd>();

    private bool _pause;

    public bool Pause
    {
        get
        {
            return _pause;
        }
        set
        {
            _pause = value;
            if (value == false)
            {
                Receive(null);
            }
        }
    }

    public CmdManager()
    {
        //注册消息解析函数
        _parse.Add(typeof(EnterMapCmd), ScenesManager.OnEnterMap);
        //_parse.Add();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="cmd"></param>
    public void SendCmd(Cmd cmd)
    {
        Receive(cmd);
    }

    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="cmd"></param>
    public void Receive(Cmd cmd)
    {
        if (cmd != null)
        {
            _cache.Add(cmd);
        }
        if (Pause)
        {
            return;
        }
        foreach (var item in _cache)
        {
            Action<Cmd> function;
            //分发消息
            if (_parse.TryGetValue(item.GetType(),out function))
            {
                function?.Invoke(item);
            }
        }
        _cache.Clear();
    }

    /// <summary>
    /// 消息检测
    /// </summary>
    public bool CheckCmd(Cmd cmd, Type targetType)
    {
        if (cmd.GetType() != targetType) 
        {
            Debug.LogError(string.Format("需要{0}，但接收到了{1}", targetType, cmd.GetType()));
            return false;
        }
        return true;
    }
}
