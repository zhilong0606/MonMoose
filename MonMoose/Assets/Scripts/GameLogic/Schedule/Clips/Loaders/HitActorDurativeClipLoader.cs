using System.Xml;

namespace MonMoose.Logic
{
    public partial class HitActorDurativeClip
    {
        public override void Load(XmlElement element)
        {
            base.Load(element);
            for (int i = 0; i < element.ChildNodes.Count; ++i)
            {
                if (element.ChildNodes[i].Name == "Radius")
                {
                    radius = int.Parse(element.ChildNodes[i].Attributes["value"].Value);
                }
            }
        }
    }
}
