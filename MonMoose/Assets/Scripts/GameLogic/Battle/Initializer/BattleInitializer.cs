using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonMoose.Core;
using MonMoose.GameLogic;
using MonMoose.StaticData;
using UnityEngine.SceneManagement;

namespace MonMoose.GameLogic.Battle
{
    public class BattleInitializer : Initializer
    {
        private BattleScene m_battleScene = new BattleScene();

        public BattleScene battleScene
        {
            get { return m_battleScene; }
        }

        protected override IEnumerator OnProcess()
        {
            yield return null;
            BattleTouchSystem.CreateInstance();
            battleScene.Init();
            yield return null;
            InitUI();
            yield return null;
        }

        private void InitUI()
        {
            UIWindowManager.instance.OpenWindow((int)EWindowId.BattleMain);
            UIWindowManager.instance.OpenWindow((int)EWindowId.BattlePrepare);
            UIWindowManager.instance.OpenWindow((int)EWindowId.BattlePrepareCover);
        }
    }
}