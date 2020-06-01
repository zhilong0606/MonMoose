using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public abstract class Module
    {
        protected BattleBase m_battle;

        public void Init(BattleBase battle)
        {
            m_battle = battle;
            OnInit();
        }

        public void Tick()
        {

        }

        protected virtual void OnInit()
        {

        }

        protected virtual void OnTick()
        {
            
        }
    }
}
