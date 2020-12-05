using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MonMoose.Core;
using UnityEngine;
using UnityEditor;

namespace MonMoose.Battle
{
    public class FrameCommandAutoGenTools : Editor
    {
        private static string configPath = "Assets/Scripts/Battle/Module/FrameSync/Command/Editor/FrameCommand.xml";
        private static string codePathFormat = "Assets/Scripts/Battle/Module/FrameSync/Command/AutoGen/{0}Command_AutoGen.cs";
        private static string enumPath = "Assets/Scripts/Battle/Module/FrameSync/Command/AutoGen/EFrameCommandType_AutoGen.cs";

        [MenuItem("Tools/FrameSync/Generate Command Code")]
        public static void GenerateCommandCode()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            XmlNode rootNode = doc.SelectSingleNode("Root");
            List<string> classNameList = new List<string>();
            foreach (XmlNode classNode in rootNode.ChildNodes)
            {
                string className = classNode.Name;
                classNameList.Add(className);
                List<string> typeNameList = new List<string>();
                List<string> fieldNameList = new List<string>();
                foreach (XmlNode fieldNode in classNode.ChildNodes)
                {
                    string typeName = fieldNode.Name;
                    string fieldName = fieldNode.InnerText;
                    typeNameList.Add(typeName);
                    fieldNameList.Add(fieldName);
                }
                WriteCode(className, typeNameList, fieldNameList);
            }
            WriteEnum(classNameList);
        }

        private static void WriteCode(string className, List<string> typeNameList, List<string> fieldNameList)
        {
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public partial class {0}Command : FrameCommand", className);
                writer.StartBlock();
                {
                    for (int i = 0; i < typeNameList.Count; ++i)
                    {
                        writer.AppendLine("public {0} {1};", typeNameList[i], ChangeToFieldName(fieldNameList[i]));
                    }
                    writer.AppendEmptyLine();
                    writer.AppendLine("public override EFrameCommandType commandType");
                    writer.StartBlock();
                    {
                        writer.AppendLine("get {{ return EFrameCommandType.{0}; }}", className);
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override byte GetBitFlagCount()");
                    writer.StartBlock();
                    {
                        writer.AppendLine("return (int)ESerializeIndex.Max;");
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override bool CheckValid(int index)");
                    writer.StartBlock();
                    {
                        writer.AppendLine("switch ((ESerializeIndex)index)");
                        writer.StartBlock();
                        {
                            for (int i = 0; i < typeNameList.Count; ++i)
                            {
                                writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(fieldNameList[i]));
                                writer.StartTab();
                                {
                                    writer.AppendLine("return {0} != default({1});", ChangeToFieldName(fieldNameList[i]), typeNameList[i]);
                                }
                                writer.EndTab();
                            }
                        }
                        writer.EndBlock();
                        writer.AppendLine("return false;");
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override int GetSizeOf(int index)");
                    writer.StartBlock();
                    {
                        writer.AppendLine("switch ((ESerializeIndex)index)");
                        writer.StartBlock();
                        {
                            for (int i = 0; i < typeNameList.Count; ++i)
                            {
                                writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(fieldNameList[i]));
                                writer.StartTab();
                                {
                                    writer.AppendLine("return sizeof({0});", typeNameList[i]);
                                }
                                writer.EndTab();
                            }
                        }
                        writer.EndBlock();
                        writer.AppendLine("return 0;");
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override void SerializeField(byte[] buffer, ref int offset, int index)");
                    writer.StartBlock();
                    {
                        writer.AppendLine("switch ((ESerializeIndex)index)");
                        writer.StartBlock();
                        {
                            for (int i = 0; i < typeNameList.Count; ++i)
                            {
                                writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(fieldNameList[i]));
                                writer.StartTab();
                                {
                                    writer.AppendLine("ByteBufferUtility.Write{0}(buffer, ref offset, {1});", ChangeFirstToUpper(typeNameList[i]), ChangeToFieldName(fieldNameList[i]));
                                    writer.AppendLine("break;");
                                }
                                writer.EndTab();
                            }
                        }
                        writer.EndBlock();
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override void DeserializeField(byte[] buffer, ref int offset, int index)");
                    writer.StartBlock();
                    {
                        writer.AppendLine("switch ((ESerializeIndex)index)");
                        writer.StartBlock();
                        {
                            for (int i = 0; i < typeNameList.Count; ++i)
                            {
                                writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(fieldNameList[i]));
                                writer.StartTab();
                                {
                                    writer.AppendLine("{1} = ByteBufferUtility.Read{0}(buffer, ref offset);", ChangeFirstToUpper(typeNameList[i]), ChangeToFieldName(fieldNameList[i]));
                                    writer.AppendLine("break;");
                                }
                                writer.EndTab();
                            }
                        }
                        writer.EndBlock();
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("private enum ESerializeIndex");
                    writer.StartBlock();
                    {
                        for (int i = 0; i < fieldNameList.Count; ++i)
                        {
                            writer.AppendLine("{0},", ChangeFirstToUpper(fieldNameList[i]));
                        }
                        writer.AppendEmptyLine();
                        writer.AppendLine("Max");
                    }
                    writer.EndBlock();
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + string.Format(codePathFormat, className);
            writer.WriteFile(path);
        }

        private static void WriteEnum(List<string> classNameList)
        {
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public enum EFrameCommandType");
                writer.StartBlock();
                {
                    for (int i = 0; i < classNameList.Count; ++i)
                    {
                        writer.AppendLine("{0},", classNameList[i]);
                    }
                    writer.AppendEmptyLine();
                    writer.AppendLine("Max");
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + string.Format(enumPath);
            writer.WriteFile(path);
        }

        private static string ChangeFirstToLower(string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        private static string ChangeFirstToUpper(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        private static string ChangeToFieldName(string str)
        {
            return ChangeFirstToLower(str);
        }
    }
}
