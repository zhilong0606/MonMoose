using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class ActorInfoComponent : EntityInfoComponent
    {
        private ActorStaticInfo m_actorStaticInfo;

        protected override void OnInitSpecific()
        {
            base.OnInitSpecific();
            m_actorStaticInfo = StaticDataManager.instance.GetActorStaticInfo(m_entityStaticInfo.RefId);
        }
    }
}
