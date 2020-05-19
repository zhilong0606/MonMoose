using UnityEngine;

public class Actor : Entity
{
    protected ActorInfo m_actorInfo;
    public ECampType camp;

    public SkillModule skillModule = new SkillModule();
    public ActorMoveModule moveModule = new ActorMoveModule();
    public AnimationModule animationModule = new AnimationModule();
    public AttributeModule attributeModule = new AttributeModule();

    public void Init(int actorID)
    {
        //actorInfo = GameDataManager.instance.actorInfoDic[actorID];
        //skillModule.Init(this);
        //moveModule.Init(this);
        //attributeModule.Init(this);
        //animationModule.Init(GetComponent<Animation>(), actorInfo.animationIDs);
        //animationModule.Play("idle");
    }

    public void Born(SpawnRegion region)
    {
        moveModule.Position = region.Position;
    }

    private void Update()
    {
        moveModule.UnityUpdate();
    }

    public void UpdateLogic()
    {
        skillModule.FrameUpdate();
        moveModule.FrameUpdate();
        animationModule.FrameUpdate();
    }
}
