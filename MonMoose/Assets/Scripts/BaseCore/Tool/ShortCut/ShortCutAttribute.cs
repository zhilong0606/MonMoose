using System;
using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShortCutClassAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ShortCutFieldAttribute : Attribute
    {
        public bool isKeyField;

        public ShortCutFieldAttribute(bool isKeyField)
        {
            this.isKeyField = isKeyField;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ShortCutMethodAttribute : Attribute
    {
        public bool isPublic;

        public ShortCutMethodAttribute(bool isPublic)
        {
            this.isPublic = isPublic;
        }
    }
}
