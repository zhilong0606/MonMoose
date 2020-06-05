using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class EntityInfoComponent : EntityComponent
    {
        protected EntityStaticInfo m_entityStaticInfo;
        protected int curLevel;
        protected int curHp;

        protected AttributeHandler[] handlers = new AttributeHandler[(int)EAttributeType.Max];

        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Info; }
        }

        public virtual int size
        {
            get { return 1; }
        }

        protected override void OnInit(EntityInitData entityInitData)
        {
            base.OnInit(entityInitData);
            m_entityStaticInfo = StaticDataManager.instance.GetEntityStaticInfo(entityInitData.id);
            OnInitSpecific(entityInitData);
            //handlers[(int) EAttributeType.MaxHp] = new AttributeHandler(this.actor.actorInfo.maxHp, 0, int.MaxValue, 0, LevelDependentCalculator);
            //handlers[(int) EAttributeType.PhysicalAttack] = new AttributeHandler(this.actor.actorInfo.physicalAttack, 0, int.MaxValue, 0, LevelDependentCalculator);

            //curHp = handlers[(int)EAttributeType.Hp].TotalValue;
            curLevel = 1;
        }

        protected virtual void OnInitSpecific(EntityInitData entityInitData)
        {

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
}
