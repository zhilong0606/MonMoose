﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.Logic;
using MonMoose.StaticData;
using UnityEngine.SceneManagement;

namespace MonMoose.GameLogic.Battle
{
    public class BattleInitializer : Initializer
    {
        string battleSceneName = "BattleScene";
        protected override IEnumerator OnProcess()
        {
            SceneManager.LoadSceneAsync(battleSceneName);
            while (!SceneManager.GetSceneByName(battleSceneName).isLoaded)
            {
                yield return null;
            }
            
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
                Debug.LogError("sceneConfigRoot");
                return;
            }
            BattleSceneConfig sceneConfig = sceneConfigRoot.GetComponent<BattleSceneConfig>();
            if (sceneConfig == null)
            {
                Debug.LogError("sceneConfig");
                return;
            }
            BattleManager.instance.SetSceneConfig(sceneConfig);
            BattleGridConfig[] configs = sceneConfig.gridRoot.GetComponentsInChildren<BattleGridConfig>();
            for (int i = 0; i < configs.Length; ++i)
            {
                GameObject go = configs[i].gameObject;
                BattleGridView view = go.AddComponent<BattleGridView>();
                view.Init();
                BattleGridManager.instance.AddGridView(view);
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