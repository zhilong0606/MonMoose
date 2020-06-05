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

        protected override void OnInitSpecific(EntityInitData entityInitData)
        {
            base.OnInitSpecific(entityInitData);
            m_actorStaticInfo = StaticDataManager.instance.GetActorStaticInfo(m_entityStaticInfo.RefId);
        }
    }
}
