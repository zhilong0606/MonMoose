using System;
using System.Collections.Generic;
using System.IO;

namespace MonMoose.Core
{
    public partial class StaticDataManager : Singleton<StaticDataManager>
    {
        private static Dictionary<string, StaticDataLoader> m_loaderMap = new Dictionary<string, StaticDataLoader>();

        protected override void Init()
        {
            OnInit();
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
}
