﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Structure;

public class ProtoLoaderExporter : LoaderExporter
{
    private FileWriter m_loaderWriter = new FileWriter();

    private const string m_outputName = "StaticDataManager_Proto";
    private const string m_initLoaderFormat = "m_loaderMap.Add(\"{0}\", new ProtoDataLoader<{1}{0}{2}, {1}{0}{2}List>(m_{0}List, {1}{0}{2}List.Parser.ParseFrom, (fromList, toList) => {{ toList.Clear(); toList.AddRange(fromList.List); }}));";
    private const string m_annotationFormat = "//{0}";
    private const string m_listDefineFormat = "private List<{1}{0}{2}> m_{0}List = new List<{1}{0}{2}>();";
    private const string m_getItemFuncFormat = "public {1}{0}{2} Get{1}{0}{2}(int id) {{ foreach (var info in m_{0}List) if (info.Id == id) return info; return null; }}";
    private const string m_getEnumeratorFuncFormat = "public IEnumerator<{1}{0}{2}> Get{1}{0}{2}Enumerator() {{ return m_{0}List.GetEnumerator(); }}";

    protected override void OnExport()
    {
        m_loaderWriter.AppendLine("using System.Collections.Generic;");
        m_loaderWriter.AppendLine(string.Format("using {0};", m_context.namespaceStr));
        m_loaderWriter.AppendLine("");
        m_loaderWriter.AppendLine("public partial class StaticDataManager");
        m_loaderWriter.AppendLine("{");
        m_loaderWriter.StartTab();
        {
            m_loaderWriter.AppendLine("partial void OnInit()");
            m_loaderWriter.AppendLine("{");
            m_loaderWriter.StartTab();
            {
                foreach (var kv in DataObjectManager.Instance.structureMap)
                {
                    m_loaderWriter.AppendLine(string.Format(m_initLoaderFormat, kv.Key.name, m_context.prefixStr, m_context.postfixStr));
                }
            }
            m_loaderWriter.EndTab();
            m_loaderWriter.AppendLine("}");
            foreach (var kv in DataObjectManager.Instance.structureMap)
            {
                string structureName = kv.Key.name;
                m_loaderWriter.AppendLine("");
                m_loaderWriter.AppendLine(string.Format(m_annotationFormat, structureName));
                m_loaderWriter.AppendLine(string.Format(m_listDefineFormat, structureName, m_context.prefixStr, m_context.postfixStr));
                m_loaderWriter.AppendLine(string.Format(m_getItemFuncFormat, structureName, m_context.prefixStr, m_context.postfixStr));
                m_loaderWriter.AppendLine(string.Format(m_getEnumeratorFuncFormat, structureName, m_context.prefixStr, m_context.postfixStr));
            }
        }
        m_loaderWriter.EndTab();
        m_loaderWriter.AppendLine("}");

        string outputPath = FolderManager.Instance.GetSubDirPath(m_context.name + "/" + m_context.exportMode.modeType.ToString(), EFolderType.Code) + "\\" + m_outputName + ".cs";
        m_loaderWriter.WriteFile(outputPath);
        if (!string.IsNullOrEmpty(m_context.structureExportPath) && Directory.Exists(m_context.structureExportPath))
        {
            File.Copy(outputPath, m_context.structureExportPath + m_outputName + ".cs", true);
        }
    }
}
