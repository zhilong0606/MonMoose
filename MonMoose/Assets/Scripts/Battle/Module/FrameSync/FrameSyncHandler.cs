using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class FrameSyncHandler
    {
        protected BattleBase m_battleInstance;
        protected FrameSyncModule m_frameSyncModule;
        protected FrameCommandUnion m_curGroup;
        protected List<EFrameCommandType> m_waitingCmdList = new List<EFrameCommandType>();
        protected float m_curTime;
        protected int m_frameIndex;

        public void Init(BattleBase battleInstance, FrameSyncModule frameSyncModule)
        {
            m_battleInstance = battleInstance;
            m_frameSyncModule = frameSyncModule;
        }

        public void Receive(FrameCommand cmd)
        {
            if (m_waitingCmdList.Count > 0)
            {
                if (cmd.commandType == m_waitingCmdList[0])
                {
                    m_waitingCmdList.RemoveAt(0);
                    AddCommand(cmd);
                    SendFrameCut();
                }
            }
            else
            {
                AddCommand(cmd);
            }
        }

        public void WaitFrameCommand(EFrameCommandType cmdType)
        {
            m_waitingCmdList.Add(cmdType);
            m_curTime = 0f;
        }

        public void Tick(float deltaTime)
        {
            if (m_waitingCmdList.Count == 0)
            {
                m_curTime += deltaTime;
                while (m_curTime > FrameSyncDefine.TimeInterval)
                {
                    m_curTime -= FrameSyncDefine.TimeInterval;
                    SendFrameCut();
                }
            }
        }

        public void SendFrameCut()
        {
            FrameCut cut = m_battleInstance.FetchPoolObj<FrameCut>(this);
            if (m_curGroup != null)
            {
                cut.AddCmdGroup(m_curGroup);
                m_curGroup = null;
            }
            cut.frameIndex = m_frameIndex;
            SendFrameCut(cut);
            m_frameIndex++;
        }

        public virtual void SendFrameCut(FrameCut cut)
        {
        }

        private void AddCommand(FrameCommand cmd)
        {
            if (m_curGroup == null)
            {
                m_curGroup = m_battleInstance.FetchPoolObj<FrameCommandUnion>(this);
            }
            m_curGroup.AddCommand(cmd);
        }
    }
}
