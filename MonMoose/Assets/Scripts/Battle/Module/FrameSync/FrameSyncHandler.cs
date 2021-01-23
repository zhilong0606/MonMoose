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

        public void ReceiveCommand(int playerId, FrameCommand cmd)
        {
            if (m_curFrameCut == null)
            {
                m_curFrameCut = m_battleInstance.FetchPoolObj<FrameCut>(this);
                m_curFrameCut.frameIndex = m_frameIndex;
                m_frameCutList.Add(m_curFrameCut);
            }
            m_curFrameCut.AddCommand(playerId, cmd);
            m_relay.Send(m_curFrameCut);
            m_curFrameCut = null;
            m_frameIndex++;
        }
    }
}
