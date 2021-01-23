namespace MonMoose.Battle
{
    public partial class FrameCommandFormationExchange : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            m_battleInstance.eventListener.FormationExchange(posX1, posY1, posX2, posY2);
            return true;
        }
    }
}
