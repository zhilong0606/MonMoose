using UnityEngine;

namespace MonMoose.Logic
{
    public class BattleGridView : MonoBehaviour
    {
        [SerializeField] private Grid2D m_position;

        public Grid2D position
        {
            get { return m_position; }
            set { m_position = value; }
        }
    }
}
