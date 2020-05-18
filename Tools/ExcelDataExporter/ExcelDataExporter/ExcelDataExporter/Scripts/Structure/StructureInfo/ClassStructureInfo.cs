﻿using System;
using System.Collections.Generic;

namespace Structure
{
    public class ClassStructureInfo : MemberedStrucureInfo<ClassMemberInfo>
    {
        public sealed override EStructureType structureType { get { return EStructureType.Class; } }
        
        public ClassStructureInfo(string name) : base(name) { }
        
        public bool CanAddMember(BaseStructureInfo structureInfo, string memberName)
        {
            ClassMemberInfo memberInfo = GetMember(memberName);
            if (memberInfo != null)
            {
                if (memberInfo.structureInfo != structureInfo)
                {
                    Debug.LogError(StaticString.SameClassMemberNameFormat, name, memberName);
                }
                return false;
            }
            return true;
        }

        public override bool CanAddMember(ClassMemberInfo memberInfo)
        {
            if (memberInfo != null)
            {
                return CanAddMember(memberInfo.structureInfo, memberInfo.name);
            }
            return false;
        }

        public ClassMemberInfo AddMember(BaseStructureInfo strcutureInfo, string memberName)
        {
            if (CanAddMember(strcutureInfo, memberName))
            {
                ClassMemberInfo memberInfo = new ClassMemberInfo(strcutureInfo, memberName);
                AddMember(memberInfo);
                return memberInfo;
            }
            return null;
        }
    }
}
