using MonMoose.Battle;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic.Battle
{
    public class BattleGridView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private BattleGridConfig m_config;
        private Material m_mat;
        private bool m_canEmbattle;

        public GridPosition gridPosition
        {
            get { return m_config.gridPosition; }
        }

        public bool canEmbattle
        {
            get { return m_canEmbattle; }
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

        public void SetCanEmbattle(bool flag)
        {
            m_canEmbattle = flag;
            m_mat.color = Color.red;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            InputManager.instance.HandleClick(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            InputManager.instance.HandleDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputManager.instance.HandleUp(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InputManager.instance.HandleExit(eventData);
        }
    }
}