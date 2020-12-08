using System;
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
        private static string m_configPath = "Assets/Scripts/Battle/Module/FrameSync/Command/Editor/FrameCommand.xml";
        private static string m_autoGenFolder = "Assets/Scripts/Battle/Module/FrameSync/AutoGen";
        private static string m_codePathFormat = m_autoGenFolder + "/Command/{0}Command_AutoGen.cs";
        private static string m_codePathFormat2 = "Assets/Scripts/Battle/Module/FrameSync/Command/{0}Command.cs";
        private static string m_enumPath = m_autoGenFolder + "/EFrameCommandType_AutoGen.cs";
        private static string m_factoryPath = m_autoGenFolder + "/FrameCommandFactory_AutoGen.cs";
        private static string m_senderPath = m_autoGenFolder + "/FrameSyncSender_AutoGen.cs";

        [MenuItem("Tools/FrameSync/Generate Command Code")]
        public static void GenerateCommandCode()
        {
            ClearAutoGenFolder();
            XmlDocument doc = new XmlDocument();
            doc.Load(m_configPath);
            XmlNode rootNode = doc.SelectSingleNode("Root");
            List<ClassInfo> classList = new List<ClassInfo>();
            foreach (XmlNode classNode in rootNode.ChildNodes)
            {
                string className = classNode.Name;
                ClassInfo classInfo = new ClassInfo();
                classInfo.name = className;
                classInfo.memberList = new List<MemberInfo>();
                classList.Add(classInfo);
                foreach (XmlNode fieldNode in classNode.ChildNodes)
                {
                    string typeName = fieldNode.Name;
                    string fieldName = fieldNode.InnerText;
                    MemberInfo memberInfo = new MemberInfo();
                    memberInfo.typeName = typeName;
                    memberInfo.fieldName = fieldName;
                    classInfo.memberList.Add(memberInfo);
                }
                WriteCode(classInfo);
                WriteCode2(classInfo);
            }
            WriteEnum(classList);
            WriteFactory(classList);
            WriteSender(classList);
        }

        private static void ClearAutoGenFolder()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(m_autoGenFolder);
            if (!dirInfo.Exists)
            {
                return;
            }
            List<DirectoryInfo> dirList = new List<DirectoryInfo>() {dirInfo};
            while (dirList.Count > 0)
            {
                dirInfo = dirList[0];
                dirList.RemoveAt(0);
                List<string> folderMetaFileList = new List<string>();
                foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
                {
                    string metaName = subDirInfo.FullName + ".meta";
                    folderMetaFileList.Add(metaName);
                    dirList.Add(subDirInfo);
                }
                List<string> deleteFileList = new List<string>();
                foreach (FileInfo fileInfo in dirInfo.GetFiles())
                {
                    if (folderMetaFileList.Find(s => s == fileInfo.FullName) == null)
                    {
                        deleteFileList.Add(fileInfo.FullName);
                    }
                }
                foreach (string filePath in deleteFileList)
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError(e.Message);
                    }
                }
            }
        }

        private static void WriteCode(ClassInfo classInfo)
        {
            string className = classInfo.name;
            List<MemberInfo> memberList = classInfo.memberList;
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public partial class {0}Command", className);
                writer.StartBlock();
                {
                    if (memberList.Count > 0)
                    {
                        for (int i = 0; i < memberList.Count; ++i)
                        {
                            writer.AppendLine("public {0} {1};", memberList[i].typeName, ChangeToFieldName(memberList[i].fieldName));
                        }
                        writer.AppendEmptyLine();
                    }
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
                        if (memberList.Count > 0)
                        {
                            writer.AppendLine("return (int)ESerializeIndex.Max;");
                        }
                        else
                        {
                            writer.AppendLine("return 0;");
                        }
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override bool CheckValid(int index)");
                    writer.StartBlock();
                    {
                        if (memberList.Count > 0)
                        {
                            writer.AppendLine("switch ((ESerializeIndex)index)");
                            writer.StartBlock();
                            {
                                for (int i = 0; i < memberList.Count; ++i)
                                {
                                    writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(memberList[i].fieldName));
                                    writer.StartTab();
                                    {
                                        writer.AppendLine("return {0} != default({1});", ChangeToFieldName(memberList[i].fieldName), memberList[i].typeName);
                                    }
                                    writer.EndTab();
                                }
                            }
                            writer.EndBlock();
                        }
                        writer.AppendLine("return false;");
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override int GetSizeOf(int index)");
                    writer.StartBlock();
                    {
                        if (memberList.Count > 0)
                        {
                            writer.AppendLine("switch ((ESerializeIndex)index)");
                            writer.StartBlock();
                            {
                                for (int i = 0; i < memberList.Count; ++i)
                                {
                                    writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(memberList[i].fieldName));
                                    writer.StartTab();
                                    {
                                        writer.AppendLine("return sizeof({0});", memberList[i].typeName);
                                    }
                                    writer.EndTab();
                                }
                            }
                            writer.EndBlock();
                        }
                        writer.AppendLine("return 0;");
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override void SerializeField(byte[] buffer, ref int offset, int index)");
                    writer.StartBlock();
                    {
                        if (memberList.Count > 0)
                        {
                            writer.AppendLine("switch ((ESerializeIndex)index)");
                            writer.StartBlock();
                            {
                                for (int i = 0; i < memberList.Count; ++i)
                                {
                                    writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(memberList[i].fieldName));
                                    writer.StartTab();
                                    {
                                        writer.AppendLine("ByteBufferUtility.Write{0}(buffer, ref offset, {1});", ChangeFirstToUpper(memberList[i].typeName), ChangeToFieldName(memberList[i].fieldName));
                                        writer.AppendLine("break;");
                                    }
                                    writer.EndTab();
                                }
                            }
                            writer.EndBlock();
                        }
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    writer.AppendLine("protected override void DeserializeField(byte[] buffer, ref int offset, int index)");
                    writer.StartBlock();
                    {
                        if (memberList.Count > 0)
                        {
                            writer.AppendLine("switch ((ESerializeIndex)index)");
                            writer.StartBlock();
                            {
                                for (int i = 0; i < memberList.Count; ++i)
                                {
                                    writer.AppendLine("case ESerializeIndex.{0}:", ChangeFirstToUpper(memberList[i].fieldName));
                                    writer.StartTab();
                                    {
                                        writer.AppendLine("{1} = ByteBufferUtility.Read{0}(buffer, ref offset);", ChangeFirstToUpper(memberList[i].typeName), ChangeToFieldName(memberList[i].fieldName));
                                        writer.AppendLine("break;");
                                    }
                                    writer.EndTab();
                                }
                            }
                            writer.EndBlock();
                        }
                    }
                    writer.EndBlock();
                    writer.AppendEmptyLine();
                    if (memberList.Count > 0)
                    {
                        writer.AppendLine("private enum ESerializeIndex");
                        writer.StartBlock();
                        {
                            for (int i = 0; i < memberList.Count; ++i)
                            {
                                writer.AppendLine("{0},", ChangeFirstToUpper(memberList[i].fieldName));
                            }

                            writer.AppendEmptyLine();
                            writer.AppendLine("Max");
                        }
                        writer.EndBlock();
                    }
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + string.Format(m_codePathFormat, className);
            writer.WriteFile(path);
        }

        private static void WriteCode2(ClassInfo classInfo)
        {
            string className = classInfo.name;
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + string.Format(m_codePathFormat2, className);
            if (File.Exists(path))
            {
                return;
            }
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public partial class {0}Command : FrameCommand", className);
                writer.StartBlock();
                {
                    writer.AppendLine("public override void Execute(int playerId)");
                    writer.StartBlock();
                    {
                        writer.AppendLine("throw new System.NotImplementedException();");
                    }
                    writer.EndBlock();
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            writer.WriteFile(path);
        }

        private static void WriteEnum(List<ClassInfo> classList)
        {
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public enum EFrameCommandType");
                writer.StartBlock();
                {
                    for (int i = 0; i < classList.Count; ++i)
                    {
                        writer.AppendLine("{0},", classList[i].name);
                    }
                    writer.AppendEmptyLine();
                    writer.AppendLine("Max");
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + m_enumPath;
            writer.WriteFile(path);
        }

        private static void WriteFactory(List<ClassInfo> classList)
        {
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public static class FrameCommandFactory");
                writer.StartBlock();
                {
                    writer.AppendLine("public static FrameCommand CreateCommand(BattleBase battleInstance, EFrameCommandType cmdType)");
                    writer.StartBlock();
                    {
                        writer.AppendLine("switch (cmdType)");
                        writer.StartBlock();
                        {
                            for (int i = 0; i < classList.Count; ++i)
                            {
                                string className = classList[i].name;
                                writer.AppendLine("case EFrameCommandType.{0}:", className);
                                writer.StartTab();
                                {
                                    writer.AppendLine("return battleInstance.FetchPoolObj<{0}Command>(typeof(FrameCommandFactory));", className);
                                }
                                writer.EndTab();
                            }
                        }
                        writer.EndBlock();
                        writer.AppendLine("return null;");
                    }
                    writer.EndBlock();
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + m_factoryPath;
            writer.WriteFile(path);
        }

        private static void WriteSender(List<ClassInfo> classList)
        {
            CodeWriter writer = new CodeWriter();
            writer.AppendLine("namespace MonMoose.Battle");
            writer.StartBlock();
            {
                writer.AppendLine("public partial class FrameSyncSender");
                writer.StartBlock();
                {
                    foreach (ClassInfo classInfo in classList)
                    {
                        string className = classInfo.name;
                        List<MemberInfo> memberList = classInfo.memberList;

                        writer.StartLine("public void Send{0}(", className);
                        for (int i = 0; i < memberList.Count; ++i)
                        {
                            if (i != 0)
                            {
                                writer.Append(", ");
                            }
                            writer.Append("{0} {1}", ChangeFirstToLower(memberList[i].typeName), ChangeFirstToLower(memberList[i].fieldName));
                        }
                        writer.Append(")");
                        writer.EndLine();
                        
                        writer.StartBlock();
                        {
                            writer.AppendLine("{0}Command cmd = m_battleInstance.FetchPoolObj<{0}Command>(this);", className);
                            foreach (MemberInfo memberInfo in memberList)
                            {
                                writer.AppendLine("cmd.{0} = {1};", ChangeToFieldName(memberInfo.fieldName), ChangeFirstToLower(memberInfo.fieldName));
                            }
                            writer.AppendLine("SendCommand(cmd);");
                        }
                        writer.EndBlock();
                    }
                }
                writer.EndBlock();
            }
            writer.EndBlock();
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length) + m_senderPath;
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

        private struct ClassInfo
        {
            public string name;
            public List<MemberInfo> memberList;
        }

        private struct MemberInfo
        {
            public string typeName;
            public string fieldName;
        }
    }
}
