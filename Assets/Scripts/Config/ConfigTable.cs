using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

/// <summary>
/// 表数据结构父类
/// </summary>
public class TableDataBase
{
    public int ID;
}

/// <summary>
/// 配置表父类
/// </summary>
public class ConfigTable< TDataBase, T > : Singleton<T>
    where TDataBase : TableDataBase,new () 
    where T : new ()
{

    /// <summary>
    /// 数据存储
    /// </summary>
    private Dictionary<int, TDataBase> _cache = new Dictionary<int, TDataBase>();

    protected void Load(string tablePath)
    {
        MemoryStream stream;
        //分离开发期和运行期间读取数据的地方
        //后期会在SmallUnil类中添加一键导入配置文件的功能
#if UNITY_EDITOR
        //开发期读Project/Config
        var tableBytes = File.ReadAllBytes(Application.dataPath + "/../" + tablePath);
        stream = new MemoryStream(tableBytes);
#else
        //运行期读Resources/Config
        var table = Resources.Load<TextAsset>(tablePath);
        stream = new MemoryStream(table.bytes);
#endif
        using (var reader = new StreamReader(stream, Encoding.GetEncoding("gb2312"))) 
        {
            //读取文件头，通过反射取到表结构的字段
            //前提是配置表的表头要的数据结构的字段要匹配
            var fieldNameString = reader.ReadLine();
            var fieldNameArray = fieldNameString.Split(',');
            List<FieldInfo> allField = new List<FieldInfo>();
            foreach (var item in fieldNameArray)
            {
                allField.Add(typeof(TDataBase).GetField(item));
            }
            //读取正式数据从第二行开始
            var lineString = reader.ReadLine();
            while (lineString != null)
            {
                TDataBase TDB = ReadLine(allField, lineString);
                _cache[TDB.ID] = TDB;
                lineString = reader.ReadLine();
            }
        }
    }

    private static TDataBase ReadLine(List<FieldInfo> allField, string lineString)
    {
        var lineArray = lineString.Split(',');
        var TDB = new TDataBase();
        for (int i = 0; i < lineArray.Length; i++)
        {
            var field = allField[i];
            var data = lineArray[i];
            if (field.FieldType == typeof(int))
            {
                field.SetValue(TDB, int.Parse(data));
            }
            else if (field.FieldType == typeof(string))
            {
                field.SetValue(TDB, data);
            }
            else if (field.FieldType == typeof(float))
            {
                field.SetValue(TDB, Single.Parse(data));
            }
            else if (field.FieldType == typeof(bool))
            {
                field.SetValue(TDB, bool.Parse(data));
            }
            else if (field.FieldType == typeof(List<int>))
            {
                var list = new List<int>();
                foreach (var item in data.Split('$'))
                {
                    list.Add(int.Parse(item));
                }
                field.SetValue(TDB, list);
            }
            else if (field.FieldType == typeof(List<string>))
            {
                var list = new List<string>(data.Split('$'));
                field.SetValue(TDB, list);
            }
            else if (field.FieldType == typeof(List<float>))
            {
                var list = new List<float>();
                foreach (var item in data.Split('$'))
                {
                    list.Add(float.Parse(item));
                }
                field.SetValue(TDB, list);
            }
        }
        return TDB;
    }

    /// <summary>
    /// 通过索引器获取表格数据
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public TDataBase this[int index]
    {
        get
        {
            TDataBase td;
            _cache.TryGetValue(index,out td);
            return td;
        }
    }

}
