using System.Collections.Generic;

namespace MonMoose.Logic
{
    public class Schedule
    {
        private SkillUseContext context;
        private List<ScheduleClip> motionList = new List<ScheduleClip>();

        private int totalTime;
        private int curTime;
        private bool isStart;

        public bool IsStart
        {
            get { return isStart; }
        }

        public void Start(SkillUseContext context)
        {
            isStart = true;
            curTime = 0;
            this.context = context;
        }

        public void AddMotion(ScheduleClip scheduleClip)
        {
            motionList.Add(scheduleClip);
        }

        public void Execute()
        {
            int newTime = curTime + FrameSyncDefine.DeltaTime;
            for (int i = 0; i < motionList.Count; ++i)
            {
                ScheduleClip scheduleClip = motionList[i];
                if (scheduleClip.startTime >= curTime && scheduleClip.startTime < newTime)
                {
                    scheduleClip.OnEnter(context);
                }
            }

            for (int i = 0; i < motionList.Count; ++i)
            {
                ScheduleClip scheduleClip = motionList[i];
                if (scheduleClip.IsStart)
                {
                    scheduleClip.OnExecute();
                }
            }

            for (int i = 0; i < motionList.Count; ++i)
            {
                ScheduleClip scheduleClip = motionList[i];
                if (scheduleClip.startTime + scheduleClip.totalTime >= curTime && scheduleClip.startTime + scheduleClip.totalTime < newTime)
                {
                    scheduleClip.OnExit();
                }
            }
            curTime = newTime;
            if (curTime >= totalTime)
            {
                isStart = false;
            }
        }

        public void Load(System.Xml.XmlElement element)
        {
            totalTime = int.Parse(element.Attributes["totalTime"].Value);
        }
    }
}
