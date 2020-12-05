using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class FrameSyncHandler
    {
        private BattleBase m_battleInstance;
        private FrameSyncRelay m_relay;
        private List<FrameCut> m_frameCutList = new List<FrameCut>();
        private FrameCut m_curFrameCut;
        private float m_curTime;
        private int m_frameIndex;

        public void Init(BattleBase battleInstance, FrameSyncRelay relay)
        {
            m_battleInstance = battleInstance;
            m_relay = relay;
        }

        public void Tick(float deltaTime)
        {
            m_curTime += deltaTime;
            while (m_curTime > FrameSyncDefine.TimeInterval)
            {
                m_curTime -= FrameSyncDefine.TimeInterval;
                SendFrameCut();
            }
        }

        public void SendFrameCut()
        {
            FrameCut cut = m_battleInstance.FetchPoolObj<FrameCut>(this);
            //if (m_curGroup != null)
            //{
            //    cut.AddCmdGroup(m_curGroup);
            //    m_curGroup = null;
            //}
            cut.frameIndex = m_frameIndex;
            m_relay.Send(cut);
            m_frameIndex++;
        }

        public void AddCommand(int playerId, FrameCommand cmd)
        {
            if (m_curFrameCut == null)
            {
                m_curFrameCut = m_battleInstance.FetchPoolObj<FrameCut>(this);
            }
            m_curFrameCut.AddCommand(playerId, cmd);
        }
    }
}
