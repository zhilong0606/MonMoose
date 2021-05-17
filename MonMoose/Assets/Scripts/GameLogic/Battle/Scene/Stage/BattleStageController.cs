using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonMoose.GameLogic.Battle
{
    public class BattleStageController : BattleStageControllerAbstract
    {
        private BattleStageInitializer m_initializer = new BattleStageInitializer();

        public override void InitView()
        {
        }

        public override void UnInitView()
        {
        }

        public override void StartLoadScene(Action actionOnEnd)
        {
            m_initializer.Init(m_owner.staticInfo);
            m_initializer.StartAsync(initializer => actionOnEnd());
            EventManager.instance.Broadcast((int)EventID.BattleStage_SetActive, m_owner);
        }
    }
}
