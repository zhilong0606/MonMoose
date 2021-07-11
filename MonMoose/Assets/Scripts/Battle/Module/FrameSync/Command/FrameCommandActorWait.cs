namespace MonMoose.Battle
{
    public partial class FrameCommandActorWait : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            Entity entity = m_battleInstance.GetEntity(entityUid);
            m_battleInstance.GetCurStage().turn
            return true;
        }
    }
}
