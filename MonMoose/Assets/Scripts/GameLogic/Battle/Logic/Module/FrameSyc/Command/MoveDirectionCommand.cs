namespace MonMoose.Logic.Battle
{
    public class MoveDirectionCommand : FrameCommand
    {
        public FixVec3 direction;

        public MoveDirectionCommand()
        {
            commandType = EFrameCommandType.MoveDirection;
        }

        public override void Serialize(out byte[] buffer)
        {
            buffer = new byte[2];
            int offset = 0;
            //ByteBufferUtility.WriteInt(ref buffer, ref offset, direction.y.raw);
        }

        public override void Deserialise(ref byte[] buffer, ref int offset)
        {
            //int y = ByteBufferUtility.ReadInt(ref buffer, ref offset);
            //direction = new FixVec3(0, Fix32.Raw(y), 0);
        }

        public override void Excute()
        {
            //Actor actor = PlayerManager.instance.GetPlayer(playerId).selectedActor;
            //actor.moveComponent.MoveDir(direction);
            //actor.animationComponent.Play("move");
        }
    }
}
