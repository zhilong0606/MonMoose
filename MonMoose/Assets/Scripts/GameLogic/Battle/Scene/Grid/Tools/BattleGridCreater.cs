using MonMoose.Battle;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleGridCreater : MonoBehaviour
    {
        [SerializeField] private GameObject m_prefab;
        [SerializeField] private int m_width = 10;
        [SerializeField] private int m_height = 5;
        [SerializeField] private float m_spacing = 1f;

        public void Create()
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                Transform trans = transform.GetChild(i);
                if (trans.gameObject != m_prefab)
                {
                    DestroyImmediate(trans.gameObject);
                }
            }
            BattleGridConfig[,] configs = new BattleGridConfig[m_width, m_height];
            m_prefab.SetActive(true);
            for (int i = 0; i < m_width; ++i)
            {
                for (int j = 0; j < m_height; ++j)
                {
                    GameObject go = GameObject.Instantiate(m_prefab, transform);
                    BattleGridConfig config = go.AddComponent<BattleGridConfig>();
                    config.gridPosition = new GridPosition(i, j);
                    config.transform.position = CalcPosition(i, j);
                    configs[i, j] = config;
                }
            }
            m_prefab.SetActive(false);
        }

        private Vector3 CalcPosition(int x, int y)
        {
            float realWidth = m_width * m_spacing;
            float realHeight = m_height * m_spacing;
            Vector3 pos = new Vector3();
            pos.x = (0.5f + x) * m_spacing - realWidth / 2f;
            pos.z = (0.5f + y) * m_spacing - realHeight / 2f;
            return pos;
        }
    }
}
