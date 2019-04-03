using System;
using System.Collections.Generic;
using System.Reflection;
using Tool;

/// <summary>
/// 枚举成员的别名
/// </summary>
public class ShowEnumAlias
{
    public List<string> alias;
    private List<int> num;
    public ShowEnumAlias(Type t)
    {
        alias = new List<string>();
        num = new List<int>();
        FieldInfo[] f = t.GetFields(BindingFlags.Static | BindingFlags.Public);
        for (int i = 0; i < f.Length; i++)
        {
            object[] objs = f[i].GetCustomAttributes(typeof(EnumAliasAttribute), false);//如果目标元素没有应用特性实例 则返回null，
            string s = ((EnumAliasAttribute)objs[0]).Name;
            alias.Add(s);
            num.Add((int)Enum.Parse(t, f[i].Name));
        }
    }

    public string GetAlias(int enu)
    {
        for (int i = 0; i < num.Count; i++)
        {
            if (num[i] == enu) return alias[i];
        }
        return null;
    }

    public string GetAlias(int index, params object[] a)
    {
        return alias[index];
    }

    public int GetEnum(string alia)
    {
        for (int i = 0; i < alias.Count; i++)
        {
            if (alia.Contains(alias[i])) return num[i];
        }
        return 0;
    }

    public int GetEnum(int index)
    {
        return num[index];
    }

    public int GetIndex(string alia)
    {
        for (int i = 0; i < alias.Count; i++)
        {
            if (alia.Contains(alias[i])) return i;
        }
        return 0;
    }

    public int GetIndex(int enu)
    {
        for (int i = 0; i < num.Count; i++)
        {
            if (num[i] == enu) return i;
        }
        return 0;
    }
}