using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.GameLogic
{
    public static class StaticDataShortCut
    {
        public static string GetPrefabPath(EPrefabPathId id)
        {
            PrefabPathStaticInfo info = StaticDataManager.instance.GetPrefabPath(id);
            if (info != null)
            {
                return info.Path;
            }
            return string.Empty;
        }
    }
}
