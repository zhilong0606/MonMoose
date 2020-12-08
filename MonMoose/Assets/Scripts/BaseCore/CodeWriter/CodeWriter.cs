using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MonMoose.Core
{
    public class CodeWriter
    {
        private StringBuilder m_stringBuilder = new StringBuilder();
        private int m_tabCount = 0;
        private string m_tabStr = "    ";

        public void StartTab()
        {
            m_tabCount++;
        }

        public void EndTab()
        {
            m_tabCount--;
        }

        public void StartBlock()
        {
            AppendLine("{");
            StartTab();
        }

        public void EndBlock()
        {
            EndTab();
            AppendLine("}");
        }

        public CodeWriter StartLine(string str = null)
        {
            for (int i = 0; i < m_tabCount; ++i)
            {
                m_stringBuilder.Append(m_tabStr);
            }
            if (!string.IsNullOrEmpty(str))
            {
                m_stringBuilder.Append(str);
            }
            return this;
        }

        public void StartLine(string str, params string[] objs)
        {
            StartLine(string.Format(str, objs));
        }

        public CodeWriter Append(string str)
        {
            m_stringBuilder.Append(str);
            return this;
        }

        public void Append(string str, params string[] objs)
        {
            Append(string.Format(str, objs));
        }

        public CodeWriter AppendSpace()
        {
            m_stringBuilder.Append(" ");
            return this;
        }

        public CodeWriter EndLine()
        {
            m_stringBuilder.Append("\r\n");
            return this;
        }

        public void AppendLine(string str = null)
        {
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < m_tabCount; ++i)
                {
                    m_stringBuilder.Append(m_tabStr);
                }
                m_stringBuilder.Append(str);
            }
            m_stringBuilder.Append("\r\n");
        }

        public void AppendEmptyLine()
        {
            AppendLine();
        }

        public void AppendLine(string str, params object[] objs)
        {
            AppendLine(string.Format(str, objs));
        }

        public override string ToString()
        {
            return m_stringBuilder.ToString();
        }

        public void WriteFile(DirectoryInfo dirInfo, string fileName)
        {
            using (StreamWriter file = new StreamWriter(dirInfo.FullName + "\\" + fileName, false, Encoding.GetEncoding("GB2312")))
            {
                file.Write(m_stringBuilder.ToString());
            }
        }

        public void WriteFile(string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName, false, Encoding.GetEncoding("GB2312")))
            {
                file.Write(m_stringBuilder.ToString());
            }
        }
    }
}
