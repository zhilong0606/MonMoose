using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleScene
    {
        private GameObject m_actorRoot;
        private GameObject m_cameraRoot;
        private Camera m_camera;
        private List<BattleGridView> m_gridList = new List<BattleGridView>();

        public GameObject actorRoot
        {
            get { return m_actorRoot; }
        }

        public void Init()
        {
            m_actorRoot = new GameObject("ActorRoot");
            Object.DontDestroyOnLoad(m_actorRoot);
            GameObject prefab = ResourceManager.instance.GetPrefab(StaticDataShortCut.GetPrefabPath(StaticData.EPrefabPathId.BattleCamera));
            m_cameraRoot = GameObject.Instantiate(prefab);
            m_camera = m_cameraRoot.GetComponentInChildren<Camera>();
            Object.DontDestroyOnLoad(m_cameraRoot);
        }

        public void AddGridView(BattleGridView grid)
        {
            m_gridList.Add(grid);
        }

        public BattleGridView GetGridView(GridPosition pos)
        {
            for (int i = 0; i < m_gridList.Count; ++i)
            {
                BattleGridView gridView = m_gridList[i];
                if (gridView.gridPosition == pos)
                {
                    return gridView;
                }
            }
            return null;
        }

        public Vector3 GetGridWorldPosition(GridPosition gridPos, DcmVec2 offset)
        {
            BattleGridView gridView = GetGridView(gridPos);
            if (gridView != null)
            {
                return gridView.transform.position + new Vector3((float)offset.x, 0f, (float)offset.y);
            }
            return Vector3.zero;
        }

        public BattleGridView GetGridViewByScreenPosition(Vector2 screenPos)
        {
            Ray ray = m_camera.ScreenPointToRay(screenPos);
            BattleGridView gridView = null;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, LayerUtility.GetLayerMask(ELayerMaskType.Grid)))
            {
                gridView = hit.transform.GetComponent<BattleGridView>();
            }
            return gridView;
        }
    }
}
