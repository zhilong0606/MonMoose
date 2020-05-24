﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;
using UnityEngine.SceneManagement;

namespace MonMoose.Logic
{
    public class BattleInitializer : Initializer
    {
        private BattleBase m_battleInstance;

        public void Init(BattleBase battleInstance)
        {
            m_battleInstance = battleInstance;
        }

        protected override IEnumerator OnProcess()
        {
            string battleSceneName = "BattleScene";
            SceneManager.LoadSceneAsync(battleSceneName);
            if (!SceneManager.GetSceneByName(battleSceneName).isLoaded)
            {
                yield return null;
            }
            InitScene();
            //m_battleInitData.teamList.Sort(BattleTeamInitData.Sort);
            //for (int i = 0; i < m_battleInitData.teamList.Count; ++i)
            //{
            //    BattleTeamInitData teamInitData = m_battleInitData.teamList[i];
            //    BattleTeam team = new BattleTeam();
            //    team.id = teamInitData.id;
            //    team.camp = teamInitData.camp;
            //    team.name = teamInitData.name;
            //    team.isAI = teamInitData.isAI;
            //    for (int j = 0; j < teamInitData.actorList.Count; ++j)
            //    {
            //        ActorInitData actorInitData = teamInitData.actorList[j];
            //        Actor actor = new Actor();
            //        actor.Init(actorInitData.id);
            //        //ActorManager.instance.AddHero(actor);
            //        yield return null;
            //    }
            //    //BattlePlayerManager.instance.AddPlayer(player);
            //}
        }

        private void InitScene()
        {
            GameObject sceneConfigRoot = GameObject.Find("SceneConfigRoot");
            if (sceneConfigRoot == null)
            {
                return;
            }
            BattleSceneConfig sceneConfig = sceneConfigRoot.GetComponent<BattleSceneConfig>();
            if (sceneConfig == null)
            {
                return;
            }
            BattleGridView[] gridViews = sceneConfig.gridRoot.GetComponentsInChildren<BattleGridView>();
            for (int i = 0; i < gridViews.Length; ++i)
            {
                BattleGrid grid = new BattleGrid();
                //grid.Init(gridViews[i], gridViews[i].position);
                //BattleGridManager.instance.AddGrid(grid);
            }

            ActorStaticInfo info = StaticDataManager.instance.GetActorStaticInfo(1);
            GameObject go = ResourceManager.instance.GetPrefab(info.PrefabPath);
            GameObject actorObj = GameObject.Instantiate<GameObject>(go);
            //actorObj.transform.position = BattleGridManager.instance.GetGrid(0, 0).transPos;
        }
    }
}