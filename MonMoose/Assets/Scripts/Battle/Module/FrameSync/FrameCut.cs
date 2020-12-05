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

        public void Execute()
        {
            foreach (FrameCommandUnion union in m_unionList)
            {
                union.Execute();
            }
        }

        public void AddCommand(int playerId, FrameCommand cmd)
        {
            FrameCommandUnion union = GetUnion(playerId);
            if (union == null)
            {
                union = m_battleInstance.FetchPoolObj<FrameCommandUnion>(this);
                m_unionList.Add(union);
                m_unionList.Sort(SortUnion);
            }
            union.AddCommand(cmd);
        }

        private FrameCommandUnion GetUnion(int playerId)
        {
            foreach (FrameCommandUnion union in m_unionList)
            {
                if (union.playerId == playerId)
                {
                    return union;
                }
            }
            return null;
        }

        private int SortUnion(FrameCommandUnion u1, FrameCommandUnion u2)
        {
            return u1.playerId.CompareTo(u2.playerId);
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
                    FrameCommandUnion union = m_battleInstance.FetchPoolObj<FrameCommandUnion>(this);
                    union.Deserialize(buffer, ref offset);
                    m_unionList[index - (int)ESerializeIndex.UnionStart] = union;
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
