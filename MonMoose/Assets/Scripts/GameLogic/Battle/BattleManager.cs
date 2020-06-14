﻿using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic.Battle
{
    public class BattleManager : Singleton<BattleManager>
    {
        private BattleBase m_battleInstance;
        private GameObject m_actorRoot;

        public GameObject actorRoot
        {
            get { return m_actorRoot; }
        }

        public BattleBase battleInstance
        {
            get { return m_battleInstance; }
        }
        //private SkillController skillController = new SkillController();
        //private MoveController moveController = new MoveController();

        public void SetSceneConfig(BattleSceneConfig sceneConfig)
        {
            m_actorRoot = sceneConfig.actorRoot;
            //skillController.Init();
            //moveController.Init();
            //RegisterListener();
        }

        public void SetBattleInstance(BattleBase battleInstance)
        {
            m_battleInstance = battleInstance;
        }

        private void RegisterListener()
        {

        }

        private void RemoveListener()
        {

        }

        public void UpdateLogic()
        {
        }

        public void Clear()
        {
            //skillController.Clear();
            //moveController.Clear();
            RemoveListener();
        }
    }
}
