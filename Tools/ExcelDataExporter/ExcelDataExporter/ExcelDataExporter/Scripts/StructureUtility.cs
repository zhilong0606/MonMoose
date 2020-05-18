using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Structure
{
    public static class StructureUtility
    {
        //private static Regex m_complexRegex = new Regex(@"^\s*(\w+)\s*\{\s*(.*?)\s*\}\s*$");
        //private static Regex m_singleRegex = new Regex(@"^\s*(\w+)\s*$");
        //private static Regex m_listRegex = new Regex(@"^\s*List\s*<\s*(.+)\s*>\s*$");
        //private static Regex m_dictionaryRegex = new Regex(@"^\s*Dictionary\s*<\s*(.+)\s*>\s*$");

        //public static BaseStructureInfo AnalyzeStructure(string str)
        //{
        //    Match listMatch = m_listRegex.Match(str);
        //    if (listMatch.Success)
        //    {
        //        return AnalyzeList(listMatch.Groups[1].Value);
        //    }
        //    Match dictionaryMatch = m_dictionaryRegex.Match(str);
        //    if (dictionaryMatch.Success)
        //    {
        //        return AnalyzeDictionary(dictionaryMatch.Groups[1].Value);
        //    }
        //    Match singleMatch = m_singleRegex.Match(str);
        //    if (singleMatch.Success)
        //    {
        //        return AnalyzeSingleStructure(singleMatch.Groups[1].Value);
        //    }
        //    Match complexMatch = m_complexRegex.Match(str);
        //    if (complexMatch.Success)
        //    {
        //        return AnalyzeComplexStructure(complexMatch.Groups[1].Value, complexMatch.Groups[2].Value);
        //    }
        //    throw new Exception();
        //}

        //private static BaseStructureInfo AnalyzeSingleStructure(string structureName)
        //{
        //    ClassStructureInfo structureInfo = new ClassStructureInfo(structureName);
        //    return GetOrAddStructureInfo(structureInfo);
        //}

        //private static BaseStructureInfo AnalyzeComplexStructure(string structureName, string memberStr)
        //{
        //    ClassStructureInfo structureInfo = new ClassStructureInfo(structureName);
        //    List<ClassMemberInfo> memberList = AnalyzeMembers(memberStr);
        //    for (int i = 0; i < memberList.Count; ++i)
        //    {
        //        structureInfo.AddMember(memberList[i]);
        //    }
        //    return GetOrAddStructureInfo(structureInfo);
        //}

        //private static BaseStructureInfo AnalyzeList(string str)
        //{
        //    BaseStructureInfo innerStructureInfo = AnalyzeStructure(str);
        //    if (innerStructureInfo != null)
        //    {
        //        ListStructureInfo listStructureInfo = new ListStructureInfo(innerStructureInfo);
        //        return GetOrAddStructureInfo(listStructureInfo);
        //    }
        //    return null;
        //}

        //private static BaseStructureInfo AnalyzeDictionary(string str)
        //{
        //    string[] splits = str.Split(new[] {','}, new[] {'<', '{'}, new[] {'>', '}'}, false, true);
        //    if (splits.Length == 2)
        //    {
        //        BaseStructureInfo keyStructureInfo = AnalyzeStructure(splits[0]);
        //        BaseStructureInfo valueStructureInfo = AnalyzeStructure(splits[1]);
        //        if (keyStructureInfo != null || valueStructureInfo != null)
        //        {
        //            DictionaryStructureInfo dictionaryStructureInfo = new DictionaryStructureInfo(keyStructureInfo, valueStructureInfo);
        //            return GetOrAddStructureInfo(dictionaryStructureInfo);
        //        }
        //    }
        //    return null;
        //}

        //private static BaseStructureInfo GetOrAddStructureInfo(BaseStructureInfo info)
        //{
        //    BaseStructureInfo structureInfo = StructureManager.Instance.GetStructureInfo(info.name);
        //    if (structureInfo == null)
        //    {
        //        StructureManager.Instance.AddStructureInfo(info);
        //        return info;
        //    }
        //    return structureInfo;
        //}

        //private static List<ClassMemberInfo> AnalyzeMembers(string str)
        //{
        //    List<ClassMemberInfo> memberList = new List<ClassMemberInfo>();
        //    string[] splits = str.Split(new[] { ',' }, new[] { '<', '{' }, new[] { '>', '}' }, true, true);
        //    for (int i = 0; i < splits.Length; ++i)
        //    {
        //        string split = splits[i];
        //        ClassMemberInfo memberInfo = AnalyzeMember(split);
        //        if (memberInfo != null)
        //        {
        //            memberList.Add(memberInfo);
        //        }
        //    }
        //    return memberList;
        //}

        //private static ClassMemberInfo AnalyzeMember(string str)
        //{
        //    string[] splits = str.Split(new[] { ':' }, new[] { '<', '{' }, new[] { '>', '}' }, false, true);
        //    if (splits.Length == 2)
        //    {
        //        string memberName = splits[0];
        //        BaseStructureInfo memberStructureInfo = AnalyzeStructure(splits[1]);
        //        if (memberStructureInfo != null)
        //        {
        //            return new ClassMemberInfo(memberStructureInfo, memberName);
        //        }
        //    }
        //    Debug.LogError(StaticString.WrongMemberPairFormat, str);
        //    return null;
        //}

        //public static object GetValue(string str, BaseStructureInfo structureInfo, Assembly assembly)
        //{
        //    str = str.Replace(" ", string.Empty);
        //    str = str.Replace('<', '{');
        //    str = str.Replace('[', '{');
        //    str = str.Replace('(', '{');
        //    str = str.Replace('>', '}');
        //    str = str.Replace(']', '}');
        //    str = str.Replace(')', '}');
        //    if (structureInfo.structureType != EStructureType.Enum && structureInfo.structureType != EStructureType.Basic)
        //    {
        //        str = "{" + str + "}";
        //    }
        //    return GetValueInternal(str, structureInfo, assembly);
        //}

        //private static object GetValueInternal(string str, BaseStructureInfo structureInfo, Assembly assembly)
        //{
        //    if (structureInfo.structureType != EStructureType.Enum && structureInfo.structureType != EStructureType.Basic)
        //    {
        //        str = str.Substring(1, str.Length - 2);
        //    }
        //    switch (structureInfo.structureType)
        //    {
        //        case EStructureType.Basic:
        //            return GetBasicValue(str, structureInfo);
        //        case EStructureType.Enum:
        //            return GetEnumValue(str, structureInfo as EnumStructureInfo);
        //        case EStructureType.Class:
        //            return GetClassValue(str, structureInfo as ClassStructureInfo, assembly);
        //        case EStructureType.List:
        //            return GetListValue(str, structureInfo, assembly);
        //        default:
        //            throw new Exception();
        //    }
        //}

        //private static object GetBasicValue(string str, BaseStructureInfo structureInfo)
        //{
        //    switch (structureInfo.name)
        //    {
        //        case "Bool":
        //            return GetBoolValue(str);
        //        case "Int32":
        //            return (int)GetNumberValue(str);
        //        case "Int64":
        //            return GetNumberValue(str);
        //        case "Float":
        //            return (float)GetDecimalValue(str);
        //        case "Double":
        //            return GetDecimalValue(str);
        //        case "String":
        //            return GetStringValue(str);
        //        default:
        //            throw new Exception();
        //    }
        //}

        //private static string GetStringValue(string str)
        //{
        //    if (str == null)
        //    {
        //        str = string.Empty;
        //    }
        //    return str;
        //}

        //private static bool GetBoolValue(string str)
        //{
        //    bool value;
        //    if (bool.TryParse(str, out value))
        //    {
        //        return value;
        //    }
        //    throw new Exception();
        //}

        //private static long GetNumberValue(string str)
        //{
        //    long value;
        //    if (long.TryParse(str, out value))
        //    {
        //        return value;
        //    }
        //    throw new Exception();
        //}

        //private static double GetDecimalValue(string str)
        //{
        //    double value;
        //    if (double.TryParse(str, out value))
        //    {
        //        return value;
        //    }
        //    throw new Exception();
        //}

        //private static object GetEnumValue(string str, EnumStructureInfo structureInfo)
        //{
        //    if (structureInfo != null)
        //    {
        //        EnumMemberInfo memberInfo = structureInfo.GetMember(str);
        //        if (memberInfo != null)
        //        {
        //            return memberInfo.index;
        //        }
        //    }
        //    Debug.LogError("");
        //    return null;
        //}

        //private static object GetClassValue(string str, ClassStructureInfo structureInfo, Assembly assembly)
        //{
        //    if (structureInfo != null)
        //    {
        //        List<string> splitList = GetSplitString(str);
        //        Type type = assembly.GetType(structureInfo.name);
        //        object value = Activator.CreateInstance(type);
        //        for (int i = 0; i < structureInfo.memberList.Count; ++i)
        //        {
        //            BaseMemberInfo memberInfo = structureInfo.memberList[i];
        //            FieldInfo fieldInfo = type.GetField(memberInfo.name.Substring(0, 1).ToLower() + memberInfo.name.Substring(1) + "_", BindingFlags.NonPublic | BindingFlags.Instance);
        //            string fieldStr = splitList[i];
        //            object fieldValue = GetValueInternal(fieldStr, memberInfo.structureInfo, assembly);
        //            fieldInfo.SetValue(value, fieldValue);
        //        }
        //        return value;
        //    }
        //    Debug.LogError("");
        //    return null;
        //}

        //private static object GetListValue(string str, BaseStructureInfo structureInfo, Assembly assembly)
        //{
        //    List<string> splitList = GetSplitString(str);
        //    Type itemType = assembly.GetType(structureInfo.valueInfo.name);
        //    object value = Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
        //    MethodInfo listAddMethodInfo = value.GetType().GetMethod("Add", new[] {itemType}, new[] {new ParameterModifier(1)});
        //    for (int i = 0; i < splitList.Count; ++i)
        //    {
        //        object itemObj = GetValueInternal(splitList[i], structureInfo.valueInfo, assembly);
        //        listAddMethodInfo.Invoke(value, new[] {itemObj});
        //    }
        //    return value;
        //}

        //private static List<string> GetSplitString(string str)
        //{
        //    List<string> splitList = new List<string>();
        //    int startIndex = 0;
        //    int depth = 0;
        //    for (int i = 0; i < str.Length; ++i)
        //    {
        //        char ch = str[i];
        //        if (ch == ',')
        //        {
        //            if (depth == 0)
        //            {
        //                splitList.Add(str.Substring(startIndex, i - startIndex));
        //                startIndex = i + 1;
        //            }
        //        }
        //        else if (ch == '{')
        //        {
        //            depth++;
        //        }
        //        else if (ch == '}')
        //        {
        //            depth--;
        //        }
        //        if (i == str.Length - 1)
        //        {
        //            if (depth == 0)
        //            {
        //                splitList.Add(str.Substring(startIndex, str.Length - startIndex));
        //            }
        //        }
        //    }
        //    return splitList;
        //}
    }
}
