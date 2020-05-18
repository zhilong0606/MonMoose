using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public class TableHeaderAnalyzer : StringTableAnalyzer
    {
        private static TablePosition m_typePos = new TablePosition(0, 0);
        private static TablePosition m_namePos = new TablePosition(0, 1);
        private static string[] sheetTypeStrs = new string[(int)ETableDataType.Max];

        private string m_name;
        private ETableDataType m_dataType;

        public string name { get { return m_name; } }
        public ETableDataType dataType { get { return m_dataType; } }

        static TableHeaderAnalyzer()
        {
            for (int i = 0; i < sheetTypeStrs.Length; ++i)
            {
                sheetTypeStrs[i] = ((ETableDataType)i).ToString();
            }
        }

        public TableHeaderAnalyzer(StringTable table) : base(table)
        {
        }

        public void Analyze()
        {
            m_dataType = AdaptExcelDataType(m_table[m_typePos]);
            m_name = m_table[m_namePos];
        }

        private static ETableDataType AdaptExcelDataType(string str)
        {
            for (int i = 0; i < sheetTypeStrs.Length; ++i)
            {
                if (str == sheetTypeStrs[i])
                {
                    return (ETableDataType)i;
                }
            }
            return ETableDataType.None;
        }
    }
}
