using System;
using System.Collections.Generic;

namespace MonMoose.StaticData
{
    public class ProtoDataLoader<T, U> : ProtoDataLoader
    {
        private U m_formList;
        private List<T> m_toList;
        private Func<byte[], U> m_funcOnParse;
        private Action<U, List<T>> m_actionOnLoadEnd;

        public ProtoDataLoader(List<T> toList, Func<byte[], U> funcOnParse, Action<U, List<T>> actionOnLoadEnd)
        {
            m_toList = toList;
            m_funcOnParse = funcOnParse;
            m_actionOnLoadEnd = actionOnLoadEnd;
        }

        protected override void OnLoad(byte[] buffer)
        {
            m_formList = m_funcOnParse(buffer);
            m_actionOnLoadEnd(m_formList, m_toList);
        }
    }

    public abstract class ProtoDataLoader : StaticDataLoader
    {
    }
}
