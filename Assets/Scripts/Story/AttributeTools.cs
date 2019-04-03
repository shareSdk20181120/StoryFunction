using System;

namespace Tool
{
    [AttributeUsage(AttributeTargets.Field)]//指定 自定义特性的作用范围  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class EnumAliasAttribute : Attribute//自定义一个特性类，该类必须包含至少 一个公共的构造函数，可以在类里面定义字段、属性。但是不能定义方法、时间。
    {
        private string name = string.Empty;
        public EnumAliasAttribute(string alias)
        {
            name = alias;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}