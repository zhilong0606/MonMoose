using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace MonMoose.GameLogic.UI
{

    public class LobbyWindow : UIWindow
    {
        private Dictionary<GameObject, int> m_goToIdMap = new Dictionary<GameObject, int>();

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            GameObjectPool pool = GetInventory().GetComponent<GameObjectPool>((int)EWidget.Content);
            pool.Init();
            foreach (BattleSceneStaticInfo battleInfo in StaticDataManager.instance.battleSceneList)
            {
                var holder = pool.Fetch();
                holder.obj.transform.Find("Text").GetComponent<Text>().text = string.Format("{0}.{1}", battleInfo.Id, battleInfo.Name);
                holder.obj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    int battleId = m_goToIdMap[holder.obj];
                    EventManager.instance.Broadcast((int)EventID.BattleStart_StartRequest, battleId);
                });
                m_goToIdMap[holder.obj] = battleInfo.Id;
            }
        }

        private enum EWidget
        {
            Content,
        }
    }
}
