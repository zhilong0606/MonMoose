using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.Logic
{
    public abstract class EntityObjView : EntityView
    {
        protected GameObject m_obj;
        protected StaticLerpVec3 posLerp = new StaticLerpVec3();
        protected bool m_isMoving;

        protected abstract GameObject rotateRoot { get; }

        protected abstract string prefabPath { get; }

        public override void SetPosition(MonMoose.Logic.Battle.Grid grid, FixVec2 offset, bool isTeleport)
        {
            Vector3 worldPos = BattleGridManager.instance.GetWorldPosition(grid.gridPosition, offset);
            if (isTeleport)
            {
                m_obj.transform.position = worldPos;
                posLerp.Stop();
            }
            else
            {
                posLerp.Ready(m_obj.transform.position, worldPos, 0.1f);
                posLerp.Start();
            }
        }

        public override void SetForward(FixVec2 forward)
        {
            base.SetForward(forward);
            rotateRoot.transform.forward = new Vector3((float)forward.x, 0f, (float)forward.y);
        }

        public override void CreateView()
        {
            GameObject prefab = ResourceManager.instance.GetPrefab(prefabPath);
            m_obj = GameObject.Instantiate(prefab);
        }

        public override void DestroyView()
        {
            GameObject.Destroy(m_obj);
            m_obj = null;
        }

        public override void StartMove()
        {
            base.StartMove();
            m_isMoving = true;
        }

        public override void StopMove()
        {
            base.StopMove();
        }

        public override void Tick(float deltaTime)
        {
            if (posLerp.isStart)
            {
                posLerp.Tick(deltaTime);
                m_obj.transform.position = posLerp.curValue;
            }
            //if (m_isMoving)
            //{
            //    LocationComponent locationComponent = m_entity.GetComponent<LocationComponent>();
            //    Vector3 targetPos = BattleGridManager.instance.GetWorldPosition(locationComponent.locateGrid.gridPosition, locationComponent.offset);
            //    m_obj.transform.position = Vector3.Lerp(m_obj.transform.position, targetPos, 0.2f);
            //    if ((m_obj.transform.position - targetPos).sqrMagnitude < 0.001f)
            //    {
            //        m_obj.transform.position = targetPos;
            //        m_isMoving = false;
            //    }
            //}
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
    }
}
