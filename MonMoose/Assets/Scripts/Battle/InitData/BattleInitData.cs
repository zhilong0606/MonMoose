using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class BattleInitData
    {
        public int id;
        public Action<int, string> actionOnDebug;
        public Func<EBattleViewControllerType, BattleViewController> funcOnCreateCtrl;
        //public Action<byte[]> actionOnSendMsg;
        public IBattleEventListener eventListener;
        public FrameSyncRelay relay;
        public List<TeamInitData> teamList = new List<TeamInitData>();
    }
}
