using System.Collections;
using System.IO;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic
{
    public class StaticDataInitializer : Initializer
    {
        protected override IEnumerator OnProcess()
        {
            StaticDataManager.instance.Load(Application.dataPath + "/Resources/Exported/StaticData", OnLoadData);
            yield return null;
        }

        private byte[] OnLoadData(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}
