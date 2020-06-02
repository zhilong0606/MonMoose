using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class FrameCut : BattleObj
    {
        private List<FrameCommandGroup> groupList = new List<FrameCommandGroup>();
        private int m_frameIndex = 0;

        public int frameIndex
        {
            get { return m_frameIndex; }
        }

        public void Deserialize()
        {

        }

        public void Excute()
        {
            for (int i = 0; i < groupList.Count; ++i)
            {
                groupList[i].Excute();
                //groupList[i].Release();
            }
            groupList.Clear();
        }

        /***For Local Use***/
        public void LocalDeserialize(int frameIndex, FrameCommandGroup group)
        {
            m_frameIndex = frameIndex;
            groupList.Add(group);
        }
    }
}
