using System;
using System.Collections.Generic;

namespace MonMoose.BattleLogic
{
    public class FrameSyncModule : Module
    {
        private bool isStart;
        private EBattlePlayMode m_playMode;
        
        private FrameSyncReceiver m_receiver = new FrameSyncReceiver();
        private FrameSyncServer m_server;
        private FrameSyncSender m_sender;

        public bool isLocal
        {
            get { return m_playMode == EBattlePlayMode.Local; }
        }

        public FrameSyncSender sender
        {
            get { return m_sender; }
        }

        protected override void OnInit(BattleInitData initData)
        {
            base.OnInit(initData);
            m_server = initData.server;
            m_sender = initData.sender;
            m_playMode = initData.playMode;
            m_receiver.Init(m_battleInstance, this);
            if (m_server != null)
            {
                m_server.Init(m_battleInstance, this);
            }
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

        public void WaitFrameCommand(EFrameCommandType cmdType)
        {
            if (m_server != null)
            {
                m_server.WaitFrameCommand(cmdType);
            }
        }

        public void ReceiveFrameCut(FrameCut cut)
        {
            m_receiver.Receive(cut);
        }

        public void Tick(float deltaTime)
        {
            if (!isStart)
            {
                return;
            }
            if (m_server != null)
            {
                m_server.Tick(deltaTime);
            }
        }
    }
}