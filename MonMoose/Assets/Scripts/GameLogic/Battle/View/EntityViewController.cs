using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class EntityViewController : EntityViewControllerAbstract
    {
        protected EntityView m_view;
        protected bool m_isMoving;

        public override void PlayAnimation(string animName, float fadeTime)
        {
        }

        public override void SetPosition(BattleGrid grid, DcmVec2 offset, bool isTeleport)
        {
            m_view.SetPosition(grid, offset, isTeleport);
        }

        public override void SetForward(DcmVec2 forward)
        {
            m_view.SetForward(forward);
        }

        public override void InitView()
        {
            ActorInfoComponent infoComponent = m_owner.GetComponent<ActorInfoComponent>();
            string prefabPath = infoComponent.actorStaticInfo.PrefabPath;
            GameObject prefab = ResourceManager.instance.GetPrefab(prefabPath);
            GameObject actorRoot = BattleManager.instance.actorRoot;
            GameObject go = GameObject.Instantiate(prefab, actorRoot.transform);
            m_view = go.AddComponent<ActorView>();
            m_view.Init();
        }

        public override void UnInitView()
        {
            m_view = null;
        }

        public override void StartMove()
        {
            m_isMoving = true;
        }

        public override void StopMove()
        {
            m_isMoving = false;
        }
    }
}
