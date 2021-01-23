namespace MonMoose.Battle
{
    public partial class FrameCommandFormationEnd : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            m_battleInstance.eventListener.FormationEnd();
            return true;
        }
    }
}
