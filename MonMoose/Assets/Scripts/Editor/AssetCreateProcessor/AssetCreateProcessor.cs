using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace MonMoose.Core
{
    public class AssetCreateProcessor : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            AssetCreateProcessorSetting setting = LoadSetting();
            List<AssetCreateProcessorSetting.RuleSetting> ruleSettingList = new List<AssetCreateProcessorSetting.RuleSetting>(setting.ruleSettingList);
            ruleSettingList.Sort((x, y) => string.Compare(y.folderPath, x.folderPath, StringComparison.Ordinal));
            foreach (AssetCreateProcessorSetting.RuleSetting ruleSetting in ruleSettingList)
            {
                if (path.StartsWith(ruleSetting.folderPath) && path.EndsWith(ruleSetting.postfix + ".meta"))
                {
                    ruleSetting.subSetting.Apply(path, ruleSetting);
                    return;
                }
            }
        }

        private static AssetCreateProcessorSetting LoadSetting()
        {
            string settingTypePath = AssetPathUtility.GetClassAssetPath(typeof(AssetCreateProcessorSetting));
            string settingPath = AssetPathUtility.GetFileFolderPath(settingTypePath) + "/Settings/" + AssetPathUtility.GetFileName(settingTypePath, false, true) + ".asset";
            return AssetDatabase.LoadAssetAtPath<AssetCreateProcessorSetting>(settingPath);
        }
    }
}
