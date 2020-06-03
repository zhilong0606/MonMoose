using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class FrameSyncDummyServer
    {
        private BattleBase m_battleInstance;
        private FrameSyncModule m_frameSyncModule;
        private FrameCommandGroup m_curGroup;
        private float m_curTime = 0f;
        private int m_frameIndex;

        public void Init(BattleBase battleInstance, FrameSyncModule frameSyncModule)
        {
            m_battleInstance = battleInstance;
            m_frameSyncModule = frameSyncModule;
        }

        public void Receive(FrameCommand cmd)
        {
            if (m_curGroup == null)
            {
                m_curGroup = m_battleInstance.FetchPoolObj<FrameCommandGroup>();
            }
            m_curGroup.AddCommand(cmd);
        }

        public void Tick(float deltaTime)
        {
            m_curTime += deltaTime;
            while (m_curTime > FrameSyncDefine.TimeInterval)
            {
                m_curTime -= FrameSyncDefine.TimeInterval;
                FrameCut cut = m_battleInstance.FetchPoolObj<FrameCut>();
                if (m_curGroup != null)
                {
                    cut.AddCmdGroup(m_curGroup);
                    m_curGroup = null;
                }
                cut.frameIndex = m_frameIndex;
                m_frameSyncModule.ReceiveFrameCut(cut);
                m_frameIndex++;
            }
        }
    }
}
