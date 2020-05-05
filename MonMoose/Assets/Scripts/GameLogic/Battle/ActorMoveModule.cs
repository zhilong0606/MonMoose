public class ActorMoveModule : MoveModule
{
    private Actor actor;

    public void Init(Actor actor)
    {
        this.actor = actor;
        base.Init(actor.gameObject);
    }

    protected override void OnStartMove()
    {
        actor.animationModule.Play("move");
    }

    protected override void OnStopMove()
    {
        actor.animationModule.Stop("move");
    }
}
