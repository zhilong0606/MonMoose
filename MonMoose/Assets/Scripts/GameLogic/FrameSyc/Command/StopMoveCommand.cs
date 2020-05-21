
namespace MonMoose.Logic
{
    public class StopMoveCommand : FrameCommand
    {
        public StopMoveCommand()
        {
            commandType = EFrameCommandType.StopMove;
        }

        public override void Excute()
        {
            Actor actor = PlayerManager.instance.GetPlayer(playerID).selectedActor;
            //actor.moveComponent.StopMove();
            //actor.animationComponent.Stop("move");
        }
    }
}
