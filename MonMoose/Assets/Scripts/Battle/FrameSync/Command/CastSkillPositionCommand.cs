namespace MonMoose.Battle
{
    public class CastSkillPositionCommand : FrameCommand
    {
        public short slotType;
        //public FixVec3 position;

        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.CastSkillPosition; }
        }

        //public override void Serialize(out byte[] buffer)
        //{
        //    buffer = new byte[10];
        //    int offset = 0;
        //    //ByteBufferUtility.WriteShort(ref buffer, ref offset, slotType);
        //    //ByteBufferUtility.WriteInt(ref buffer, ref offset, position.x.raw);
        //    //ByteBufferUtility.WriteInt(ref buffer, ref offset, position.z.raw);
        //}

        //public override void Deserialise(ref byte[] buffer, ref int offset)
        //{
        //    //slotType = ByteBufferUtility.ReadShort(ref buffer, ref offset);
        //    //int x = ByteBufferUtility.ReadInt(ref buffer, ref offset);
        //    //int z = ByteBufferUtility.ReadInt(ref buffer, ref offset);
        //    //position = new FixVec3(Fix32.Raw(x), Fix32.zero, Fix32.Raw(z));
        //}

        //public override void Excute()
        //{
        //    //Debug.LogError("CastSkillPositionCommand Skill_" + slotType + ": " + position);
        //    //Player player = PlayerManager.instance.GetPlayer(playerId);
        //    //player.selectedActor.skillModule.skillSlots[slotType].CastSkillPosition(position);
        //}
    }
}
