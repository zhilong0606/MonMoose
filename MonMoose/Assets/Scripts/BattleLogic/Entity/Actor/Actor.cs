using MonMoose.StaticData;

namespace MonMoose.BattleLogic
{
    public class Actor : Entity
    {
        protected override void OnInitComponent()
        {
            m_componentList.Add(m_battleInstance.FetchPoolObj<EntityInitComponent>());
            m_componentList.Add(m_battleInstance.FetchPoolObj<ActorInfoComponent>());
            m_componentList.Add(m_battleInstance.FetchPoolObj<MoveComponent>());
            m_componentList.Add(m_battleInstance.FetchPoolObj<AnimationComponent>());
            m_componentList.Add(m_battleInstance.FetchPoolObj<SkillComponent>());
            base.OnInitComponent();
        }
    }
}
