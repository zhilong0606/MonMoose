using System;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public abstract class EntityComponent : BattleObj
    {
        public abstract EEntityComponentType type { get; }

        protected Entity m_entity;
        private EntityComponentInitInvoker m_initInvoker = new EntityComponentInitInvoker();

        public void Init(Entity entity, EntityInitData entityInitData)
        {
            m_entity = entity;
            OnInit(entityInitData);
            OnInitRegister(m_initInvoker);
            m_initInvoker.Invoke(entityInitData);
        }

        public void Tick()
        {
            OnTick();
        }

        protected virtual void OnInitRegister(EntityComponentInitInvoker invoker)
        {
        }

        protected virtual void OnInit(EntityInitData entityInitData)
        {

        }

        protected virtual void OnTick()
        {

        }

        public static int Sort(EntityComponent x, EntityComponent y)
        {
            return x.type.CompareTo(y.type);
        }
    }
}
