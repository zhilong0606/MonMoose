using System;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class FrameSyncModule : Module
    {
        private bool isStart = false;
        private Dcm32 m_time;
        private Dcm32 m_deltaTime;
        private EBattlePlayMode m_playMode;
        private Action<byte[]> m_actionOnSendMsg;
        
        private FrameSyncSender m_sender = new FrameSyncSender();
        private FrameSyncReceiver m_receiver = new FrameSyncReceiver();
        private FrameSyncDummyServer m_dummyServer = new FrameSyncDummyServer();

        public bool isLocal
        {
            get { return m_playMode == EBattlePlayMode.Local; }
        }

        protected override void OnInit(BattleInitData initData)
        {
            base.OnInit(initData);
            m_playMode = initData.playMode;
            m_actionOnSendMsg = initData.actionOnSendMsg;
            m_sender.Init(m_battleInstance, this);
            m_receiver.Init(m_battleInstance, this);
            m_dummyServer.Init(m_battleInstance, this);
        }

        public void Start()
        {
            isStart = true;
        }

        public void Resume()
        {
            isStart = true;
        }

        public void Pause()
        {
            isStart = false;
        }

        public void ReceiveFrameCut(FrameCut cut)
        {
            m_receiver.Receive(cut);
        }

        public void Tick(float deltaTime)
        {
            m_dummyServer.Tick(deltaTime);
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        public FrameSyncSender GetSender()
        {
            return m_sender;
        }

        public void SendMsg(byte[] buffer)
        {
            if (m_actionOnSendMsg != null)
            {
                m_actionOnSendMsg(buffer);
            }
        }

        public void SendDummyServer(FrameCommand cmd)
        {
            m_dummyServer.Receive(cmd);
        }
    }
}