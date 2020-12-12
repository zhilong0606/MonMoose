namespace MonMoose.Battle
{
    public abstract class EntityViewControllerAbstract : BattleViewController<Entity>
    {
        public abstract void PlayAnimation(string animName, float fadeTime);
        public abstract void SetPosition(BattleGrid grid, DcmVec2 offset, bool isTeleport);
        public abstract void StartMove();
        public abstract void StopMove();
        public abstract void SetForward(DcmVec2 forward);
    }
}
