using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class FrameSyncReceiver
    {
        private BattleBase m_battleInstance;
        private FrameSyncModule m_frameSyncModule;

        public void Init(BattleBase battleInstance, FrameSyncModule frameSyncModule)
        {
            m_battleInstance = battleInstance;
            m_frameSyncModule = frameSyncModule;
        }

        public void Receive(FrameCut cut)
        {
            cut.Excute();
            m_battleInstance.FrameTick();
        }
    }
}
