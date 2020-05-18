using System;
using System.Collections.Generic;
using System.Text;

public static class Debug
{
    private static List<string> m_logStrList = new List<string>();

    public static void LogError(string logStr, params object[] prms)
    {
        LogError(string.Format(logStr, prms));
    }

    public static void LogError(string logStr)
    {
        throw new Exception(logStr);
    }

    public static void LogWarning(string logStr, params object[] prms)
    {
        LogWarning(string.Format(logStr, prms));
    }

    public static void LogWarning(string logStr)
    {
        m_logStrList.Add(logStr);
    }

    public static void LogOut(Action<string> actionOnLogOut)
    {
        if (m_logStrList.Count == 0)
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < m_logStrList.Count; ++i)
        {
            if (i != 0)
            {
                sb.Append("\n");
            }
            sb.Append(m_logStrList[i].Trim());
        }
        if (actionOnLogOut != null)
        {
            actionOnLogOut(sb.ToString());
        }
    }

    public static void Clear()
    {
        m_logStrList.Clear();
    }
}
