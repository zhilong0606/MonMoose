namespace MonMoose.Battle
{
    public partial class FrameCommandMoveToGrid : FrameCommand
    {
        public override bool Execute(int playerId)
        {
            Player player = m_battleInstance.GetPlayer(playerId);
            if (player == null)
            {
                return false;
            }
            Entity entity = m_battleInstance.GetEntity(entityUid);
            if (entity == null)
            {
                return false;
            }
            if (entity.team == null)
            {
                return false;
            }
            if (entity.team != player.team)
            {
                return false;
            }
            if (!m_battleInstance.CheckActiveController(player.controller))
            {
                return false;
            }
            MoveComponent moveComponent = entity.GetComponent<MoveComponent>();
            if (moveComponent == null)
            {
                return false;
            }
            moveComponent.MoveToGrid(new GridPosition(gridX, gridY));
            return true;
        }
    }
}
