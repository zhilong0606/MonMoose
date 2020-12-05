using UnityEngine;

namespace MonMoose.Core
{
    public class UIWindowConfig : MonoBehaviour, IUIWindowHolder
    {
        [SerializeField] private bool m_isImmortal = false;
        [SerializeField] private UIWindowCanvas m_bgCanvas;
        [SerializeField] private UIWindowCanvas m_mainCanvas;
        [SerializeField] private UIModelDisplay m_modelDisplay;
        [SerializeField] private EWindowPriority m_priority;
        [SerializeField] private bool m_activeToTop;
        [SerializeField] private bool m_needStack;

        private UIWindow m_window;
        private int m_sortingOrder;

        public UIWindow window
        {
            get { return m_window; }
        }

        public bool IsImmortal
        {
            get { return m_isImmortal; }
        }

        public UIWindowCanvas BgCanvas
        {
            get { return m_bgCanvas; }
        }

        public UIWindowCanvas MainCanvas
        {
            get { return m_mainCanvas; }
        }

        public UIModelDisplay ModelCamera
        {
            get { return m_modelDisplay; }
        }

        public EWindowPriority Priority
        {
            get { return m_priority; }
        }

        public bool ActiveToTop
        {
            get { return m_activeToTop; }
        }

        public bool NeedStack
        {
            get { return m_needStack; }
        }

        public bool IsCameraEmpty
        {
            get { return m_modelDisplay == null || m_modelDisplay.Camera == null; }
        }

        public int SortingOrder
        {
            get { return m_sortingOrder; }
            set
            {
                m_sortingOrder = value;
                if (m_bgCanvas != null)
                {
                    m_bgCanvas.sortingOrder = value;
                }
                if (m_mainCanvas != null)
                {
                    m_mainCanvas.sortingOrder = value;
                }
            }
        }

        public void Initialize(UIWindow window)
        {
            m_window = window;
            if (m_bgCanvas != null)
            {
                m_bgCanvas.Initialize(m_window);
            }
            if (m_mainCanvas != null)
            {
                m_mainCanvas.Initialize(m_window);
            }
            if (m_modelDisplay != null)
            {
                m_modelDisplay.Initialize(m_window);
            }
        }

        public void UpdateCameraAndDepth(ref Camera lastCamera, ref int depth)
        {
            SetCanvasDepth(BgCanvas, ref lastCamera, ref depth);
            SetModelDepth(ModelCamera, ref lastCamera, ref depth);
            SetCanvasDepth(MainCanvas, ref lastCamera, ref depth);
        }

        private void SetCanvasDepth(UIWindowCanvas canvas, ref Camera lastCamera, ref int depth)
        {
            if (canvas != null && canvas.isActiveAndEnabled)
            {
                if (lastCamera == null)
                {
                    Camera uiCamera = UIWindowManager.instance.NewCamera();
                    canvas.camera = uiCamera;
                    lastCamera = uiCamera;
                    uiCamera.depth = depth++;
                }
                else
                {
                    canvas.camera = lastCamera;
                }
            }
        }

        private void SetModelDepth(UIModelDisplay display, ref Camera lastCamera, ref int depth)
        {
            if (display != null)
            {
                Camera camera = display.Camera;
                if (camera != null && camera.gameObject.activeInHierarchy)
                {
                    lastCamera = null;
                    camera.depth = depth++;
                }
            }
        }

        public UIWindowCanvas GetCanvas(EWindowCanvasType canvasType)
        {
            switch (canvasType)
            {
                case EWindowCanvasType.Bg:
                    return BgCanvas;
                case EWindowCanvasType.Main:
                    return MainCanvas;
            }
            return null;
        }
    }
}
