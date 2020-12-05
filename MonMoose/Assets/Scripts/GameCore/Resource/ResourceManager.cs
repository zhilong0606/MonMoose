using UnityEngine;

namespace MonMoose.Core
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        public Object GetResource(string path)
        {
            return Resources.Load(path);
        }

        public GameObject GetPrefab(string path)
        {
            return Resources.Load(path) as GameObject;
        }
    }
}
