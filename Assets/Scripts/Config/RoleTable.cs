using System;
using System.Collections.Generic;

public class RoleDataBase : TableDataBase
{
    public string Name;//角色名称
    public string ModelPath;//模型路径
}

/// <summary>
/// 角色表
/// </summary> 
public class RoleTable : ConfigTable<RoleDataBase,RoleTable>
{

    public RoleTable()
    {
        Load("");
    }

}
