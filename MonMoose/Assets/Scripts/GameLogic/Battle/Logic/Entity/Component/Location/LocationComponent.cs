﻿using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class LocationComponent : EntityComponent
    {
        protected Grid m_grid;
        protected FixVec2 m_offset;

        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Location; }
        }

        public Grid grid
        {
            get { return m_grid; }
        }

        public FixVec2 offset
        {
            get { return m_offset; }
        }

        protected override void OnInit(EntityInitData entityInitData)
        {
            base.OnInit(entityInitData);
            m_grid = m_battleInstance.GetGrid(entityInitData.pos);
            SetPosition(m_grid, FixVec2.zero, true);
        }

        public void SetPosition(Grid grid, FixVec2 offset, bool isTeleport)
        {
            m_grid = grid;
            m_offset = offset;
            m_entity.view.SetPosition(m_grid, m_offset, isTeleport);
        }

        public void SetPosition(GridPosition gridPosition, FixVec2 offset, bool isTeleport)
        {
            m_grid = m_battleInstance.GetGrid(gridPosition);
            SetPosition(m_grid, offset, isTeleport);
        }
    }
}
