using MonMoose.Logic.Battle;
using UnityEngine;

namespace MonMoose.Logic
{
    public class BattleGridView : MonoBehaviour
    {
        [SerializeField] private GridPosition m_position;

        public GridPosition position
        {
            get { return m_position; }
            set { m_position = value; }
        }
    }
}
