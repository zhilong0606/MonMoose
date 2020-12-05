using MonMoose.StaticData;

namespace MonMoose.Battle
{
    public class Actor : Entity
    {
        protected override void OnInitComponent()
        {
            m_componentList.Add(m_battleInstance.FetchPoolObj<EntityInitComponent>(this));
            m_componentList.Add(m_battleInstance.FetchPoolObj<ActorInfoComponent>(this));
            m_componentList.Add(m_battleInstance.FetchPoolObj<MoveComponent>(this));
            m_componentList.Add(m_battleInstance.FetchPoolObj<AnimationComponent>(this));
            m_componentList.Add(m_battleInstance.FetchPoolObj<SkillComponent>(this));
            base.OnInitComponent();
        }
    }
}
