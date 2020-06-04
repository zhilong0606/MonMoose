using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class Grid : BattleObj
    {
        private GridPosition m_gridPosition;
        private Fix32 m_size;
        private FixVec2 m_stagePosition;
        private GridStaticInfo m_staticInfo;
        
        public GridPosition gridPosition
        {
            get { return m_gridPosition; }
        }

        public FixVec2 stagePosition
        {
            get { return m_stagePosition; }
        }

        public Fix32 size
        {
            get { return m_size; }
        }

        public void Init(int id, GridPosition gridPosition, FixVec2 stagePosition, Fix32 size)
        {
            m_staticInfo = StaticDataManager.instance.GetGridStaticInfo(id);
            m_gridPosition = gridPosition;
            m_stagePosition = stagePosition;
            m_size = size;
        }

        public float GetCost(Entity entity)
        {
            return 1f;
        }
    }
}
