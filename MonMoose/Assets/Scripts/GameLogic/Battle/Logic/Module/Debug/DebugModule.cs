using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class DebugModule : Module
    {
        private Action<int, string> m_actionOnDebug;

        protected override void OnInit(BattleInitData initData)
        {
            base.OnInit(initData);
            m_actionOnDebug = initData.actionOnDebug;
        }

        public void Log(int level, string str)
        {
            if (m_actionOnDebug != null)
            {
                m_actionOnDebug(level, str);
            }
        }
    }
}
