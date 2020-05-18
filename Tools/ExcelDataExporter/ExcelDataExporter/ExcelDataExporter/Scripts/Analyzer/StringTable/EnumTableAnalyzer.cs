using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Structure;

namespace Analyzer
{
    public class EnumTableAnalyzer : StructureTableAnalyzer
    {
        private const int m_startRowIndex = 2;
        private const int m_indexColmIndex = 0;
        private const int m_nameColmIndex = 1;

        public Action<int, string> actionOnAddMemeber;

        public EnumTableAnalyzer(StringTable table, TableHeaderAnalyzer headerAnalyzer) : base(table, headerAnalyzer)
        {
        }

        public void AnalyzeMember(UserContext context)
        {
            for (int i = m_startRowIndex; i < m_table.rowCount; ++i)
            {
                string indexStr = m_table[i, m_indexColmIndex];
                string nameStr = m_table[i, m_nameColmIndex];
                int indexValue;
                if (!int.TryParse(indexStr, out indexValue))
                {
                    continue;
                }
                if (actionOnAddMemeber != null)
                {
                    actionOnAddMemeber(indexValue, nameStr);
                }
            }
        }
    }
}
