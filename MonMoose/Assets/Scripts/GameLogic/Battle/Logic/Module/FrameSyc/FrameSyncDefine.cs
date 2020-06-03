namespace MonMoose.Logic.Battle
{
    public enum EFrameCommandType
    {
        MoveToGrid,
        StopMove,
        CastSkillTarget,
        CastSkillPosition,
        CastSkillDirection,
        Count,
    }

    public class FrameSyncDefine
    {
        public const int Precision = 1000;
        public const int DeltaTime = 50;
        //public static Fix32 FixedTimeInterval = new Fix32(50, 1000);
        public const float TimeInterval = 0.05f;
        public const int CommandTypeCount = (int)EFrameCommandType.Count;
        public static bool IsLocalSync = true;
    }
}
