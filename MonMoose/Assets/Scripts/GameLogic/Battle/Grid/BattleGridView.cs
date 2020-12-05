using MonMoose.Battle;
using MonMoose.Logic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonMoose.GameLogic.Battle
{
    public class BattleGridView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private GridPosition m_gridPosition;

        public GridPosition gridPosition
        {
            get { return m_gridPosition; }
            set { m_gridPosition = value; }
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
