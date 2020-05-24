using System.Collections.Generic;

namespace MonMoose.Logic.Battle
{
    public class FrameSyncManager /*: Singleton<FrameSyncManager>*/
    {
        private FrameCommandGroup curGroup;
        private List<FrameCut> cutList = new List<FrameCut>();
        private int curFrameIndex;
        private float curTime = 0f;
        private bool isStart = false;

        public void Start()
        {
            isStart = true;
            curTime = 0f;
            curFrameIndex = 0;
        }

        public void Stop()
        {
            if (curGroup != null)
            {
                //curGroup.Release();
                curGroup = null;
            }
            for (int i = 0; i < cutList.Count; ++i)
            {
                //cutList[i].Release();
            }
            cutList.Clear();
            isStart = false;
        }

        public void Resume()
        {
            isStart = true;
        }

        public void Pause()
        {
            isStart = false;
        }

        public void Tick(float deltaTime)
        {
            if (!isStart)
            {
                return;
            }
            curTime += deltaTime;
            while (curTime > FrameSyncDefine.TimeInterval)
            {
                curTime -= FrameSyncDefine.TimeInterval;
                if (curGroup != null)
                {
                    if (FrameSyncDefine.IsLocalSync)
                    {
                        //FrameCut cut = ClassPoolManager.instance.Fetch<FrameCut>();
                        //cut.LocalDeserialize(curFrameIndex, curGroup);
                        //cutList.Add(cut);
                    }
                    //Will DO::Send CurGroup
                    /*************/
                    /****SPACE****/
                    /*************/
                    curGroup = null;
                }
                //EventManager.instance.Broadcast((int)EventID.Frame_Tick);
            }
            for (int i = 0; i < cutList.Count; ++i)
            {
                if (cutList[0].frameIndex == curFrameIndex)
                {
                    cutList[0].Excute();
                    //cutList[0].Release();
                    cutList.RemoveAt(0);
                    curFrameIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        public void Send(FrameCommand command)
        {
            if (curGroup == null)
            {
                //curGroup = ClassPoolManager.instance.Fetch<FrameCommandGroup>();
            }
            curGroup.AddCommand(command);
        }
    }
}