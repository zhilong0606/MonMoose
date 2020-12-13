using System.Collections;
using System.Collections.Generic;
using System.IO;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonMoose.GameLogic.Battle
{
    public class BattleStageInitializer : Initializer
    {
        private string m_battleSceneName;
        private Scene m_scene;

        public Scene scene
        {
            get { return m_scene; }
        }

        public void Init(BattleStageStaticInfo staticInfo)
        {
            m_battleSceneName = "Assets/Resources/Exported/" + staticInfo.ScenePath;
        }

        protected override IEnumerator OnProcess()
        {
            AsyncOperation asyncOperation = EditorSceneManager.LoadSceneAsyncInPlayMode(m_battleSceneName, new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.None));
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            string sceneName = Path.GetFileNameWithoutExtension(m_battleSceneName);
            m_scene = SceneManager.GetSceneByName(sceneName);
            if (!m_scene.IsValid())
            {
                Debug.LogError(m_battleSceneName + " is Not Valid");
                yield return null;
            }
            while (!m_scene.isLoaded)
            {
                yield return null;
            }
            SceneManager.SetActiveScene(m_scene);
            yield return null;
            InitScene();
            yield return null;
        }

        private void InitScene()
        {
            GameObject sceneConfigRoot = null;
            GameObject[] rootObjs = m_scene.GetRootGameObjects();
            foreach (GameObject rootObj in rootObjs)
            {
                if (rootObj.name == "SceneConfigRoot")
                {
                    sceneConfigRoot = rootObj;
                    break;
                }
            }
            if (sceneConfigRoot == null)
            {
                return;
            }
            BattleSceneConfig sceneConfig = sceneConfigRoot.GetComponent<BattleSceneConfig>();
            if (sceneConfig == null)
            {
                return;
            }
            BattleGridConfig[] configs = sceneConfig.gridRoot.GetComponentsInChildren<BattleGridConfig>();
            for (int i = 0; i < configs.Length; ++i)
            {
                GameObject go = configs[i].gameObject;
                BattleGridView view = go.AddComponent<BattleGridView>();
                view.Init();
                BattleShortCut.AddGridView(view);
            }
        }
    }
}
