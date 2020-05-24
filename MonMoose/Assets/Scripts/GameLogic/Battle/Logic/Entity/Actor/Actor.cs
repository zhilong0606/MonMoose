namespace MonMoose.Logic.Battle
{
    public class Actor : Entity
    {
        protected ActorInfoComponent m_actorInfo = new ActorInfoComponent();

        public void Init(int actorID)
        {
            //ActorStaticInfo staticInfo = StaticDataManager.instance.GetActorStaticInfo(actorID);
            //m_actorInfo.Init(staticInfo);
        }
    }
}
