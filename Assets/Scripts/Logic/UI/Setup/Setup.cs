using UnityEngine;

public class Setup : MonoBehaviour
{

    public void Start()
    {
        //闪屏
        //这个闪屏就是宣传自己的界面，就是类似于王者荣耀刚打开的timi那个。
        //游戏加载
        GameManager.instance.Initialize();
    }

}
