using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareCoverWindow : UIWindow
    {
        private BattlePrepareCoverActorItemWidget m_actorItemWidget;
        private bool m_isDraging;
        private int m_dragingActorId;
        private int m_fingerId = -1;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            PrefabPathStaticInfo info = StaticDataManager.instance.GetPrefabPathStaticInfo(EPrefabPathId.BattlePrefabActorItem);
            GameObject prefab = ResourceManager.instance.GetPrefab(info.Path);
            GameObject go = GameObject.Instantiate(prefab, transform);
            m_actorItemWidget = go.AddComponent<BattlePrepareCoverActorItemWidget>();
            m_actorItemWidget.Initialize(this);
            m_actorItemWidget.SetActive(false);
        }

        protected override void RegisterListener()
        {
            base.RegisterListener();
            EventManager.instance.RegisterListener<int>((int)EventID.BattlePrepare_DragActor_Start, OnDragActorStart);
            //EventManager.instance.RegisterListener<int>((int)EventID.BattlePrepare_DragActor_Stop, OnDragActorStop);
        }

        protected override void UnregisterListener()
        {
            base.UnregisterListener();
            EventManager.instance.UnregisterListener<int>((int)EventID.BattlePrepare_DragActor_Start, OnDragActorStart);
            //EventManager.instance.UnregisterListener<int>((int)EventID.BattlePrepare_DragActor_Stop, OnDragActorStop);
        }

        private void OnDragActorStart(int actorId)
        {
            m_isDraging = true;
            m_dragingActorId = actorId;
            m_actorItemWidget.SetActor(actorId);
        }

        private void Update()
        {
            if (m_isDraging)
            {
                m_actorItemWidget.SetActive(true);
                m_actorItemWidget.SetScreenPos(Input.mousePosition);
                if (Input.GetMouseButtonUp(0))
                {
                    m_isDraging = false;
                    m_actorItemWidget.SetActive(false);
                    BattleGridView view = BattleTouchSystem.instance.GetGridView(Input.mousePosition);
                    if (view == null)
                    {
                        EventManager.instance.Broadcast((int)EventID.BattlePrepare_DragActor_Cancel, m_dragingActorId);
                    }
                    else
                    {
                            
                    }
                    //EventManager.instance.Broadcast((int)EventID.BattlePrepare_DragActor_Stop)
                }
            }
        }
    }
}
