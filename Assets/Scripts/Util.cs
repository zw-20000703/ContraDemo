using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 通用工具类
/// </summary>
public static class Util
{

    public static bool FloatEquals(float a, float b)
    {
        return Mathf.Abs(a - b) < 0.001F;
    }

}
