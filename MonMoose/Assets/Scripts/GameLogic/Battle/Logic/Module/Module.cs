using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public abstract class Module
    {
        protected BattleBase m_battleInstance;

        public void Init(BattleBase battleInstance, BattleInitData battleInitData)
        {
            m_battleInstance = battleInstance;
            OnInit(battleInitData);
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
