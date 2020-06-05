using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class LocationComponent : EntityComponent
    {
        protected Grid m_locateGrid;
        protected List<Grid> m_occupyGridList = new List<Grid>();
        protected FixVec2 m_offset;

        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Location; }
        }

        public Grid locateGrid
        {
            get { return m_locateGrid; }
        }

        public FixVec2 offset
        {
            get { return m_offset; }
        }

        protected override void OnInit(EntityInitData entityInitData)
        {
            base.OnInit(entityInitData);
            SetPosition(entityInitData.pos, FixVec2.zero, true);
        }

        public void SetPosition(GridPosition gridPosition, FixVec2 offset, bool isTeleport)
        {
            Grid grid = m_battleInstance.GetGrid(gridPosition);
            SetPosition(grid, offset, isTeleport);
        }

        public void SetPosition(Grid grid, FixVec2 offset, bool isTeleport)
        {
            if (grid == null)
            {
                return;
            }
            m_locateGrid = grid;
            m_offset = offset;
            m_entity.view.SetPosition(grid, m_offset, isTeleport);
            m_occupyGridList.Clear();
            EntityInfoComponent infoComponent = m_entity.GetComponent<EntityInfoComponent>();
            for (int i = 0; i < infoComponent.size; ++i)
            {
                for (int j = 0; j < infoComponent.size; ++j)
                {
                    Grid g = m_battleInstance.GetGrid(grid.gridPosition + new GridPosition(i, j));
                    if (g != null)
                    {
                        m_occupyGridList.Add(g);
                    }
                }
            }
        }

        public bool IsOccupyGrid(Grid grid)
        {
            return m_occupyGridList.Contains(grid);
        }

        public bool CanLocate(Grid grid)
        {
            EntityInfoComponent infoComponent = m_entity.GetComponent<EntityInfoComponent>();
            for (int i = 0; i < infoComponent.size; ++i)
            {
                for (int j = 0; j < infoComponent.size; ++j)
                {
                    Grid g = m_battleInstance.GetGrid(grid.gridPosition + new GridPosition(i, j));
                    if (g == null)
                    {
                        return false;
                    }

                    for (int k = 0; k < m_battleInstance.entityList.Count; ++k)
                    {
                        Entity entity = m_battleInstance.entityList[k];
                        if (entity == m_entity)
                        {
                            continue;
                        }
                        if (entity.GetComponent<LocationComponent>().IsOccupyGrid(g))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
