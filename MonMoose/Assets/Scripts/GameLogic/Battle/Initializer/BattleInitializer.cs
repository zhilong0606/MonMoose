using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;
using UnityEngine.SceneManagement;
using Grid = MonMoose.Logic.Battle.Grid;

namespace MonMoose.Logic
{
    public class BattleInitializer : Initializer
    {
        protected override IEnumerator OnProcess()
        {
            string battleSceneName = "BattleScene";
            SceneManager.LoadSceneAsync(battleSceneName);
            if (!SceneManager.GetSceneByName(battleSceneName).isLoaded)
            {
                yield return null;
            }
            yield return null;
            InitScene();
            yield return null;
            //m_battleInitData.teamList.Sort(TeamInitData.Sort);
            //for (int i = 0; i < m_battleInitData.teamList.Count; ++i)
            //{
            //    TeamInitData teamInitData = m_battleInitData.teamList[i];
            //    Team team = new Team();
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
                Grid grid = new Grid();
                //grid.Init(gridViews[i], gridViews[i].position);
                //BattleGridManager.instance.AddGrid(grid);
            }
        }
    }
}