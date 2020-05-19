using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;
using UnityEngine;

public class EntityInfoComponent : EntityComponent
{
    private Actor actor;

    private int curLevel;
    private int curHp;

    AttributeHandler[] handlers = new AttributeHandler[(int) EAttributeType.Max];

    public void Init(Actor actor)
    {
        this.actor = actor;
        //handlers[(int) EAttributeType.MaxHp] = new AttributeHandler(this.actor.actorInfo.maxHp, 0, int.MaxValue, 0, LevelDependentCalculator);
        //handlers[(int) EAttributeType.PhysicalAttack] = new AttributeHandler(this.actor.actorInfo.physicalAttack, 0, int.MaxValue, 0, LevelDependentCalculator);

        curHp = handlers[(int) EAttributeType.Hp].TotalValue;
        curLevel = 1;
    }

    public void TakeDamage()
    {
        //Debug.LogError("player " + actor.ownerID + " Hurted!!");
    }

    public int LevelDependentCalculator(int baseValue, int addiation, int growth, int rate)
    {
        int value = baseValue + addiation + (curLevel - 1) * growth;
        value = value * rate / 100;
        return value;
    }
}
