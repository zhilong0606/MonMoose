using System;
using System.Collections.Generic;

namespace Structure
{
    public class ClassMemberInfo : BaseMemberInfo
    {
        protected BaseStructureInfo m_structureInfo;

        public BaseStructureInfo structureInfo
        {
            get { return m_structureInfo; }
        }

        public ClassMemberInfo(BaseStructureInfo structureInfo, string name) : base (name)
        {
            m_structureInfo = structureInfo;
        }
    }
}
