namespace MonMoose.Battle
{
    public partial class FrameCommandFormationtEmbattle : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            EntityInitData entityInitData = new EntityInitData();
            entityInitData.id = actorRid;
            entityInitData.level = 1;
            entityInitData.pos = new GridPosition(posX, posY);

            BattleFactory.CreateEntity(m_battleInstance, entityInitData, actorRid);
            m_battleInstance.eventListener.FormationEmbattle(actorRid, posX, posY);
            return true;
        }
    }
}
