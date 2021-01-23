namespace MonMoose.Battle
{
    public partial class FrameCommandFormationtEmbattle : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            m_battleInstance.eventListener.FormationEmbattle(actorId, posX, posY);
            return true;
        }
    }
}
