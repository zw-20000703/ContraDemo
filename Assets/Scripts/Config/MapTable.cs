using System;
using System.Collections.Generic;

/// <summary>
/// 地图表数据结构
/// </summary>
public class MapDataBse : TableDataBase
{
    public string Name;//场景名字
    public string SceneName;//场景文件名称
}

/// <summary>
/// 地图表
/// </summary>
public class MapTable : ConfigTable<MapDataBse,MapTable>
{

    public MapTable()
    {
        Load("Config/MapTable.csv");
    }

}
