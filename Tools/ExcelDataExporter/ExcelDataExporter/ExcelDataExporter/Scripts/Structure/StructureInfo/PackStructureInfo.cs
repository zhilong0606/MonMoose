using System;
using System.Collections.Generic;

namespace Structure
{
    public class PackStructureInfo : MemberedStrucureInfo<ClassMemberInfo>
    {
        private ClassStructureInfo m_classStructureInfo;

        public sealed override EStructureType structureType { get { return EStructureType.Pack; } }

        public ClassStructureInfo classStructureInfo
        {
            get { return m_classStructureInfo; }
        }

        public PackStructureInfo(ClassStructureInfo structureInfo) : base(structureInfo.name + "List")
        {
            m_classStructureInfo = structureInfo;
            ListStructureInfo listStructureInfo = new ListStructureInfo(structureInfo);
            StructureManager.Instance.AddStructureInfo(listStructureInfo);
            ClassMemberInfo memberInfo = new ClassMemberInfo(listStructureInfo, "list");
            AddMember(memberInfo);
        }
    }
}