using UnityEngine;

namespace MonMoose.Core
{
    public class UIModelDisplay : MonoBehaviour, IUIWindowHolder
    {
        [SerializeField] private Camera m_camera;

        private UIWindow m_window;

        public UIWindow window
        {
            get { return m_window; }
        }

        public Camera Camera
        {
            get { return m_camera; }
        }

        public void Initialize(UIWindow window)
        {
            m_window = window;
        }
    }
}
