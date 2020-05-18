using System;
using System.Collections.Generic;
using Structure;
using Data;

namespace Analyzer
{
    public class ClassGroupAnalyzer : StructureGroupAnalyzer<ClassTableAnalyzer, ClassStructureInfo>
    {
        public override ETableDataType dataType { get { return ETableDataType.Data; } }

        public ClassGroupAnalyzer(string name) : base(name)
        {
            RegisterAnalyzeStep(EAnalyzeStep.AnalyzeMember, AnalyzeMember);
            RegisterAnalyzeStep(EAnalyzeStep.CheckDefaultValue, CheckDefaultValue);
            RegisterAnalyzeStep(EAnalyzeStep.AnalyzeData, AnalyzeData);
        }

        protected override void OnAddTableAnalyzer(ClassTableAnalyzer analyzer)
        {
            analyzer.actionOnAddMemeber = OnAddMember;
            analyzer.actionOnAddData = OnAddData;
        }

        protected override void OnCreateStructure(BaseStructureInfo structureInfo)
        {
            base.OnCreateStructure(structureInfo);
            PackStructureInfo packStructureInfo = new PackStructureInfo(structureInfo as ClassStructureInfo);
            StructureManager.Instance.AddStructureInfo(packStructureInfo);
        }

        private void OnAddMember(BaseStructureInfo structureInfo, string name)
        {
            m_structureInfo.AddMember(structureInfo, name);
        }

        private void OnAddData(int id, DataObject dataObject)
        {
            DataObjectManager.Instance.AddDataObject(m_structureInfo, id, dataObject);
        }

        private void AnalyzeMember(UserContext context)
        {
            foreach (ClassTableAnalyzer analyzer in m_tableAnalyzerList)
            {
                analyzer.AnalyzeMember(context);
            }
        }

        private void CheckDefaultValue(UserContext context)
        {
            foreach (ClassTableAnalyzer analyzer in m_tableAnalyzerList)
            {
                analyzer.CheckDefaultValue(context);
            }
        }

        private void AnalyzeData(UserContext context)
        {
            foreach (ClassTableAnalyzer analyzer in m_tableAnalyzerList)
            {
                analyzer.AnalyzeData(context);
            }
        }
    }
}
