using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class Grid : BattleObj
    {
        private GridPosition m_pos;
        private GridStaticInfo m_staticInfo;
        
        public GridPosition gridPos
        {
            get { return m_pos; }
        }

        public void Init(int id, int x, int y)
        {
            m_staticInfo = StaticDataManager.instance.GetGridStaticInfo(id);
            m_pos = new GridPosition(x, y);
        }
    }
}
