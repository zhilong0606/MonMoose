using System.Xml;
using MonMoose.Core;

namespace MonMoose.Logic
{
    public class ScheduleClip : ClassPoolObj
    {
        protected SkillUseContext context;

        public int startTime;
        public int totalTime;
        public bool isStart = false;

        public bool IsStart
        {
            get { return isStart; }
        }

        public virtual void OnEnter(SkillUseContext context)
        {
            isStart = true;
            this.context = context;
        }

        public virtual void OnExecute()
        {

        }

        public virtual void OnExit()
        {
            isStart = true;
        }

        public void Stop()
        {
            isStart = false;
        }

        public virtual void Load(XmlElement element)
        {
            startTime = int.Parse(element.GetAttribute("startTime"));
            totalTime = int.Parse(element.GetAttribute("totalTime"));
        }
    }
}
