using System;
using System.Collections.Generic;

namespace Structure
{
    public class EnumStructureInfo : MemberedStrucureInfo<EnumMemberInfo>
    {
        public sealed override EStructureType structureType { get { return EStructureType.Enum; } }

        public EnumStructureInfo(string name) : base(name)
        {
        }

        public bool CanAddMember(int memberIndex, string memberName)
        {
            EnumMemberInfo memberInfo = GetMember(memberName);
            if (memberInfo != null)
            {
                if (memberInfo.index != memberIndex)
                {
                    Debug.LogError(StaticString.SameEnumNameFormat, name, memberName);
                }
                return false;
            }
            memberInfo = GetMember(memberIndex);
            if (memberInfo != null)
            {
                if (memberInfo.name != memberName)
                {
                    Debug.LogError(StaticString.SameEnumIndexFormat, name, memberIndex);
                }
                return false;
            }
            return true;
        }

        public override bool CanAddMember(EnumMemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                return CanAddMember(memberInfo.index, memberInfo.name);
            }
            return false;
        }

        public EnumMemberInfo GetMember(int memberIndex)
        {
            foreach (EnumMemberInfo memberInfo in m_memberList)
            {
                if (memberInfo.index == memberIndex)
                {
                    return memberInfo;
                }
            }
            return null;
        }

        public EnumMemberInfo AddMember(int memberIndex, string memberName)
        {
            if (CanAddMember(memberIndex, memberName))
            {
                EnumMemberInfo memberInfo = new EnumMemberInfo(memberName, memberIndex);
                AddMember(memberInfo);
                return memberInfo;
            }
            return null;
        }
    }
}
