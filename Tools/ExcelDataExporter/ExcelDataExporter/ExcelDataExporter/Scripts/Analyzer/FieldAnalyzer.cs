using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Structure;

namespace Analyzer
{
    public class FieldAnalyzer
    {
        private static Regex m_complexRegex = new Regex(@"^\s*(\w+)\s*{(.*)}\s*$");
        private static Regex m_singleRegex = new Regex(@"^\s*(\w+)\s*$");
        private static Regex m_listRegex = new Regex(@"^\s*List\s*<(.*)>\s*$");


        //Comp1 {count:Int32,name:String}
        private BaseStructureInfo m_structureInfo;

        private string m_name;
        private string m_structureName;
        private string m_defaultValue;
        private string m_foreign;
        private List<string> m_tagList = new List<string>();
        private bool m_isList;
        private bool m_isComplex;

        public List<string> complexMemberNameList = new List<string>();

        public BaseStructureInfo structureInfo
        {
            get { return m_structureInfo; }
        }

        public string name
        {
            get { return m_name; }
        }

        public string defaultValue
        {
            get { return m_defaultValue; }
        }

        public FieldAnalyzer(string tagStr, string structureName, string name, string defaultValue, string foreign)
        {
            string[] splits = tagStr.Split('|');
            for (int i = 0; i < splits.Length; ++i)
            {
                string split = splits[i].Trim();
                if (!string.IsNullOrEmpty(split))
                {
                    m_tagList.Add(split);
                }
            }
            m_name = name;
            m_structureName = structureName;
            m_defaultValue = defaultValue;
            m_foreign = foreign;
            m_isList = false;
        }

        public void Analyze()
        {
            m_structureInfo = AnalyzeStructure(m_structureName);
        }

        public void CheckDefaultValid()
        {

        }

        public void CheckForeignValid()
        {

        }


        private BaseStructureInfo AnalyzeStructure(string str)
        {
            BaseStructureInfo structureInfo = AnalyzeList(str);
            if (structureInfo == null)
            {
                structureInfo = AnalyzeSimpleStructure(str);
            }
            if (structureInfo == null)
            {
                structureInfo = AnalyzeComplexStructure(str);
            }
            if (structureInfo == null)
            {
                Debug.LogError("");
            }
            return structureInfo;
        }

        private BaseStructureInfo AnalyzeSimpleStructure(string str)
        {
            Match match = m_singleRegex.Match(str);
            if (!match.Success)
            {
                return null;
            }
            string structureName = match.Groups[1].Value;
            BaseStructureInfo structureInfo = StructureManager.Instance.GetStructureInfo(structureName);
            if (structureInfo.structureType == EStructureType.Basic || structureInfo.structureType == EStructureType.Enum)
            {
                return structureInfo;
            }
            Debug.LogError("");
            return null;
        }

        private BaseStructureInfo AnalyzeList(string str)
        {
            if (m_isList || m_isComplex)
            {
                return null;
            }
            Match match = m_listRegex.Match(str);
            if (!match.Success)
            {
                return null;
            }
            m_isList = true;
            BaseStructureInfo innerStructureInfo = AnalyzeStructure(match.Groups[1].Value);
            if (innerStructureInfo == null)
            {
                return null;
            }
            ListStructureInfo listStructureInfo = new ListStructureInfo(innerStructureInfo);
            BaseStructureInfo structureInfo = StructureManager.Instance.GetStructureInfo(listStructureInfo.name);
            if (structureInfo != null)
            {
                return structureInfo;
            }
            StructureManager.Instance.AddStructureInfo(listStructureInfo);
            return listStructureInfo;
        }

        private BaseStructureInfo AnalyzeComplexStructure(string str)
        {
            if (m_isComplex)
            {
                Debug.LogError("");
                return null;
            }
            Match match = m_complexRegex.Match(str);
            if (!match.Success)
            {
                return null;
            }
            m_isComplex = true;
            string structureName = match.Groups[1].Value;
            BaseStructureInfo structureInfo = StructureManager.Instance.GetStructureInfo(structureName);
            if (structureInfo == null)
            {
                structureInfo = new ClassStructureInfo(structureName);
                StructureManager.Instance.AddStructureInfo(structureInfo);
            }
            ClassStructureInfo classStructureInfo = structureInfo as ClassStructureInfo;
            if (classStructureInfo == null)
            {
                return null;
            }
            string[] memberSplits = match.Groups[2].Value.Split(new[] {','}, null, null, false, true);
            for (int i = 0; i < memberSplits.Length; ++i)
            {
                string memberStr = memberSplits[i];
                string[] memberParamSplits = memberStr.Split(new[] {':'}, null, null, false, true);
                if (memberParamSplits.Length != 2)
                {
                    complexMemberNameList.Add(string.Empty);
                    Debug.LogError("");
                    continue;
                }
                string memberName = memberParamSplits[0];
                string memberStructureName = memberParamSplits[1];
                if (!classStructureInfo.HasMember(memberName))
                {
                    BaseStructureInfo memberStructureInfo = AnalyzeSimpleStructure(memberStructureName);
                    classStructureInfo.AddMember(memberStructureInfo, memberName);
                }
                complexMemberNameList.Add(memberName);
            }
            return structureInfo;
        }
    }
}
