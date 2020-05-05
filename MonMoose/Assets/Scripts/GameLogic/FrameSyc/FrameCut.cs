using System.Collections.Generic;
using MonMoose.Core;

public class FrameCut : ClassPoolObj
{
    private List<FrameCommandGroup> groupList = new List<FrameCommandGroup>();
    public int frameIndex = 0;

    public void Deserialize()
    {
        
    }

    public void Excute()
    {
        for (int i = 0; i < groupList.Count; ++i)
        {
            groupList[i].Excute();
            groupList[i].Release();
        }
        groupList.Clear();
    }

    /***For Local Use***/
    public void LocalDeserialize(int frameIndex, FrameCommandGroup group)
    {
        this.frameIndex = frameIndex;
        groupList.Add(group);
    }
}
