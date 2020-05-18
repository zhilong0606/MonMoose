using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    public struct TablePosition
    {
        public int row;
        public int colm;

        public TablePosition(int row, int colm)
        {
            this.row = row;
            this.colm = colm;
        }
    }
}
