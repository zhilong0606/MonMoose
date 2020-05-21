using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.Logic
{
    public class Actor : Entity
    {
        protected ActorInfo m_actorInfo = new ActorInfo();

        public void Init(int actorID)
        {
            ActorStaticInfo staticInfo = StaticDataManager.instance.GetActorStaticInfo(actorID);
            m_actorInfo.Init(staticInfo);
        }
    }
}
