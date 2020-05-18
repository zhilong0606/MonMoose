using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public class StringTable
    {
        private string[,] m_table = new string[0, 0]; //Row,Colm
        private int m_rowCount;
        private int m_colmCount;

        public string this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < rowCount && y >= 0 && y < colmCount)
                {
                    return m_table[x, y];
                }
                return string.Empty;
            }
        }

        public string this[TablePosition p]
        {
            get { return this[p.row, p.colm]; }
        }

        public int rowCount
        {
            get { return m_rowCount; }
        }

        public int colmCount
        {
            get { return m_colmCount; }
        }

        public StringTable(List<List<string>> grid)
        {
            m_rowCount = grid.Count;
            m_colmCount = 0;
            foreach (IList<string> list in grid)
            {
                m_colmCount = Math.Max(list.Count, m_colmCount);
            }
            m_table = new string[m_rowCount, m_colmCount];
            for (int rowIndex = 0; rowIndex < grid.Count; ++rowIndex)
            {
                IList<string> list = grid[rowIndex];
                for (int colmIndex = 0; colmIndex < list.Count; ++colmIndex)
                {
                    string str = list[colmIndex];
                    if (!string.IsNullOrEmpty(str))
                    {
                        m_table[rowIndex, colmIndex] = str;
                    }
                }
            }
        }
    }
}
