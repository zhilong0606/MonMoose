public class SkillInfo : BaseGameInfo
{
    public string name;
    public ESkillAppointType appointType;
    public ECostType costType;
    public ECostFuncType costFuncType;
    public int costValue;
    public int costGrowth;
    public int coolDown;
    public int coolDownGrowth;
    public int attackRange;
    public int searchRange;
    public int targetCampFilter;
    public int targetTypeFilter;
    public int immediateUse;
    public ESpeedFuncType speedFuncType;
    public float indicatorSize;
    public string iconPath;
    public string actionPath;
    public string indicatorPath;
    public string rangePath;

    public override void LoadCSVLine(string lineStr)
    {
        string[] attrStrs = lineStr.Split(',');
        id = int.Parse(attrStrs[0]);
        name = attrStrs[1];
        appointType = (ESkillAppointType)int.Parse(attrStrs[2]);
        costType = (ECostType)int.Parse(attrStrs[3]);
        costFuncType = (ECostFuncType)int.Parse(attrStrs[4]);
        costValue = int.Parse(attrStrs[5]);
        costGrowth = int.Parse(attrStrs[6]);
        coolDown = int.Parse(attrStrs[7]);
        coolDownGrowth = int.Parse(attrStrs[8]);
        attackRange = int.Parse(attrStrs[9]);
        searchRange = int.Parse(attrStrs[10]);
        targetCampFilter = int.Parse(attrStrs[11]);
        targetTypeFilter = int.Parse(attrStrs[12]);
        immediateUse = int.Parse(attrStrs[13]);
        speedFuncType = (ESpeedFuncType)int.Parse(attrStrs[14]);
        indicatorSize = float.Parse(attrStrs[15]);
        iconPath = attrStrs[16];
        actionPath = attrStrs[17];
        indicatorPath = attrStrs[18];
        rangePath = attrStrs[19];
    }

}
