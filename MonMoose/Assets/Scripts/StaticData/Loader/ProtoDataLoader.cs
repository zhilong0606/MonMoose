using System;
using System.Collections.Generic;

namespace MonMoose.StaticData
{
    public class ProtoDataLoader<TItem, TList> : ProtoDataLoader
    {
        private List<TItem> m_toList;
        private Func<byte[], TList> m_funcOnParse;
        private Action<TList, List<TItem>> m_actionOnLoadEnd;

        public ProtoDataLoader(List<TItem> toList, Func<byte[], TList> funcOnParse, Action<TList, List<TItem>> actionOnLoadEnd)
        {
            m_toList = toList;
            m_funcOnParse = funcOnParse;
            m_actionOnLoadEnd = actionOnLoadEnd;
        }

        protected override void OnLoad(byte[] buffer)
        {
            TList fromList = m_funcOnParse(buffer);
            m_actionOnLoadEnd(fromList, m_toList);
        }
    }

    public abstract class ProtoDataLoader : StaticDataLoader
    {
    }
}
