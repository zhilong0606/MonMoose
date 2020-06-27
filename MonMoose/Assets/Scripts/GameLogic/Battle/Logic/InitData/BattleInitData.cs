﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class BattleInitData
    {
        public int id;
        public Action<int, string> actionOnDebug;
        public Func<int, EntityView> funcOnGetView;
        //public Action<byte[]> actionOnSendMsg;
        public FrameSyncServer server;
        public FrameSyncSender sender;
        public EBattlePlayMode playMode;
        public List<TeamInitData> teamList = new List<TeamInitData>();
    }
}
