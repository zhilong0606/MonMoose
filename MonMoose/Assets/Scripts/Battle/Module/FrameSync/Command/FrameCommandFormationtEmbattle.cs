namespace MonMoose.Battle
{
    public partial class FrameCommandFormationtEmbattle : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            EntityInitData entityInitData = new EntityInitData();
            entityInitData.rid = entityRid;
            entityInitData.level = 1;
            entityInitData.pos = new GridPosition(posX, posY);

            m_battleInstance.AddFormationActor(entityInitData);
            m_battleInstance.eventListener.FormationEmbattle(entityRid, posX, posY);
            return true;
        }
    }
}
