using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.BattleLogic
{
    internal static class BattleFactory
    {
        public static Entity CreateEntity(BattleBase battleInstance, EntityInitData initData, int uid)
        {
            Entity entity = null;
            EntityStaticInfo info = StaticDataManager.instance.GetEntityStaticInfo(initData.id);
            switch (info.EntityType)
            {
                case EEntityType.Actor:
                    entity = battleInstance.FetchPoolObj<Actor>();
                    break;
            }
            if (entity != null)
            {
                entity.Init(uid, initData);
                battleInstance.AddEntity(entity);
            }
            return entity;
        }
    }
}
