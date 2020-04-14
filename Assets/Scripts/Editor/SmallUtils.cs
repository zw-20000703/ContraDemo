using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

/// <summary>
/// 小工具类，使用即可，不要随意修改
/// </summary>
public class SmallUtils : Editor
{

    /// <summary>
    /// 自定义菜单栏，一键返回Setup场景
    /// </summary>
    [MenuItem("Custom/GotoSetup")]
    private static void GotoSetup()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/InitializeScene/Setup.unity");
    }

}
