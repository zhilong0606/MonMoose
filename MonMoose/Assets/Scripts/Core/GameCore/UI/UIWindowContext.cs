using System;

namespace MonMoose.Core
{
    public class UIWindowContext
    {
        private string m_path;
        private Type m_type;

        public string Path
        {
            get { return m_path; }
        }

        public Type Type
        {
            get { return m_type; }
        }

        public UIWindowContext(string path, Type type)
        {
            m_path = path;
            m_type = type;
        }
    }
}
