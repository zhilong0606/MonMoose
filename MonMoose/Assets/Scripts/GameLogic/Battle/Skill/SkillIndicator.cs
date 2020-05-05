using UnityEngine;

public class SkillIndicator : MonoBehaviour
{
    public EIndicatorType type;
    public GameObject indicatorObj;
    public GameObject coreObj;

    private SkillIndicatorContext context;
    private float range = 10f;
    private float size = 1f;
    private bool visible = true;

    public bool Visible
    {
        get { return visible; }
        set
        {      
            visible = value;
            indicatorObj.SetActive(value);
        }
    }

    public void Init(GameObject root, SkillIndicatorContext context)
    {
        indicatorObj.transform.SetParent(root.transform);
        indicatorObj.transform.localPosition = Vector3.zero;
        indicatorObj.transform.localScale = Vector3.one;
        this.context = context;
        Visible = false;
    }

    public void SetSkill(Skill skill)
    {
        range = FrameSyncUtility.MilliToFloat(skill.info.attackRange);
        size = skill.info.indicatorSize;
    }

    private void Update()
    {
        if (!visible)
        {
            return;
        }
        switch (type)
        {
            case EIndicatorType.Direction:
                indicatorObj.transform.forward = context.direction;
                indicatorObj.transform.localScale = new Vector3(size, 1, range);
                break;
            case EIndicatorType.Distance:
                indicatorObj.transform.forward = context.direction;
                indicatorObj.transform.localScale = new Vector3(size, 1, context.distance);
                break;
            case EIndicatorType.Pos:
                indicatorObj.transform.forward = context.direction;
                coreObj.transform.localPosition = new Vector3(0, 0, context.distance);
                coreObj.transform.localScale = new Vector3(size, 1, size);
                break;
        }
    }
}
