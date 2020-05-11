using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActorListWidget : UIComponent
{
    private GameObjectPool m_pool;

    protected override void OnInit(object param)
    {
        base.OnInit(param);
        m_pool = GetComponentToWidget<GameObjectPool>((int)EWidget.Pool);
        m_pool.Init(OnActorItemInit);
    }

    private void OnActorItemInit(GameObjectPool.PoolObjHolder holder)
    {
        BattleActorItemWidget widget = holder.obj.AddComponent<BattleActorItemWidget>();
        widget.Initialize(window);
        holder.AddComponent()
    }

    private enum EWidget
    {
        Pool,
    }
}
