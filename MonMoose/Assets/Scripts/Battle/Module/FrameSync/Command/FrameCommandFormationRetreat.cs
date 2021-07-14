namespace MonMoose.Battle
{
    public partial class FrameCommandFormationRetreat : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            m_battleInstance.RemoveFormationActor(entityUid);
            //m_battleInstance.eventListener.FormationRetreat(entityUid);
            return true;
        }
    }
}
