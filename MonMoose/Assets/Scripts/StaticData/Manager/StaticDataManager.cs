using System;
using System.Collections.Generic;
using System.IO;

namespace MonMoose.StaticData
{
    public partial class StaticDataManager
    {
        private static Dictionary<string, StaticDataLoader> m_loaderMap = new Dictionary<string, StaticDataLoader>();

        private static StaticDataManager m_instance;

        public static StaticDataManager instance
        {
            get
            {
                CreateInstance();
                return m_instance;
            }
        }

        public static void CreateInstance()
        {
            if (m_instance == null)
            {
                m_instance = new StaticDataManager();
                m_instance.OnInitLoaders();
            }
        }

        public static void DestroyInstance()
        {
            m_instance = null;
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
            OnLoadAllEnd();
        }

        partial void OnInitLoaders();
        partial void OnLoadAllEnd();
    }
}
