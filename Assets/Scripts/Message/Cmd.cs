using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd
{
	


}

public class EnterMapCmd : Cmd
{
    public int MapID;//场景ID
}

public class LoadRoleCmd : Cmd
{
    public int ThisID;//角色唯一ID
    public string Name;//角色名称
    public int Blood;//角色血量
    public Vector3 Pos;//角色生成坐标
}
