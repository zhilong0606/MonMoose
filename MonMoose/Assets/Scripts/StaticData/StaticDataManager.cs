using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class StaticDataManager
{
    private static Dictionary<string, StaticDataLoader> m_loaderMap = new Dictionary<string, StaticDataLoader>();

    private static StaticDataManager m_instance;

    public static StaticDataManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new StaticDataManager();
                m_instance.OnInit();
            }
            return m_instance;
        }
    }

    public void Load(string folderPath, Func<string, byte[]> actionOnLoad)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        foreach (FileInfo fileInfo in directoryInfo.GetFiles())
        {
            byte[] buffer = actionOnLoad(fileInfo.FullName);
            if (buffer == null)
            {
                continue;
            }
            StaticDataLoader loader;
            if (m_loaderMap.TryGetValue(fileInfo.Name, out loader))
            {
                loader.Load(fileInfo.Name, fileInfo.FullName, buffer);
            }
        }
    }

    partial void OnInit();
}
