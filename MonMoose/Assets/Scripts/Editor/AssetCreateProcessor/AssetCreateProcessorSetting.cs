using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class AssetCreateProcessorSetting : BaseScriptableObject
    {
        public List<RuleSetting> ruleSettingList = new List<RuleSetting>();

        [Serializable]
        public class RuleSetting
        {
            public string folderPath;
            public string postfix;
            public AssetCreateSetting subSetting;
        }
    }
}
