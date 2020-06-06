using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class Grid : BattleObj
    {
        private GridPosition m_gridPosition;
        private Fix32 m_size;
        private FixVec2 m_stagePosition;
        private TerrainStaticInfo m_staticInfo;

        private List<Entity> m_occupiedEntityList = new List<Entity>();
        
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
            m_staticInfo = StaticDataManager.instance.GetTerrainStaticInfo(id);
            m_gridPosition = gridPosition;
            m_stagePosition = stagePosition;
            m_size = size;
        }

        public Fix32 GetCost(Entity entity)
        {
            return Fix32.one;
        }

        public void AddOccupiedEntity(Entity entity)
        {
            m_occupiedEntityList.Add(entity);
        }

        public void RemoveOccupiedEntity(Entity entity)
        {
            m_occupiedEntityList.Remove(entity);
        }

        public bool CanLocate(Entity entity)
        {
            for (int i = 0; i < m_occupiedEntityList.Count; ++i)
            {
                if (m_occupiedEntityList[i] != entity)
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryGetOffset(Grid grid, FixVec2 offset, out FixVec2 targetOffset)
        {
            if (grid == this)
            {
                targetOffset = offset;
            }
            else
            {
                targetOffset = (grid.gridPosition - gridPosition).ToFix() * size + offset;
            }
            return MathFix.Abs(targetOffset.x) < size / 2 && MathFix.Abs(targetOffset.y) < size / 2;
        }
    }
}
