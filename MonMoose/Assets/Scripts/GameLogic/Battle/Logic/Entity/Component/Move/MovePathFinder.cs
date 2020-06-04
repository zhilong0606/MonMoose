using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MovePathFinder : BattleObj
    {
        private LinkedList<MovePath> m_openList = new LinkedList<MovePath>();
        private List<MovePath> m_closeList = new List<MovePath>();
        private Grid m_startGrid;
        private Grid m_targetGrid;
        private Entity m_entity;

        public bool FindPath(Entity entity, Grid targetGrid, List<Grid> gridList)
        {
            gridList.Clear();
            m_entity = entity;
            m_startGrid = entity.GetComponent<LocationComponent>().grid;
            if (entity.GetComponent<LocationComponent>().grid == m_targetGrid)
            {
                return false;
            }
            m_targetGrid = targetGrid;
            MovePath startPath = m_battleInstance.FetchPoolObj<MovePath>();
            startPath.grid = m_startGrid;
            m_openList.AddLast(startPath);

            EState state = EState.Finding;
            while (state == EState.Finding)
            {
                state = FindPath();
            }
            if (state == EState.Succeeded)
            {
                MovePath path = m_openList.First.Value;
                while (path != null)
                {
                    gridList.Add(path.grid);
                    path = path.nextPath;
                }
            }
            gridList.Reverse();
            return true;
        }

        private EState FindPath()
        {
            if (m_openList.Count == 0)
            {
                return EState.Failed;
            }
            MovePath path = m_openList.First.Value;
            if (path.grid == m_targetGrid)
            {
                return EState.Succeeded;
            }
            m_openList.RemoveFirst();
            m_closeList.Add(path);
            AddNewPath(path, -1, 0);
            AddNewPath(path, 1, 0);
            AddNewPath(path, 0, -1);
            AddNewPath(path, 0, 1);
            return EState.Finding;
        }

        private void AddNewPath(MovePath path, int offsetX, int offsetY)
        {
            Grid grid = m_battleInstance.GetGrid(path.grid.gridPosition.x + offsetX, path.grid.gridPosition.y + offsetY);
            if (grid == null || CheckInCloseList(grid))
            {
                return;
            }
            MovePath newPath = m_battleInstance.FetchPoolObj<MovePath>();
            newPath.grid = grid;
            newPath.nextPath = path;
            newPath.g = (path.grid.GetCost(m_entity) + grid.GetCost(m_entity)) / 2f + path.g;
            newPath.h = grid.gridPosition.DistanceTo(path.grid.gridPosition);
            newPath.f = newPath.g + newPath.h;
            AddToOpenList(newPath);
        }

        private void AddToOpenList(MovePath path)
        {
            LinkedListNode<MovePath> current = m_openList.First;
            while (current != null)
            {
                if (path.f < current.Value.f)
                {
                    break;
                }
                current = current.Next;
            }

            if (current != null)
            {
                m_openList.AddBefore(current, path);
            }
            else
            {
                m_openList.AddLast(path);
            }
        }

        private bool CheckInCloseList(Grid grid)
        {
            for (int i = 0; i < m_closeList.Count; ++i)
            {
                if (m_closeList[i].grid == grid)
                {
                    return true;
                }
            }
            return false;
        }

        public enum EState
        {
            Failed,
            Succeeded,
            Finding,
        }

    }
}
