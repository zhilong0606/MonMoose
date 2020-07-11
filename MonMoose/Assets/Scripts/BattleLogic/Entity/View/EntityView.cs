namespace MonMoose.BattleLogic
{
    public abstract class EntityView : BattleObj
    {
        protected Entity m_entity;

        internal void SetEntity(Entity entity)
        {
            m_entity = entity;
        }

        public virtual void PlayAnimation(string animName, float fadeTime)
        {

        }

        public virtual void SetPosition(BattleGrid grid, DcmVec2 offset, bool isTeleport)
        {

        }

        public virtual void StartMove()
        {

        }

        public virtual void StopMove()
        {

        }

        public virtual void CreateView()
        {

        }

        public virtual void DestroyView()
        {

        }

        public virtual void SetForward(DcmVec2 forward)
        {

        }

        public virtual void Tick(float deltaTime)
        {

        }
    }
}
