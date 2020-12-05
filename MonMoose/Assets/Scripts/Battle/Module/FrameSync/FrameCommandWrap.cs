using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Battle
{
    public class FrameCommandWrap : FrameMsgObjWrap<FrameCommand>
    {
        public int teamId;

        public override bool isBitFlagConst
        {
            get { return true; }
        }

        protected override byte GetBitFlagCount()
        {
            return (int)ESerializeIndex.Max;
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.TeamId:
                    return teamId == default(int);
                default:
                    return base.CheckValid(index);
            }
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.TeamId:
                    return sizeof(int);
                default:
                    return base.GetSizeOf(index);
            }
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.TeamId:
                    ByteBufferUtility.WriteInt(buffer, ref offset, teamId);
                    break;
                default:
                    base.GetSizeOf(index);
                    break;
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
                case ESerializeIndex.TeamId:
                    teamId = ByteBufferUtility.ReadInt(buffer, ref offset);
                    break;
                default:
                    base.GetSizeOf(index);
                    break;
            }
        }
        
        private enum ESerializeIndex
        {
            TeamId = usedBitFlagCount,

            Max
        }
    }
}
