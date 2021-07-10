using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    public class AssetCreateSettingCode : AssetCreateSetting
    {
        public string nameSpace;
        public string superClassName;
        public List<string> usingNameSpaceList = new List<string>();

        public override void Apply(string path, AssetCreateProcessorSetting.RuleSetting setting)
        {
            string typeName = AssetPathUtility.GetFileName(path, false, true);
            path = path.Replace(".meta", "");
            CodeWriter writer = new CodeWriter();
            foreach (string usingNameSpace in usingNameSpaceList)
            {
                writer.AppendLine("using {0};", usingNameSpace);
            }
            writer.AppendEmptyLine();
            if (!string.IsNullOrEmpty(nameSpace))
            {
                writer.AppendLine("namespace {0}", nameSpace);
                writer.StartBlock();
            }
            if (string.IsNullOrEmpty(superClassName))
            {
                writer.AppendLine("public class {0}", typeName);
            }
            else
            {
                writer.AppendLine("public class {0} : {1}", typeName, superClassName);
            }
            writer.StartBlock();
            {
            }
            writer.EndBlock();
            if (!string.IsNullOrEmpty(nameSpace))
            {
                writer.EndBlock();
            }
            writer.WriteFile(path);
        }
    }
}
