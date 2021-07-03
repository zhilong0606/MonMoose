using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.Battle
{
    public class BattleGrid : BattleObj
    {
        private GridPosition m_gridPosition;
        private Dcm32 m_size;
        private DcmVec2 m_stagePosition;
        private BattleTerrainStaticInfo m_staticInfo;
        private BattleGridControllerAbstract m_ctrl;

        private List<Entity> m_occupiedEntityList = new List<Entity>();
        
        public GridPosition gridPosition
        {
            get { return m_gridPosition; }
        }

        public DcmVec2 stagePosition
        {
            get { return m_stagePosition; }
        }

        public Dcm32 size
        {
            get { return m_size; }
        }

        public BattleGridControllerAbstract ctrl
        {
            get { return m_ctrl; }
        }

        public void Init(int id, GridPosition gridPosition, DcmVec2 stagePosition, Dcm32 size)
        {
            m_staticInfo = StaticDataManager.instance.GetBattleTerrain(id);
            m_gridPosition = gridPosition;
            m_stagePosition = stagePosition;
            m_size = size;
            m_ctrl = m_battleInstance.GetViewController(EBattleViewControllerType.Grid) as BattleGridControllerAbstract;
            m_ctrl.Init(this);
            m_ctrl.InitView();
        }

        public Dcm32 GetCost(Entity entity)
        {
            return Dcm32.one;
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

        public Entity GetExchangeEntity()
        {
            return m_occupiedEntityList.GetValueSafely(0);
        }

        public bool TryGetOffset(BattleGrid grid, DcmVec2 offset, out DcmVec2 targetOffset)
        {
            if (grid == this)
            {
                targetOffset = offset;
            }
            else
            {
                targetOffset = (grid.gridPosition - gridPosition).ToFix() * size + offset;
            }
            return MathDcm.Abs(targetOffset.x) < size / 2 && MathDcm.Abs(targetOffset.y) < size / 2;
        }
    }
}
