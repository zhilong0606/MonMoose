namespace MonMoose.Logic.Battle
{
    public class StopMoveCommand : FrameCommand
    {
        public StopMoveCommand()
        {
            commandType = EFrameCommandType.StopMove;
        }

        public override void Excute()
        {
            //Actor actor = BattlePlayerManager.instance.GetPlayer(playerID).selectedActor;
            //actor.moveComponent.StopMove();
            //actor.animationComponent.Stop("move");
        }
    }
}
