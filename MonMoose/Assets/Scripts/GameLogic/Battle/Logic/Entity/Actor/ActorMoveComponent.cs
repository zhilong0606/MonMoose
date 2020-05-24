namespace MonMoose.Logic.Battle
{
    public class ActorMoveComponent : MoveComponent
    {
        private Actor actor;

        public void Init(Actor actor)
        {
            this.actor = actor;
        }

        //protected override void OnStartMove()
        //{
        //    //actor.animationComponent.Play("move");
        //}

        //protected override void OnStopMove()
        //{
        //    //actor.animationComponent.Stop("move");
        //}
    }
}
