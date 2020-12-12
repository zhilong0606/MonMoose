using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MonMoose.Core
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        public const string resourcesPrefix = "";
        public const string assetBundlePrefix = "Exported/";

        public Object GetResource(string path)
        {
            return Load(path);
        }

        public GameObject GetPrefab(string path)
        {
            return Load(path) as GameObject;
        }

        public Sprite GetSprite(string path)
        {
            Object[] objs = LoadAllFormAssetBundle(path);
            foreach (Object obj in objs)
            {
                Sprite sprite = obj as Sprite;
                if (sprite != null)
                {
                    return obj as Sprite;
                }
            }
            objs = LoadAllFormResources(path);
            foreach (Object obj in objs)
            {
                Sprite sprite = obj as Sprite;
                if (sprite != null)
                {
                    return obj as Sprite;
                }
            }
            return null;
        }

        private Object Load(string path)
        {
            Object res = LoadFormAssetBundle(path);
            if (res == null)
            {
                res = LoadFormResources(path);
            }
            return res;
        }

        private Object LoadFormResources(string path)
        {
            return Resources.Load(resourcesPrefix + CutPostfix(path));
        }

        private Object LoadFormAssetBundle(string path)
        {
            return Resources.Load(assetBundlePrefix + CutPostfix(path));
        }

        private Object[] LoadAllFormResources(string path)
        {
            return Resources.LoadAll(resourcesPrefix + CutPostfix(path));
        }

        private Object[] LoadAllFormAssetBundle(string path)
        {
            return Resources.LoadAll(assetBundlePrefix + CutPostfix(path));
        }

        private string CutPostfix(string str)
        {
            int dotIndex = str.LastIndexOf(".", StringComparison.Ordinal);
            if (dotIndex >= 0)
            {
                str = str.Substring(0, dotIndex);
            }
            return str;
        }
    }
}
