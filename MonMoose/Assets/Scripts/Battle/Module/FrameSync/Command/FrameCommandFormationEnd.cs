namespace MonMoose.Battle
{
    public partial class FrameCommandFormationEnd : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            BattleStage stage = m_battleInstance.GetCurStage();
            stage.ChangeState(EBattleStageState.Running);
            return true;
        }
    }
}
