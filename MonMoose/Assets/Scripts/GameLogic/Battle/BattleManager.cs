using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Logic
{
    public class BattleManager : Singleton<BattleManager>
    {
        private GameObject m_actorRoot;

        public GameObject actorRoot
        {
            get { return m_actorRoot; }
        }
        //private SkillController skillController = new SkillController();
        //private MoveController moveController = new MoveController();

        public void Init(BattleSceneConfig sceneConfig)
        {
            m_actorRoot = sceneConfig.actorRoot;
            //skillController.Init();
            //moveController.Init();
            //RegisterListener();
        }

        private void RegisterListener()
        {

        }

        private void RemoveListener()
        {

        }

        public void UpdateLogic()
        {
        }

        public void Clear()
        {
            //skillController.Clear();
            //moveController.Clear();
            RemoveListener();
        }
    }
}
