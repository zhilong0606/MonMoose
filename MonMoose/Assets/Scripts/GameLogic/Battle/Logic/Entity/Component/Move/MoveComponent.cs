using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class MoveComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Move; }
        }
        
        private Grid m_curGrid;
        private Grid m_targetGrid;
        private FixVec2 m_offset;
        private List<Grid> m_pathGridList = new List<Grid>();
        private bool m_isStart;

        private int m_curIndex;
        private bool m_isArrived;

        public void MoveToGrid(GridPosition gridPos)
        {
            Grid targetGrid = m_battleInstance.GetGrid(gridPos);
            MovePathFinder pathFinder = m_battleInstance.FetchPoolObj<MovePathFinder>();
            pathFinder.FindPath(m_entity, targetGrid, m_pathGridList);
            if (m_pathGridList.Count < 2)
            {
                return;
            }
            m_curGrid = m_entity.GetComponent<LocationComponent>().locateGrid;
            m_targetGrid = targetGrid;
            m_offset = m_entity.GetComponent<LocationComponent>().offset;
            m_curIndex = 0;
            m_isStart = true;
            m_isArrived = false;
        }

        protected override void OnTick()
        {
            if (m_isStart)
            {
                Grid formGrid = m_pathGridList[m_curIndex];
                Grid toGrid = m_pathGridList[m_curIndex + 1];
                FixVec2 toVec = new FixVec2(toGrid.gridPosition.x - formGrid.gridPosition.x, toGrid.gridPosition.y - formGrid.gridPosition.y);
                FixVec2 deltaPos = toVec * new Fix32(50, 1000) * new Fix32(2000, 1000);
                FixVec2 offset = m_offset + deltaPos;
                Grid grid = formGrid;
                Fix32 half = new Fix32(500, 1000);
                if (offset.x > half)
                {
                    grid = toGrid;
                    offset.x = offset.x - 1;
                }
                else if(offset.x < - half)
                {
                    grid = toGrid;
                    offset.x = 1 + offset.x;
                }
                if (offset.y > half)
                {
                    grid = toGrid;
                    offset.y = offset.y - 1;
                }
                else if (offset.y < -half)
                {
                    grid = toGrid;
                    offset.y = 1 + offset.y;
                }

                if (grid == toGrid && (offset.x * m_offset.x >= 0 && offset.y * m_offset.y >= 0))
                {
                    m_offset = FixVec2.zero;
                    m_curIndex++;
                    if (m_curIndex == m_pathGridList.Count - 1)
                    {
                        m_isStart = false;
                    }
                }

                m_offset = m_offset + deltaPos;
                m_entity.GetComponent<LocationComponent>().SetPosition(grid, offset, true);
            }
            base.OnTick();
        }

        //private Fix32 speed = new Fix32(3, 10);
        //private GameObject targetObj;
        //private FixVec3 position;
        //private FixVec3 direction = FixVec3.forward;
        //private FixVec3 targetPos;
        //private bool isMoving;
        //private int speedRate = 1000;
        //private float preFrameTime = 0f;
        //private Vector3 preFramePos;
        //private EMoveType moveType;
        //private int curTime;
        //private int totalTime;

        //public FixVec3 Position
        //{
        //    get { return position; }
        //    set
        //    {
        //        position = value;
        //        targetObj.transform.position = value.ToVector3();
        //    }
        //}

        //public FixVec3 Direction
        //{
        //    get { return direction; }
        //    set
        //    {
        //        direction = value;
        //        targetObj.transform.forward = value.ToVector3();
        //    }
        //}

        //public bool IsMoving
        //{
        //    get { return isMoving; }
        //    protected set
        //    {
        //        if (isMoving == value)
        //        {
        //            return;
        //        }
        //        isMoving = value;
        //        if (value)
        //        {
        //            OnStartMove();
        //        }
        //        else
        //        {
        //            OnStopMove();
        //        }
        //    }
        //}

        //public void Init(GameObject targetObj)
        //{
        //    this.targetObj = targetObj;
        //}

        //public void StartMove()
        //{
        //    IsMoving = true;
        //}

        //public void StopMove()
        //{
        //    IsMoving = false;
        //}

        //public void SetSpeedRate(int rate)
        //{
        //    speedRate = rate;
        //}

        //public void MoveDir(FixVec3 dir)
        //{
        //    moveType = EMoveType.Direction;
        //    direction = dir;
        //    speed = new Fix32(3, 10);
        //    UpdatePreFrame();
        //    IsMoving = true;
        //}

        //public void MovePos(FixVec3 pos)
        //{
        //    moveType = EMoveType.Position;
        //    targetPos = pos;
        //    direction = (targetPos - position).normalized;
        //    speed = new Fix32(3, 10);
        //    CheckArrive();
        //    IsMoving = true;
        //}

        //public void MovePos(FixVec3 pos, int time)
        //{
        //    moveType = EMoveType.PositionConstTime;
        //    targetPos = pos;
        //    direction = (targetPos - position).normalized;
        //    curTime = 0;
        //    totalTime = time;
        //    speed = (pos - position).magnitude * FrameSyncDefine.DeltaTime / totalTime;
        //    UpdatePreFrame();
        //    IsMoving = true;
        //}

        //protected virtual void OnStartMove()
        //{

        //}

        //protected virtual void OnStopMove()
        //{

        //}

        //public void CheckArrive()
        //{
        //    if (FixVec3.Dot(targetPos - position, direction) <= 0)
        //    {
        //        position = targetPos;
        //        IsMoving = false;
        //    }
        //    else
        //    {
        //        IsMoving = true;
        //    }
        //}

        //public void UnityUpdate()
        //{
        //    if (IsMoving)
        //    {
        //        targetObj.transform.position = preFramePos + direction.ToVector3() * ((float)speed / FrameSyncDefine.TimeInterval) * (Time.time - preFrameTime);
        //        targetObj.transform.forward = direction.ToVector3();
        //    }
        //}

        //public void FrameUpdate()
        //{
        //    if (IsMoving)
        //    {
        //        position = position + direction * speed;
        //        UpdatePreFrame();
        //        if (moveType == EMoveType.Position)
        //        {
        //            CheckArrive();
        //        }
        //        if (moveType == EMoveType.PositionConstTime)
        //        {
        //            curTime += FrameSyncDefine.DeltaTime;
        //            if (curTime >= totalTime)
        //            {
        //                Position = targetPos;
        //                StopMove();
        //            }
        //        }
        //    }
        //}

        //private void UpdatePreFrame()
        //{
        //    preFrameTime = Time.time;
        //    preFramePos = position.ToVector3();
        //}

        public enum EMoveType
        {
            Direction,
            Position,
            Target,
            TargetConstTime,
            PositionConstTime,
        }
    }
}
