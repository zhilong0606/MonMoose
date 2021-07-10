using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MonMoose.Battle;
using MonMoose.Core;
using UnityEditor;
using UnityEngine;

public static class BattleModuleGenerator
{
    [MenuItem("Tools/Battle/Generate Battle Shortcut Code")]
    public static void CreatBattleShortcutCode()
    {
        Type battleType = typeof(BattleBase);
        string codePath = AssetPathUtility.GetClassAssetPath(battleType);
        string codeFolderPath = AssetPathUtility.GetFileFolderPath(codePath);
        string newFolderPath = codeFolderPath + "/AutoGen";
        if (!Directory.Exists(newFolderPath))
        {
            Directory.CreateDirectory(newFolderPath);
        }
        //var sss = battleType.GetFields();
        //var ddd = battleType.GetRuntimeFields();
        foreach (FieldInfo fi in battleType.GetRuntimeFields())
        {
            if (typeof(MonMoose.Battle.Module).IsAssignableFrom(fi.FieldType))
            {
                string newCodePath = string.Format("{0}/{1}_{2}.cs", newFolderPath, battleType.Name, fi.FieldType.Name);
                WriteCode(newCodePath, battleType, fi.FieldType, fi.Name);
            }
        }
    }

    private static void WriteCode(string outputFilePath, Type partialType, Type moduleType, string moduleName)
    {
        CodeWriter writer = new CodeWriter();
        string moduleCodePath = AssetPathUtility.GetClassAssetPath(moduleType);
        string[] lines = File.ReadAllLines(moduleCodePath);
        foreach (string lineStr in lines)
        {
            string str = lineStr.Trim();
            if (str.StartsWith("using"))
            {
                writer.AppendLine(str);
            }
        }
        writer.AppendEmptyLine();
        writer.AppendLine("namespace {0}", partialType.Namespace);
        writer.StartBlock();
        {
            writer.AppendLine("public partial class {0}", partialType.Name);
            writer.StartBlock();
            {
                bool needBlankLine = false;
                foreach (MethodInfo mi in moduleType.GetMethods())
                {
                    if (mi.IsPublic && !mi.IsVirtual && !mi.IsStatic && !mi.IsAbstract && mi.DeclaringType == moduleType)
                    {
                        if (mi.Name.StartsWith("get_") || mi.Name.StartsWith("set_"))
                        {
                            continue;
                        }
                        if (needBlankLine)
                        {
                            writer.AppendEmptyLine();
                        }
                        writer.AppendLine("public {0}", GetMethodDefineStr(mi));
                        writer.StartBlock();
                        {
                            writer.AppendLine("if ({0} != null)", moduleName);
                            writer.StartBlock();
                            {
                                if (mi.ReturnType == typeof(void))
                                {
                                    writer.AppendLine("{0}.{1}({2});", moduleName, mi.Name, GetMethodInvokeStr(mi));
                                }
                                else
                                {
                                    writer.AppendLine("return {0}.{1}({2});", moduleName, mi.Name, GetMethodInvokeStr(mi));
                                }
                            }
                            writer.EndBlock();
                            if (mi.ReturnType != typeof(void))
                            {
                                writer.AppendLine("return default;");
                            }
                        }
                        writer.EndBlock();
                        needBlankLine = true;
                    }
                }
            }
            writer.EndBlock();
        }
        writer.EndBlock();
        writer.WriteFile(outputFilePath);
    }

    private static string GetMethodDefineStr(MethodInfo methodInfo)
    {
        string name = methodInfo.Name;
        string paramStr = string.Empty;
        foreach (var prm in methodInfo.GetParameters())
        {
            if (!string.IsNullOrEmpty(paramStr))
            {
                paramStr += ", ";
            }
            paramStr += string.Format("{0} {1}", GetTypeName(prm.ParameterType), prm.Name);
        }
        return string.Format("{0} {1}({2})", GetTypeName(methodInfo.ReturnType), name, paramStr);
    }

    private static string GetMethodInvokeStr(MethodInfo methodInfo)
    {
        string paramStr = string.Empty;
        foreach (var prm in methodInfo.GetParameters())
        {
            if (!string.IsNullOrEmpty(paramStr))
            {
                paramStr += ", ";
            }
            paramStr += prm.Name;
        }
        return paramStr;
    }

    private static string GetTypeName(Type type)
    {
        if (type.IsGenericType)
        {
            Type mainType = type.GetGenericTypeDefinition();
            string[] splits = mainType.Name.Split('`');
            if (splits.Length != 2)
            {
                Debug.LogError(string.Format("{0}是无法解析的类型", type.Name));
                return string.Empty;
            }
            string paramsStr = string.Empty;
            foreach (var arg in type.GetGenericArguments())
            {
                if (!string.IsNullOrEmpty(paramsStr))
                {
                    paramsStr += ", ";
                }
                paramsStr += GetTypeName(arg);
            }
            return string.Format("{0}<{1}>", splits[0], paramsStr);
        }
        Dictionary<Type, string> typeNameMap = new Dictionary<Type, string>()
        {
            {typeof(bool), "bool"},
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(float), "float"},
            {typeof(double), "double"},
            {typeof(decimal), "decimal"},
            {typeof(string), "string"},
            {typeof(void), "void"},
        };
        string typeName;
        if (typeNameMap.TryGetValue(type, out typeName))
        {
            return typeName;
        }
        return type.Name;
    }
}
