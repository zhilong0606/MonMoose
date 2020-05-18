using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure;

namespace Analyzer
{
    public abstract class StructureGroupAnalyzer<T, U> : StructureGroupAnalyzer
        where T : StructureTableAnalyzer 
        where U : BaseStructureInfo
        
    {
        protected List<T> m_tableAnalyzerList = new List<T>();
        protected U m_structureInfo;

        protected StructureGroupAnalyzer(string name) : base(name)
        {
        }

        public sealed override void AddTableAnalyzer(StructureTableAnalyzer tableAnalyzer)
        {
            if (!CanAddTableAnaylzer(tableAnalyzer))
            {
                return;
            }
            T analyzer = tableAnalyzer as T;
            m_tableAnalyzerList.Add(analyzer);
            OnAddTableAnalyzer(analyzer);
        }
        
        protected override void OnCreateStructure(BaseStructureInfo structureInfo)
        {
            m_structureInfo = structureInfo as U;
        }

        protected virtual void OnAddTableAnalyzer(T analyzer)
        {
        }
    }

    public abstract class StructureGroupAnalyzer
    {
        private string m_name;
        private Dictionary<int, Action<UserContext>> m_analyzeStopMap = new Dictionary<int, Action<UserContext>>();

        public string name
        {
            get { return m_name; }
        }

        public abstract ETableDataType dataType { get; }

        protected StructureGroupAnalyzer(string name)
        {
            m_name = name;
        }

        protected void RegisterAnalyzeStep(EAnalyzeStep step, Action<UserContext> action)
        {
            m_analyzeStopMap.Add((int)step, action);
        }

        public void AnalyzeStep(EAnalyzeStep step, UserContext context)
        {
            Action<UserContext> action;
            if (m_analyzeStopMap.TryGetValue((int)step, out action))
            {
                action(context);
            }
        }

        protected bool CanAddTableAnaylzer(StructureTableAnalyzer tableAnalyzer)
        {
            if (tableAnalyzer.dataType != dataType)
            {
                throw new Exception(string.Format(StaticString.StructureOneMoreDataTypeFormat, m_name));
            }
            return true;
        }

        public BaseStructureInfo CreateStructure()
        {
            BaseStructureInfo structureInfo = null;
            switch (dataType)
            {
                case ETableDataType.Enum:
                    structureInfo = new EnumStructureInfo(m_name);
                    break;
                case ETableDataType.Data:
                    structureInfo = new ClassStructureInfo(m_name);
                    break;
            }
            if (structureInfo != null)
            {
                StructureManager.Instance.AddStructureInfo(structureInfo);
                OnCreateStructure(structureInfo);
            }
            return structureInfo;
        }

        protected abstract void OnCreateStructure(BaseStructureInfo structureInfo);
        public abstract void AddTableAnalyzer(StructureTableAnalyzer tableAnalyzer);
    }
}
