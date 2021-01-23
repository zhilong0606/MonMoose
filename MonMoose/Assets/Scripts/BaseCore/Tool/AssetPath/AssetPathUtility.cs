using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class AssetPathUtility
{
    private const string m_delimiter = "/";

    public static string GetFileFullNameExceptExtension(string fileFullName)
    {
        if (!string.IsNullOrEmpty(fileFullName))
        {
            fileFullName = GetNormalizePath(fileFullName);
            int lastDotIndex = fileFullName.LastIndexOf(".", StringComparison.Ordinal);
            int lastDelimiterIndex = fileFullName.LastIndexOf(m_delimiter, StringComparison.Ordinal);
            if (lastDotIndex >= 0)
            {
                if (lastDotIndex > lastDelimiterIndex)
                {
                    CutStr(fileFullName, lastDotIndex, true);
                }
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

    public static string CutStrBy(string srcStr, string cutterStr, bool isForward, bool useFront)
    {
        if (!string.IsNullOrEmpty(srcStr))
        {
            int index = isForward ? srcStr.IndexOf(cutterStr, StringComparison.Ordinal) : srcStr.LastIndexOf(cutterStr, StringComparison.Ordinal);
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
            return path.Replace("\\", "/");
        }
        return string.Empty;
    }
}
