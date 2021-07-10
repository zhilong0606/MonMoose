using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        public void Log(int level, string str)
        {
            if (m_debugModule != null)
            {
                m_debugModule.Log(level, str);
            }
        }
    }
}
