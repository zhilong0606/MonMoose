public class ActorInfo : BaseGameInfo
{
    public int[] skillIds;
    public int[] animationIDs;
    public int maxHp;
    public int maxEp;
    public int hpRecover;
    public int epRecover;
    public int physicalAttack;
    public int magicalAttack;
    public int physicalDefence;
    public int magicalDefence;
    public int attackSpeed;
    public int moveSpeed;
    public string prefabPath;

    public override void LoadCSVLine(string lineStr)
    {
        string[] attrStrs = lineStr.Split(',');
        id = int.Parse(attrStrs[(int) EValueType.Id]);
        string[] skillIDStrs = attrStrs[(int) EValueType.SkillIds].Split('|');
        skillIds = new int[skillIDStrs.Length];
        for (int i = 0; i < skillIDStrs.Length; ++i)
        {
            skillIds[i] = int.Parse(skillIDStrs[i]);
        }
        string[] animIDStrs = attrStrs[(int) EValueType.AnimationIds].Split('|');
        animationIDs = new int[animIDStrs.Length];
        for (int i = 0; i < animIDStrs.Length; ++i)
        {
            animationIDs[i] = int.Parse(animIDStrs[i]);
        }
        maxHp = int.Parse(attrStrs[(int) EValueType.MaxHp]);
        maxEp = int.Parse(attrStrs[(int) EValueType.MaxEp]);
        hpRecover = int.Parse(attrStrs[(int) EValueType.HpRecover]);
        epRecover = int.Parse(attrStrs[(int) EValueType.EpRecover]);
        physicalAttack = int.Parse(attrStrs[(int) EValueType.PhysicalAttack]);
        magicalAttack = int.Parse(attrStrs[(int) EValueType.MagicalAttack]);
        physicalDefence = int.Parse(attrStrs[(int) EValueType.PhysicaDefence]);
        magicalDefence = int.Parse(attrStrs[(int) EValueType.MagicaDefence]);
        attackSpeed = int.Parse(attrStrs[(int) EValueType.AttackSpeed]);
        moveSpeed = int.Parse(attrStrs[(int) EValueType.MoveSpeed]);
        prefabPath = attrStrs[(int) EValueType.PrefabPath];
    }

    private enum EValueType
    {
        Id,
        SkillIds,
        AnimationIds,
        MaxHp,
        MaxEp,
        HpRecover,
        EpRecover,
        PhysicalAttack,
        MagicalAttack,
        PhysicaDefence,
        MagicaDefence,
        AttackSpeed,
        MoveSpeed,
        PrefabPath,
    }
}
