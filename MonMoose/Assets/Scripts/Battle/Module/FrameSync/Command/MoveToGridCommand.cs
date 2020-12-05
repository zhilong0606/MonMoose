namespace MonMoose.Battle
{
    public partial class MoveToGridCommand : FrameCommand
    {
        public override void Excute(int playerId)
        {
            Entity entity = m_battleInstance.GetEntity(entityId);
            if (entity != null)
            {
                MoveComponent moveComponent = entity.GetComponent<MoveComponent>();
                if (moveComponent != null)
                {
                    //moveComponent.MoveToGrid(gridPos);
                }
            }
        }
    }
}
