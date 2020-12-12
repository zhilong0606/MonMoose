using MonMoose.Battle;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleGridView : MonoBehaviour
    {
        private BattleGridConfig m_config;
        private Material m_mat;

        public GridPosition gridPosition
        {
            get { return m_config.gridPosition; }
        }

        public void Init()
        {
            m_config = GetComponent<BattleGridConfig>();
            m_mat = GetComponent<MeshRenderer>().sharedMaterial;
            m_mat = new Material(m_mat);
            GetComponent<MeshRenderer>().sharedMaterial = m_mat;
        }

        public void SetColor(Color c)
        {
            m_mat.color = c;
        }
    }
}