using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public abstract class Module
    {
        protected BattleBase m_battleInstance;

        public void Init(BattleBase battleInstance, BattleInitData initData)
        {
            m_battleInstance = battleInstance;
            OnInit(initData);
        }

        public void Tick()
        {

        }

        protected virtual void OnInit(BattleInitData initData)
        {

        }

        protected virtual void OnTick()
        {
            
        }
    }
}
