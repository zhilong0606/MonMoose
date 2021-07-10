using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public abstract class AssetCreateSetting : BaseScriptableObject
    {
        public abstract void Apply(string path, AssetCreateProcessorSetting.RuleSetting setting);
    }
}
