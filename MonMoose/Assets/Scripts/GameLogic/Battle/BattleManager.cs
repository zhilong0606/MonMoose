using MonMoose.BattleLogic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattleManager : Singleton<BattleManager>
    {
        private BattleBase m_battleInstance;
        private GameObject m_actorRoot;
        private int m_hostTeamId;

        public GameObject actorRoot
        {
            get { return m_actorRoot; }
        }

        public BattleBase battleInstance
        {
            get { return m_battleInstance; }
        }

        public int hostTeamId
        {
            get { return m_hostTeamId; }
        }
        //private SkillController skillController = new SkillController();
        //private MoveController moveController = new MoveController();

        public void SetSceneConfig(BattleSceneConfig sceneConfig)
        {
            m_actorRoot = sceneConfig.actorRoot;
            //skillController.Init();
            //moveController.Init();
            //RegisterListener();
        }

        public void SetBattleInstance(BattleBase battleInstance)
        {
            m_battleInstance = battleInstance;
        }

        public void StagePrepare()
        {

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
