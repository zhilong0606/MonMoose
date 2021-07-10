using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AssetPathUtility
{
    private const string m_delimiter = "/";

    public static string GetFileName(string fileFullName, bool isFullName, bool exceptExtension)
    {
        if (!string.IsNullOrEmpty(fileFullName))
        {
            fileFullName = GetNormalizePath(fileFullName);
            if (isFullName)
            {
                if (exceptExtension)
                {
                    fileFullName = CutStrBy(fileFullName, ".", true, true);
                }
                return fileFullName;
            }
            else
            {
                string folderPath = GetFileFolderPath(fileFullName);
                string fileName = fileFullName.Substring(folderPath.Length + 1);
                if (exceptExtension)
                {
                    fileName = CutStrBy(fileName, ".", true, true);
                }
                return fileName;
            }
        }
        return fileFullName;
    }

    public static string GetFileFolderPath(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            filePath = GetNormalizePath(filePath);
            return CutStrBy(filePath, m_delimiter, false, true);
        }
        return string.Empty;
    }

    public static string Concat(string str1, string str2, string dupStr)
    {
        int dupLength = dupStr.Length;
        return str1 + CutStr(str2, dupLength - 1, false);
    }

    public static string CutStrBy(string srcStr, string cutterStr, bool isForwardFindCutter, bool useFront)
    {
        if (!string.IsNullOrEmpty(srcStr))
        {
            int index = isForwardFindCutter ? srcStr.IndexOf(cutterStr, StringComparison.Ordinal) : srcStr.LastIndexOf(cutterStr, StringComparison.Ordinal);
            return CutStr(srcStr, index, useFront);
        }
        return string.Empty;
    }

    public static string CutStr(string srcStr, int index, bool useFront)
    {
        if (!string.IsNullOrEmpty(srcStr))
        {
            if (index >= 0)
            {
                return useFront ? srcStr.Substring(0, index) : srcStr.Substring(index + 1);
            }
            return srcStr;
        }
        return string.Empty;
    }

    private static string GetNormalizePath(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            return path.Replace("\\", m_delimiter);
        }
        return string.Empty;
    }

    public static string GetClassFullPath(Type type)
    {
        return GetClassPath(type, true);
    }

    public static string GetClassAssetPath(Type type)
    {
        return GetClassPath(type, false);
    }

    public static string GetClassPath(Type type, bool isFullPath)
    {
        string path = AssetDatabase.FindAssets("t:Script")
            .Where(v => Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(v)) == type.Name)
            .Select(id => AssetDatabase.GUIDToAssetPath(id))
            .FirstOrDefault();
        if (isFullPath)
        {
            return Concat(Application.dataPath, path, "Assets");
        }
        return path;
    }
}
