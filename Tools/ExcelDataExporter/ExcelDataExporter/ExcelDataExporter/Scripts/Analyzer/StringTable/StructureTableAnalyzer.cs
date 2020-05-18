using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public abstract class StructureTableAnalyzer : StringTableAnalyzer
    {
        private TableHeaderAnalyzer m_headerAnalyzer;

        public string name
        {
            get { return m_headerAnalyzer.name; }
        }

        public ETableDataType dataType
        {
            get { return m_headerAnalyzer.dataType; }
        }

        public StructureTableAnalyzer(StringTable table, TableHeaderAnalyzer headerAnalyzer) : base(table)
        {
            m_headerAnalyzer = headerAnalyzer;
        }
    }
}
