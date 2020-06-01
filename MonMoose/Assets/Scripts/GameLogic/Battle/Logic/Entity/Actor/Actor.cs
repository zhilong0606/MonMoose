namespace MonMoose.Logic.Battle
{
    public class Actor : Entity
    {
        protected ActorInfoComponent m_actorInfo = new ActorInfoComponent();
        

        protected override void OnInitComponent()
        {
            m_actorInfo = new ActorInfoComponent();
            base.OnInitComponent();
        }
    }
}
