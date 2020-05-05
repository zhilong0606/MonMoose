public class AnimationInfo : BaseGameInfo
{
    public string animName;
    public int priority;

    public override void LoadCSVLine(string lineStr)
    {
        string[] attrStrs = lineStr.Split(',');
        id = int.Parse(attrStrs[0]);
        animName = attrStrs[1];
        priority = int.Parse(attrStrs[2]);
    }
}
