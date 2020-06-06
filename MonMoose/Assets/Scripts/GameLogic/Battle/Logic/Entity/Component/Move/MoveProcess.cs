using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MoveProcess : PoolObj
    {
        private Grid m_fromGrid;
        private Grid m_toGrid;
        private Entity m_entity;
        
        private FixVec2 m_offset;
        private bool m_isSameGrid;

        private Action<Fix32> m_actionOnEnd;

        public void Init(Entity entity, Grid fromGrid, FixVec2 offset, Grid toGrid, Action<Fix32> actionOnEnd)
        {
            m_entity = entity;
            m_fromGrid = fromGrid;
            m_offset = offset;
            m_toGrid = toGrid;
            m_actionOnEnd = actionOnEnd;

            m_isSameGrid = fromGrid == toGrid;
        }

        public void Tick(Fix32 time)
        {
            FixVec2 toVec = m_isSameGrid ? -m_offset.normalized : (m_toGrid.gridPosition - m_fromGrid.gridPosition).ToFix();
            LocationComponent locationComponent = m_entity.GetComponent<LocationComponent>();
            locationComponent.SetForward(toVec);
            FixVec2 deltaPos = toVec * time * m_entity.GetComponent<EntityInfoComponent>().moveSpeed;
            FixVec2 offset = m_offset + deltaPos;
            FixVec2 calcOffset;
            if(!m_isSameGrid && m_fromGrid.TryGetOffset(m_fromGrid, offset, out calcOffset))
            {
                locationComponent.SetPosition(m_fromGrid, offset, false);
            }
            else
            {
                bool isInToGrid = m_toGrid.TryGetOffset(m_fromGrid, offset, out calcOffset);
                bool isArrived = FixVec2.Dot(toVec, calcOffset) >= 0 || !isInToGrid;
                if (isArrived)
                {
                    Fix32 leftTime = calcOffset.magnitude / m_entity.GetComponent<EntityInfoComponent>().moveSpeed;
                    locationComponent.SetPosition(m_toGrid, FixVec2.zero, false);
                    m_actionOnEnd(leftTime);
                }
                else
                {
                    locationComponent.SetPosition(m_toGrid, calcOffset, false);
                }
            }
            m_offset = offset;
        }
    }
}
