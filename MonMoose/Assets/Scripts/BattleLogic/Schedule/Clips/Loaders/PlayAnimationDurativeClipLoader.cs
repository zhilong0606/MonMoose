using System.Xml;

namespace MonMoose.BattleLogic
{
    public partial class PlayAnimationDurativeClip
    {
        public override void Load(XmlElement element)
        {
            base.Load(element);
            for (int i = 0; i < element.ChildNodes.Count; ++i)
            {
                if (element.ChildNodes[i].Name == "AnimName")
                {
                    animName = element.ChildNodes[i].Attributes["value"].Value;
                }
            }
        }
    }
}
