using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareCoverWindow : UIWindow
    {
        private BattlePrepareCoverActorItemWidget m_actorItemWidget;

        public BattlePrepareCoverActorItemWidget actorItemWidget
        {
            get { return m_actorItemWidget; }
        }

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
    }
}
