using MonMoose.Logic.Battle;
using UnityEngine;

namespace MonMoose.Logic
{
    public class BattleGridView : MonoBehaviour
    {
        [SerializeField] private GridPosition m_gridPosition;

        public GridPosition gridPosition
        {
            get { return m_gridPosition; }
            set { m_gridPosition = value; }
        }
    }
}
