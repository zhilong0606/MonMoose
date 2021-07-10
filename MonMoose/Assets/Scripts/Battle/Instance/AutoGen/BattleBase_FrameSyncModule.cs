using System;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        public void StartFrameSync()
        {
            if (m_frameSyncModule != null)
            {
                m_frameSyncModule.StartFrameSync();
            }
        }
    }
}
