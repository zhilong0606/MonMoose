using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MonMoose.Core
{
    public class UIWindowCanvas : MonoBehaviour, IUIWindowHolder
    {
        private UIWindow m_window;
        private Canvas m_canvas;
        private BaseRaycaster m_raycaster;
        private CanvasScaler m_scaler;
        private UICamera m_camera;

        public UIWindow window
        {
            get { return m_window; }
        }

        public Canvas canvas
        {
            get { return m_canvas; }
        }

        public BaseRaycaster raycaster
        {
            get { return m_raycaster; }
        }

        public CanvasScaler scaler
        {
            get { return m_scaler; }
        }

        public UICamera uiCamera
        {
            get { return m_camera; }
            set
            {
                m_camera = value;
                m_canvas.worldCamera = value.camera;
            }
        }

        public int sortingOrder
        {
            get { return m_canvas.sortingOrder; }
            set { m_canvas.sortingOrder = value; }
        }

        public float scaleFactor
        {
            get { return m_canvas.scaleFactor; }
        }

        public RenderMode renderMode
        {
            get { return m_canvas.renderMode; }
            set { m_canvas.renderMode = value; }
        }

        public void Initialize(UIWindow window)
        {
            m_window = window;
            m_canvas = GetComponent<Canvas>();
            m_raycaster = GetComponent<BaseRaycaster>();
            m_scaler = GetComponent<CanvasScaler>();
        }
    }
}
