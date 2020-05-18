using System;
using System.Collections.Generic;
using Structure;

namespace Analyzer
{
    public class EnumGroupAnalyzer : StructureGroupAnalyzer<EnumTableAnalyzer, EnumStructureInfo>
    {
        public override ETableDataType dataType { get { return ETableDataType.Enum; } }

        public EnumGroupAnalyzer(string name) : base(name)
        {
            RegisterAnalyzeStep(EAnalyzeStep.AnalyzeMember, AnalyzeMember);
        }

        private void AnalyzeMember(UserContext context)
        {
            foreach (EnumTableAnalyzer tableAnlayzer in m_tableAnalyzerList)
            {
                tableAnlayzer.AnalyzeMember(context);
            }
        }

        protected override void OnAddTableAnalyzer(EnumTableAnalyzer analyzer)
        {
            analyzer.actionOnAddMemeber = OnAddMember;
        }

        private void OnAddMember(int memberIndex, string memberName)
        {
            m_structureInfo.AddMember(memberIndex, memberName);
        }
    }
}
