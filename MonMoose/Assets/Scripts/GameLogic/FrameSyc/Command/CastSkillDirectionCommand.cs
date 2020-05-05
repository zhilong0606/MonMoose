using UnityEngine;

public class CastSkillDirectionCommand : FrameCommand
{
    public short slotType;
    public FixVec3 direction;

    public CastSkillDirectionCommand()
    {
        commandType = EFrameCommandType.CastSkillDirection;
    }

    public override void Serialize(out byte[] buffer)
    {
        buffer = new byte[6];
        int offset = 0;
        ByteBufferUtility.WriteShort(ref buffer, ref offset, slotType);
        ByteBufferUtility.WriteInt(ref buffer, ref offset, direction.y.raw);
    }

    public override void Deserialise(ref byte[] buffer, ref int offset)
    {
        slotType = ByteBufferUtility.ReadShort(ref buffer, ref offset);
        int y = ByteBufferUtility.ReadInt(ref buffer, ref offset);
        direction = new FixVec3(0, Fix32.Raw(y), 0);
    }

    public override void Excute()
    {
        Debug.LogError("CastSkillDirectionCommand Skill_" + slotType + ": " + direction);
        Player player = PlayerManager.instance.GetPlayer(playerID);
        player.selectedActor.skillModule.skillSlots[slotType].CastSkillDirection(direction);
    }
}
