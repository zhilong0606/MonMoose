using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameMsgObjWrap<T> : FrameMsgObj where T : FrameMsgObj
    {
        protected const int usedBitFlagCount = (int)ESerializeIndex.Max;

        protected T m_msgObj;

        public void Set(T msgObj)
        {
            m_msgObj = msgObj;
        }
        
        public override bool isBitFlagConst
        {
            get { return true; }
        }

        protected override byte GetBitFlagCount()
        {
            return usedBitFlagCount;
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.MsgObj:
                    return m_msgObj != null;
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.MsgObj:
                    return m_msgObj.GetByteBufferLength();
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.MsgObj:
                    m_msgObj.Serialize(buffer, ref offset);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.MsgObj:
                    m_msgObj.Deserialize(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            MsgObj,

            Max,
        }
    }
}