using System.Xml;

namespace MonMoose.Logic.Battle
{
    public class ScheduleManager /*: Singleton<ScheduleManager>*/
    {
        private XmlDocument document = new XmlDocument();

        public Schedule Load(string path)
        {
            Schedule schedule = new Schedule();
            //Object resource = ResourceManager.instance.GetResource(path);
            //if (resource == null)
            //{
            //    return schedule;
            //}
            //document.LoadXml(resource.ToString());
            //XmlElement element = document.DocumentElement;
            //schedule.Load(element);
            //for (int i = 0; i < element.ChildNodes.Count; ++i)
            //{
            //    System.Type type = System.Type.GetType(element.ChildNodes[i].Name);
            //    if (type != null)
            //    {
            //        ScheduleClip scheduleClip = System.Activator.CreateInstance(type) as ScheduleClip;
            //        scheduleClip.Load(element.ChildNodes[i] as XmlElement);
            //        schedule.AddMotion(scheduleClip);
            //    }
            //}
            return schedule;
        }
    }
}

