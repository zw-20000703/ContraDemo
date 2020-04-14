using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理类
/// </summary>
class ScenesManager : Singleton<ScenesManager>
{
    /// <summary>
    /// 当前角色所在地图编号
    /// </summary>
    public int MapIndex;

    public static void OnEnterMap(Cmd cmd)
    {
        if (!CmdManager.instance.CheckCmd(cmd, typeof(EnterMapCmd)))
        {
            return;
        }
        EnterMapCmd enterMapCmd = cmd as EnterMapCmd;
        ScenesManager.instance.LoadScene(enterMapCmd.MapID);

    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="mapIndex">地图编号</param>
    private void LoadScene(int mapIndex)
    {
        var mapDataBase = MapTable.instance[mapIndex];
        if (mapDataBase == null) 
        {
            Debug.LogError("不存在的地图" + mapIndex);
            return;
        }
        CmdManager.instance.Pause = true;
        AsyncOperation ao = SceneManager.LoadSceneAsync(mapDataBase.SceneName);
        QuickCoroutine.instance.StartCoroutine(EndScene(ao));
    }

    private IEnumerator EndScene(AsyncOperation ao)
    {
        if (!ao.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("场景加载完毕");
        CmdManager.instance.Pause = false;
    }
}
