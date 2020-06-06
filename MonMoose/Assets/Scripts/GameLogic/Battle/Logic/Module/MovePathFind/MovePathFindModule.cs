using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MovePathFindModule : Module
    {
        private LinkedList<MovePath> m_openList = new LinkedList<MovePath>();
        private List<MovePath> m_closeList = new List<MovePath>();
        private List<MovePath> m_createdPathList = new List<MovePath>();
        private Grid m_startGrid;
        private Grid m_targetGrid;
        private Entity m_entity;
        private FixVec2 m_offset;

        public bool FindPath(Entity entity, Grid startGrid, FixVec2 offset, Grid targetGrid, List<Grid> gridList)
        {
            gridList.Clear();
            if (startGrid == targetGrid && offset == FixVec2.zero)
            {
                return false;
            }
            m_entity = entity;
            m_startGrid = startGrid;
            m_offset = offset;
            m_targetGrid = targetGrid;

            MovePath startPath = CreatePath();
            startPath.grid = m_startGrid;
            startPath.offset = m_offset;
            if (m_offset != FixVec2.zero)
            {
                AddToOpenList(startPath, m_startGrid, m_startGrid.GetCost(m_entity) * m_offset.magnitude / m_startGrid.size);
                Grid grid = m_battleInstance.GetGrid(GetNearGridPosition(startGrid.gridPosition, m_offset));
                if (grid != null)
                {
                    AddToOpenList(startPath, grid, m_startGrid.GetCost(m_entity) * (Fix32.half - m_offset.magnitude / m_startGrid.size) + grid.GetCost(m_entity) / 2);
                }
            }
            else
            {
                m_openList.AddLast(startPath);
            }

            EState state;
            do
            {
                state = FindPath();
            } while (state == EState.Finding);

            if (state == EState.Succeeded)
            {
                CollectPath(gridList);
            }
            Clear();
            return state == EState.Succeeded;
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
            Grid grid = m_battleInstance.GetGrid(path.grid.gridPosition + new GridPosition(offsetX, offsetY));
            if (grid == null || CheckInCloseList(grid) || !m_entity.GetComponent<LocationComponent>().CanLocate(grid))
            {
                return;
            }

            Fix32 g = path.grid.GetCost(m_entity) * Fix32.half + grid.GetCost(m_entity) / 2 + path.g;
            AddToOpenList(path, grid, g);
        }

        private void AddToOpenList(MovePath fromPath, Grid toGrid, Fix32 g)
        {
            Fix32 h = toGrid.gridPosition.DistanceTo(m_targetGrid.gridPosition);
            Fix32 f = g + h;
            MovePath openedPath = null;
            foreach (var path in m_openList)
            {
                if (path.grid == toGrid)
                {
                    openedPath = path;
                    break;
                }
            }
            if (openedPath != null)
            {
                if (openedPath.f > f)
                {
                    m_openList.Remove(openedPath);
                }
                else
                {
                    return;
                }
            }
            MovePath toPath = CreatePath();
            toPath.grid = toGrid;
            toPath.fromPath = fromPath;
            toPath.g = g;
            toPath.h = h;
            toPath.f = f;
            AddToOpenList(toPath);
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

        private void CollectPath(List<Grid> gridList)
        {
            MovePath path = m_openList.First.Value;
            while (path != null)
            {
                gridList.Add(path.grid);
                path = path.fromPath;
            }
            gridList.Reverse();
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

        private MovePath CreatePath()
        {
            MovePath path = m_battleInstance.FetchPoolObj<MovePath>();
            m_createdPathList.Add(path);
            return path;
        }

        private GridPosition GetNearGridPosition(GridPosition gridPosition, FixVec2 offset)
        {
            if (offset != FixVec2.zero)
            {
                if (offset.x != 0)
                {
                    return gridPosition + new GridPosition(MathFix.Sign(offset.x), 0);
                }
                if (offset.y != 0)
                {
                    return gridPosition + new GridPosition(0, MathFix.Sign(offset.y));
                }
            }
            return gridPosition;
        }

        private void Clear()
        {
            m_entity = null;
            m_startGrid = null;
            m_targetGrid = null;
            for (int i = 0; i < m_createdPathList.Count; ++i)
            {
                m_createdPathList[i].Release();
            }
            m_createdPathList.Clear();
            m_openList.Clear();
            m_closeList.Clear();
        }

        private enum EState
        {
            Failed,
            Succeeded,
            Finding,
        }

    }
}
