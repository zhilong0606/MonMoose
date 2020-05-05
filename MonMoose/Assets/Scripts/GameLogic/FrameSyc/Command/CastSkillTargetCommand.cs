using UnityEngine;

public class CastSkillTargetCommand : FrameCommand
{
    public short slotType;
    public short targetID;

    public CastSkillTargetCommand()
    {
        commandType = EFrameCommandType.CastSkillTarget;
    }

    public override void Serialize(out byte[] buffer)
    {
        buffer = new byte[4];
        int offset = 0;
        ByteBufferUtility.WriteShort(ref buffer, ref offset, slotType);
        ByteBufferUtility.WriteShort(ref buffer, ref offset, targetID);
    }

    public override void Deserialise(ref byte[] buffer, ref int offset)
    {
        slotType = ByteBufferUtility.ReadShort(ref buffer, ref offset);
        targetID = ByteBufferUtility.ReadShort(ref buffer, ref offset);
    }

    public override void Excute()
    {
        Debug.LogError("Skill_" + slotType + ": " + targetID);
    }
}
