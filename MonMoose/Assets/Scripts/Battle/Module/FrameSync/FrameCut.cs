using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class FrameCut : FrameMsgObj
    {
        public int frameIndex = 0;
        private List<FrameCommandUnion> m_unionList = new List<FrameCommandUnion>();

        public override bool isBitFlagConst
        {
            get { return false; }
        }

        public void Excute()
        {
            for (int i = 0; i < m_unionList.Count; ++i)
            {
                m_unionList[i].Excute();
            }
        }

        public void AddCmdGroup(FrameCommandUnion union)
        {
            m_unionList.Add(union);
        }

        protected override byte GetBitFlagCount()
        {
            return (byte)((int)ESerializeIndex.UnionStart + m_unionList.Count);
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.FrameIndex:
                    return frameIndex == default(int);
                default:
                    return m_unionList[index - (int)ESerializeIndex.UnionStart] != null;
            }
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.FrameIndex:
                    return sizeof(int);
                default:
                    return m_unionList[index - (int)ESerializeIndex.UnionStart].GetByteBufferLength();
            }
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.FrameIndex:
                    ByteBufferUtility.WriteInt(buffer, ref offset, frameIndex);
                    break;
                default:
                    m_unionList[index - (int)ESerializeIndex.UnionStart].Serialize(buffer, ref offset);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.FrameIndex:
                    frameIndex = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                default:
                    m_unionList[index - (int)ESerializeIndex.UnionStart].Deserialize(buffer, ref offset);
                    break;
            }
        }

        private enum ESerializeIndex
        {
            FrameIndex,
            UnionStart,
        }
    }
}
