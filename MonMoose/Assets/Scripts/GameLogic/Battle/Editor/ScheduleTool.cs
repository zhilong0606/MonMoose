using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using MonMoose.Battle;
using UnityEditor;

namespace MonMoose.GameLogic.Battle
{
    public static class ScheduleTool
    {
        private const string loaderFolderPath = "Assets\\Scripts\\GameLogic\\Schedule\\Clips\\Loaders\\";

        private static readonly Dictionary<Type, Action<StringBuilder, string>> typeLoader = new Dictionary<Type, Action<StringBuilder, string>>()
        {
            {typeof(int), IntLoader},
            {typeof(string), StringLoader}
        };

        [MenuItem("Tools/Schedule Tools/Generate ScheduleClip Reader")]
        public static void GenerateMotionReader()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ScheduleClip));
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; ++i)
            {
                Type type = types[i];
                if (type.IsClass)
                {
                    object[] motionAttributes = type.GetCustomAttributes(typeof(ScheduleClipAttribute), true);
                    if (motionAttributes.Length > 0)
                    {
                        GenerateMotionReader(type);
                    }
                }
            }
        }

        private static void GenerateMotionReader(Type type)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\" + loaderFolderPath + type.Name + "Loader.cs";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (!NeedGenerate(type))
            {
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("using System.Xml;\r\n");
            stringBuilder.Append("\r\n");
            stringBuilder.Append("public partial class ").Append(type.Name).Append("\r\n");
            stringBuilder.Append("{\r\n");
            stringBuilder.Append("\tpublic override void Load(XmlElement element)\r\n");
            stringBuilder.Append("\t{\r\n");
            stringBuilder.Append("\t\tbase.Load(element);\r\n");
            stringBuilder.Append("\t\tfor (int i = 0; i < element.ChildNodes.Count; ++i)\r\n");
            stringBuilder.Append("\t\t{\r\n");
            bool isFirstIf = true;
            FieldInfo[] fieldInfos = type.GetFields();
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                object[] memberAttributes = fieldInfo.GetCustomAttributes(typeof(ScheduleClipMemberAttribute), true);
                if (memberAttributes.Length > 0)
                {
                    ScheduleClipMemberAttribute memberAttribute = (ScheduleClipMemberAttribute)memberAttributes[0];
                    stringBuilder.Append("\t\t\t");
                    if (!isFirstIf)
                    {
                        stringBuilder.Append("else ");
                    }
                    isFirstIf = false;
                    stringBuilder.Append("if (element.ChildNodes[i].Name == \"").Append(memberAttribute.name).Append("\")\r\n");
                    stringBuilder.Append("\t\t\t{\r\n");
                    stringBuilder.Append("\t\t\t\t").Append(fieldInfo.Name).Append(" = ");
                    typeLoader[fieldInfo.FieldType](stringBuilder, "element.ChildNodes[i].Attributes[\"value\"].Value");
                    stringBuilder.Append(";\r\n");
                    stringBuilder.Append("\t\t\t}\r\n");
                }
            }
            stringBuilder.Append("\t\t}\r\n");
            stringBuilder.Append("\t}\r\n");
            stringBuilder.Append("}\r\n");

            FileStream fs = new FileStream(filePath, FileMode.Create);
            byte[] buffers = Encoding.Default.GetBytes(stringBuilder.ToString());
            fs.Write(buffers, 0, buffers.Length);
            fs.Flush();
            fs.Close();
        }

        private static bool NeedGenerate(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields();
            bool needGenerate = false;
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                object[] memberAttributes = fieldInfo.GetCustomAttributes(typeof(ScheduleClipMemberAttribute), true);
                if (memberAttributes.Length > 0)
                {
                    needGenerate = true;
                }
            }
            return needGenerate;
        }

        private static void IntLoader(StringBuilder builder, string valueStr)
        {
            builder.Append("int.Parse(").Append(valueStr).Append(")");
        }

        private static void StringLoader(StringBuilder builder, string valueStr)
        {
            builder.Append(valueStr);
        }
    }
}
