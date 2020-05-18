using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public abstract class StringTableAnalyzer
    {
        protected StringTable m_table;

        public StringTableAnalyzer(StringTable table)
        {
            m_table = table;
        }
    }
}
