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

        protected abstract string prefabPath { get; }

        public override void SetPosition(MonMoose.Logic.Battle.Grid grid, FixVec2 offset, bool isTeleport)
        {
            m_obj.transform.position = BattleGridManager.instance.GetWorldPosition(grid.gridPosition, new Vector2((float)offset.x, (float)offset.y));
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
    }
}
