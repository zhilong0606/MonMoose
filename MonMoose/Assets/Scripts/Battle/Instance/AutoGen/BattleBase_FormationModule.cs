using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        public void AddFormationActor(EntityInitData initData)
        {
            if (m_formationModule != null)
            {
                m_formationModule.AddFormationActor(initData);
            }
        }

        public void RemoveFormationActor(int uid)
        {
            if (m_formationModule != null)
            {
                m_formationModule.RemoveFormationActor(uid);
            }
        }

        public void ExchangeFormationActor()
        {
            if (m_formationModule != null)
            {
                m_formationModule.ExchangeFormationActor();
            }
        }
    }
}
