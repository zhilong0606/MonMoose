using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.Logic.Battle;
using MonMoose.StaticData;
using UnityEngine.SceneManagement;
using Grid = MonMoose.Logic.Battle.Grid;

namespace MonMoose.Logic.Battle
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
            BattleManager.CreateInstance();
            BattleTouchSystem.CreateInstance();
            yield return null;
            InitScene();
            yield return null;
            InitUI();
            yield return null;
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
            BattleManager.instance.Init(sceneConfig);
            BattleGridView[] gridViews = sceneConfig.gridRoot.GetComponentsInChildren<BattleGridView>();
            for (int i = 0; i < gridViews.Length; ++i)
            {
                BattleGridManager.instance.AddGridView(gridViews[i]);
            }
        }

        private void InitUI()
        {
            UIWindowManager.instance.OpenWindow((int)EWindowId.BattleMain);
            UIWindowManager.instance.OpenWindow((int)EWindowId.BattlePrepare);
            UIWindowManager.instance.OpenWindow((int)EWindowId.BattlePrepareCover);
        }
    }
}