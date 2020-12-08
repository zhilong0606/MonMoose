using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public abstract class EntityObjView : EntityView
    {
        protected GameObject m_obj;
        protected StaticLerpVec3 posLerp = new StaticLerpVec3();
        protected bool m_isMoving;

        protected abstract GameObject rotateRoot { get; }

        protected abstract string prefabPath { get; }

        public override void SetPosition(BattleGrid grid, DcmVec2 offset, bool isTeleport)
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

        public override void SetForward(DcmVec2 forward)
        {
            base.SetForward(forward);
            rotateRoot.transform.forward = new Vector3((float)forward.x, 0f, (float)forward.y);
        }

        public override void CreateView()
        {
            GameObject prefab = ResourceManager.instance.GetPrefab(prefabPath);
            GameObject actorRoot = BattleManager.instance.actorRoot;
            m_obj = GameObject.Instantiate(prefab, actorRoot.transform);
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
        }
    }
}
