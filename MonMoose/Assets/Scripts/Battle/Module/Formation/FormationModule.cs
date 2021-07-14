using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class FormationModule : Module
    {
        public void AddFormationActor(EntityInitData initData)
        {
            m_battleInstance.CreateEntity(initData, EBattleObjType.DynamicEntity);
        }
        
        public void RemoveFormationActor(int uid)
        {
            m_battleInstance.RemoveEntity(uid);
        }

        public void ExchangeFormationActor()
        {

        }
    }
}
