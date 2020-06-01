using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    internal static class BattleFactory
    {
        public static Entity CreateEntity(BattleBase battleInstance, int entityRid, int uid)
        {
            Entity entity = null;
            EntityStaticInfo info = StaticDataManager.instance.GetEntityStaticInfo(entityRid);
            switch (info.EntityType)
            {
                case EEntityType.Actor:
                    entity = battleInstance.FetchPoolObj<Actor>();
                    break;
            }
            if (entity != null)
            {
                entity.Init(uid, info.RefId);
            }
            return entity;
        }
    }
}
