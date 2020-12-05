using System;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class FrameSyncModule : Module
    {
        private bool isStart;

        private FrameSyncReceiver m_receiver = new FrameSyncReceiver();
        private FrameSyncSender m_sender = new FrameSyncSender();
        private FrameSyncHandler m_handler = new FrameSyncHandler();
        private FrameSyncRelay m_relay;

        public FrameSyncSender sender
        {
            get { return m_sender; }
        }

        protected override void OnInit(BattleInitData initData)
        {
            base.OnInit(initData);
            m_relay = initData.relay;
            m_relay.Init(m_sender, m_receiver);
            m_sender.Init(m_battleInstance, m_relay);
            m_receiver.Init(m_handler, m_relay);
            m_handler.Init(m_battleInstance, m_relay);
        }

        public void Start()
        {
            isStart = true;
        }

        public void WaitFrameCommand(EFrameCommandType cmdType)
        {
            //if (m_server != null)
            //{
            //    m_server.WaitFrameCommand(cmdType);
            //}
        }

        public void Tick(float deltaTime)
        {
            if (!isStart)
            {
                return;
            }
            //if (m_server != null)
            //{
            //    m_server.Tick(deltaTime);
            //}
        }
    }
}