using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class ActorInfoComponent : EntityInfoComponent
    {
        private ActorStaticInfo m_actorStaticInfo;

        public ActorStaticInfo actorStaticInfo
        {
            get { return m_actorStaticInfo; }
        }

        public override int size
        {
            get { return m_actorStaticInfo.Size; }
        }

        public override Dcm32 moveSpeed
        {
            get { return new Dcm32(m_actorStaticInfo.MoveSpeed, 1000); }
        }

        protected override void OnInitSpecific(EntityInitData entityInitData)
        {
            base.OnInitSpecific(entityInitData);
            m_actorStaticInfo = StaticDataManager.instance.GetActorStaticInfo(m_entityStaticInfo.SpecificId);
        }
    }
}
