using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class MoveComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Move; }
        }
        
        private List<BattleGrid> m_pathGridList = new List<BattleGrid>();
        private List<MoveProcess> m_movePorcessList = new List<MoveProcess>();
        private bool m_isStart;
        private int m_curIndex;
        private const int m_startIndex = 1;

        public void MoveToGrid(GridPosition gridPos)
        {
            Clear();
            BattleGrid targetGrid = m_battleInstance.GetGrid(gridPos);
            LocationComponent locationComponent = m_entity.GetComponent<LocationComponent>();
            DcmVec2 offset = locationComponent.offset;
            m_battleInstance.FindPath(m_entity, locationComponent.locateGrid, offset, targetGrid, m_pathGridList);
            if (m_pathGridList.Count <= m_startIndex)
            {
                if (m_isStart)
                {
                    StopMove();
                }
                return;
            }
            for (int i = m_startIndex; i < m_pathGridList.Count; ++i)
            {
                MoveProcess process = m_battleInstance.FetchPoolObj<MoveProcess>(this);
                process.Init(m_entity, m_pathGridList[i - 1], i == m_startIndex ? offset : DcmVec2.zero, m_pathGridList[i], OnMoveProcessEnd);
                m_movePorcessList.Add(process);
            }
            m_curIndex = 0;
            StartMove();
        }

        private void OnMoveProcessEnd(Dcm32 leftTime)
        {
            m_curIndex++;
            if (m_curIndex >= m_movePorcessList.Count)
            {
                StopMove();
                Clear();
                return;
            }
            MoveProcess process = m_movePorcessList[m_curIndex];
            process.Tick(leftTime);
        }

        protected override void OnTick()
        {
            if (m_isStart)
            {
                MoveProcess process = m_movePorcessList[m_curIndex];
                process.Tick(new Dcm32(50, 1000));
            }
            base.OnTick();
        }

        private void Clear()
        {
            for (int i = 0; i < m_movePorcessList.Count; ++i)
            {
                m_movePorcessList[i].Release();
            }
            m_movePorcessList.Clear();
        }

        private void StartMove()
        {
            m_isStart = true;
            m_entity.view.StartMove();
        }

        private void StopMove()
        {
            m_isStart = false;
            m_entity.view.StopMove();
        }
    }
}
