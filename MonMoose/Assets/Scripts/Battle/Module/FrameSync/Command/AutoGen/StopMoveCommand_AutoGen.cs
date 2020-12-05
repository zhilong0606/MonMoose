namespace MonMoose.Battle
{
    public partial class StopMoveCommand
    {

        public override EFrameCommandType commandType
        {
            get { return EFrameCommandType.StopMove; }
        }

        protected override byte GetBitFlagCount()
        {
            return (int)ESerializeIndex.Max;
        }

        protected override bool CheckValid(int index)
        {
            switch ((ESerializeIndex)index)
            {
            }
            return false;
        }

        protected override int GetSizeOf(int index)
        {
            switch ((ESerializeIndex)index)
            {
            }
            return 0;
        }

        protected override void SerializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
            }
        }

        protected override void DeserializeField(byte[] buffer, ref int offset, int index)
        {
            switch ((ESerializeIndex)index)
            {
            }
        }

        private enum ESerializeIndex
        {

            Max
        }
    }
}
