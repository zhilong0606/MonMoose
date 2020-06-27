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
        
        private DcmVec2 m_offset;
        private bool m_isSameGrid;

        private Action<Dcm32> m_actionOnEnd;

        public void Init(Entity entity, Grid fromGrid, DcmVec2 offset, Grid toGrid, Action<Dcm32> actionOnEnd)
        {
            m_entity = entity;
            m_fromGrid = fromGrid;
            m_offset = offset;
            m_toGrid = toGrid;
            m_actionOnEnd = actionOnEnd;

            m_isSameGrid = fromGrid == toGrid;
        }

        public void Tick(Dcm32 time)
        {
            DcmVec2 toVec = m_isSameGrid ? -m_offset.normalized : (m_toGrid.gridPosition - m_fromGrid.gridPosition).ToFix();
            LocationComponent locationComponent = m_entity.GetComponent<LocationComponent>();
            locationComponent.SetForward(toVec);
            DcmVec2 deltaPos = toVec * time * m_entity.GetComponent<EntityInfoComponent>().moveSpeed;
            DcmVec2 offset = m_offset + deltaPos;
            DcmVec2 calcOffset;
            if(!m_isSameGrid && m_fromGrid.TryGetOffset(m_fromGrid, offset, out calcOffset))
            {
                locationComponent.SetPosition(m_fromGrid, offset, false);
            }
            else
            {
                bool isInToGrid = m_toGrid.TryGetOffset(m_fromGrid, offset, out calcOffset);
                bool isArrived = DcmVec2.Dot(toVec, calcOffset) >= 0 || !isInToGrid;
                if (isArrived)
                {
                    Dcm32 leftTime = calcOffset.magnitude / m_entity.GetComponent<EntityInfoComponent>().moveSpeed;
                    locationComponent.SetPosition(m_toGrid, DcmVec2.zero, false);
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
