using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MonMoose.Core
{
    public class ShortCutGenerator
    {
        private List<Type> m_typeList = new List<Type>();
        private List<ClassNode> m_classNodeList = new List<ClassNode>();
        private List<ClassNode> m_generateNodeList = new List<ClassNode>();
        private Func<Type, string> m_funcOnGetTypePath;
        private Action<string> m_actionOnLog;

        public void Generate(Func<Type, string> funcOnGetTypePath, Action<string> actionOnLog)
        {
            m_funcOnGetTypePath = funcOnGetTypePath;
            m_actionOnLog = actionOnLog;
            CollectAndAnalyze();
            if (CheckDuplicate())
            {
                GenerateInternal();
            }
        }

        private void GenerateInternal()
        {
            foreach (ClassNode classNode in m_generateNodeList)
            {
                GenerateOrGetNode(classNode);
            }
        }

        private ClassNode GenerateOrGetNode(ClassNode classNode)
        {
            string folderPath = AssetPathUtility.GetFileFolderPath(GetTypePath(classNode.type));
            folderPath = folderPath + "/AutoGen";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = folderPath + "/" + classNode.type.Name + "_AutoGen.cs";
            CodeWriter writer = new CodeWriter();
            writer.WriteFile(filePath);
            return null;
        }

        private ClassNode AnalyzeClassNode(Type classType)
        {
            for (int i = 0; i < m_classNodeList.Count; ++i)
            {
                if (m_classNodeList[i].type == classType)
                {
                    return m_classNodeList[i];
                }
            }
            ClassNode classNode = new ClassNode();
            classNode.type = classType;
            m_classNodeList.Add(classNode);
            foreach (var mi in classType.GetMethods())
            {
                bool isPublic;
                if (CheckShortCut(mi, out isPublic))
                {
                    classNode.methodNodeList.Add(AnalyzeMethodNode(mi, isPublic));
                }
            }
            foreach (var fi in classType.GetFields())
            {
                bool isKeyField;
                if (CheckShortCut(fi, out isKeyField))
                {
                    classNode.fieldNodeList.Add(AnalyzeFieldNode(fi, isKeyField));
                }
            }
            return classNode;
        }

        private FieldNode AnalyzeFieldNode(FieldInfo fieldInfo, bool isKeyField)
        {
            FieldNode fieldNode = new FieldNode();
            fieldNode.fieldInfo = fieldInfo;
            fieldNode.isKeyField = isKeyField;
            fieldNode.classNode = AnalyzeClassNode(fieldInfo.FieldType);
            return fieldNode;
        }

        private MethodNode AnalyzeMethodNode(MethodInfo methodInfo, bool isPublic)
        {
            MethodNode methodNode = new MethodNode();
            methodNode.methodInfo = methodInfo;
            methodNode.isPublic = isPublic;
            return methodNode;
        }

        private void CollectAndAnalyze()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var cls in assembly.GetTypes())
                {
                    if (CheckShortCut(cls))
                    {
                        m_generateNodeList.Add(AnalyzeClassNode(cls));
                    }
                }
            }
        }

        private bool CheckDuplicate()
        {
            for (int i = 0; i < m_classNodeList.Count; ++i)
            {
                List<FieldNode> fieldNodeList = new List<FieldNode>();
                List<ClassNode> classNodeList = new List<ClassNode>(){m_classNodeList[i]};
                while (classNodeList.Count > 0)
                {
                    ClassNode classNode = classNodeList[0];
                    classNodeList.RemoveAt(0);
                    foreach (FieldNode fieldNode in classNode.fieldNodeList)
                    {
                        if (fieldNodeList.Contains(fieldNode))
                        {
                            LogOut(string.Format("{0}.{1}", classNode.type.Name, fieldNode.fieldInfo.Name));
                            return false;
                        }
                        fieldNodeList.Add(fieldNode);
                        classNodeList.Add(fieldNode.classNode);
                    }
                }
            }
            return true;
        }

        private bool CheckShortCut(Type type)
        {
            return type.GetCustomAttributes(typeof(ShortCutClassAttribute), false).GetValueSafely(0) as ShortCutClassAttribute != null;
        }

        private bool CheckShortCut(MethodInfo mi, out bool isPublic)
        {
            ShortCutMethodAttribute attr = mi.GetCustomAttributes(typeof(ShortCutMethodAttribute), false).GetValueSafely(0) as ShortCutMethodAttribute;
            if (attr != null)
            {
                isPublic = attr.isPublic;
                return true;
            }
            isPublic = false;
            return false;
        }

        private bool CheckShortCut(FieldInfo fi, out bool isKeyField)
        {
            ShortCutFieldAttribute attr = fi.GetCustomAttributes(typeof(ShortCutFieldAttribute), false).GetValueSafely(0) as ShortCutFieldAttribute;
            if (attr != null)
            {
                isKeyField = attr.isKeyField;
                return true;
            }
            isKeyField = false;
            return false;
        }

        private string GetTypePath(Type type)
        {
            if (m_funcOnGetTypePath != null)
            {
                return m_funcOnGetTypePath(type);
            }
            return string.Empty;
        }

        private void LogOut(string logStr)
        {
            if (m_actionOnLog != null)
            {
                m_actionOnLog(logStr);
            }
        }

        private class ClassGenNode
        {
            public ClassNode node;
            public List<MethodGenNode> methodList = new List<MethodGenNode>();
            public List<FieldGenNode> fieldList = new List<FieldGenNode>();
        }

        private class MethodGenNode
        {
            public MethodNode node;
        }

        private class FieldGenNode
        {
            public FieldNode node;
            public ClassGenNode classNode;
            public List<FieldGenNode> fieldList = new List<FieldGenNode>();
            public List<MethodGenNode> methodList = new List<MethodGenNode>();
        }

        private class ClassNode
        {
            public Type type;
            public List<FieldNode> fieldNodeList = new List<FieldNode>();
            public List<MethodNode> methodNodeList = new List<MethodNode>();
        }

        private class MethodNode
        {
            public MethodInfo methodInfo;
            public bool isPublic;
        }

        private class FieldNode
        {
            public bool isKeyField;
            public FieldInfo fieldInfo;
            public ClassNode classNode;
        }
    }
}
