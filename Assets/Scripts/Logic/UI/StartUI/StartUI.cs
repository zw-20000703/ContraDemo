using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{

    private Button _newGame;
    private Button _recentGame;

    private void Start()
    {
        _newGame = this.transform.Find("NewGame").GetComponent<Button>();
        _recentGame = this.transform.Find("RecentGame").GetComponent<Button>();
        _newGame.onClick.AddListener(OnNewGameButton);
        _recentGame.onClick.AddListener(OnRecentButton);
    }

    private void OnNewGameButton()
    {
        Debug.Log("场景加载成功!");
        //发送加载人物消息
        //发送加载场景消息
        CmdManager.instance.SendCmd(new EnterMapCmd() { MapID = 1 });
        //实际的配置表还没有创建
    }

    private void OnRecentButton()
    {
        //加载存档窗口
        Debug.LogError("加载最近游戏");
    }
    
}
