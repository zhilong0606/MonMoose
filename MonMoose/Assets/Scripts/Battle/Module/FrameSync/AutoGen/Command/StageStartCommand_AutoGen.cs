namespace MonMoose.Battle
{
    public partial class StageStartCommand
    {
        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.StageStart; }
        }

        protected override byte GetBitFlagCount()
        {
            return 0;
        }

        protected override bool CheckValid(int index)
        {
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
        }

    }
}
