using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Battle
{
    internal static class BattleFactory
    {
        //public static Entity CreateEntity(BattleBase battleInstance, EntityInitData initData, int uid)
        //{
        //    Entity entity = null;
        //    EntityStaticInfo info = StaticDataManager.instance.GetEntity(initData.rid);
        //    switch (info.EntityType)
        //    {
        //        case EEntityType.Actor:
        //            entity = battleInstance.FetchPoolObj<Actor>(typeof(BattleFactory));
        //            break;
        //    }
        //    if (entity != null)
        //    {
        //        entity.Init(uid, initData);
        //        battleInstance.AddEntity(entity);
        //    }
        //    return entity;
        //}
    }
}
