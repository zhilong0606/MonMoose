using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.Battle
{
    public class Stage : BattleObj
    {
        private BattleStageStaticInfo m_staticInfo;
        private GroundStaticInfo m_groundStaticInfo;
        private List<BattleGrid> m_gridList = new List<BattleGrid>();
        private int m_gridWidth;
        private int m_gridHeight;
        private DcmVec2 m_stageSize;
        private StageControllerAbstract m_ctrl;
        private StateMachine m_stateMachine = new StateMachine();

        public BattleStageStaticInfo staticInfo
        {
            get { return m_staticInfo; }
        }

        private List<KeyValuePair<int, EntityInitData>> m_entityInitDataList = new List<KeyValuePair<int, EntityInitData>>();

        public void Init(int id)
        {
            m_staticInfo = StaticDataManager.instance.GetBattleStage(id);
            m_groundStaticInfo = StaticDataManager.instance.GetGround(m_staticInfo.GroundId);
            m_gridWidth = m_groundStaticInfo.LeftWidth + m_groundStaticInfo.RightWidth;
            m_gridHeight = m_groundStaticInfo.Height;
            m_ctrl = m_battleInstance.GetViewController(EBattleViewControllerType.Stage) as StageControllerAbstract;
            m_ctrl.Init(this);
            m_ctrl.StartLoadScene(OnLoadEnd);

            List<StageState> stateList = new List<StageState>();
            stateList.Add(new StageStateNone());
            stateList.Add(new StageStatePrepare());
            stateList.Add(new StageStateRunning());
            stateList.Add(new StageStateExiting());
            stateList.Add(new StageStateExit());
            for (int i = 0; i < stateList.Count; ++i)
            {
                StageState state = stateList[i];
                state.Init(this, m_battleInstance);
                m_stateMachine.Init(stateList);
            }
            m_stateMachine.ChangeState((int)EStageState.None);
        }

        private void OnLoadEnd()
        {
            for (int i = 0; i < m_groundStaticInfo.GridIdList.Count; ++i)
            {
                BattleGrid grid = m_battleInstance.FetchPoolObj<BattleGrid>(this);
                Dcm32 gridSize = new Dcm32(1);
                int gridPosX = i / m_groundStaticInfo.Height;
                int gridPosY = i % m_groundStaticInfo.Height;
                Dcm32 stagePosX = gridSize / 2 + gridSize * gridPosX;
                Dcm32 stagePosY = gridSize / 2 + gridSize * gridPosY;
                grid.Init(m_groundStaticInfo.GridIdList[i], new GridPosition(gridPosX, gridPosY), new DcmVec2(stagePosX, stagePosY), gridSize);
                m_gridList.Add(grid);
            }
            for (int i = 0; i < m_staticInfo.StageEntityList.Count; ++i)
            {
                StageEntityStaticInfo actorInfo = m_staticInfo.StageEntityList[i];
                EntityInitData initData = new EntityInitData();
                initData.id = actorInfo.Rid;
                initData.level = actorInfo.Level;
                initData.pos = new GridPosition(actorInfo.PosX, actorInfo.PosY);
                m_entityInitDataList.Add(new KeyValuePair<int, EntityInitData>(actorInfo.Uid, initData));
            }

            for (int i = 0; i < m_staticInfo.EmbattleList.Count; ++i)
            {
                EmbattleStaticInfo embattleInfo = m_staticInfo.EmbattleList[i];
                BattleGrid grid = GetGrid(embattleInfo.PosX, embattleInfo.PosY);
                if (grid != null)
                {
                    grid.ctrl.SetColor(1f, 0f, 0f);
                }
            }
        }

        public BattleGrid GetGrid(int x, int y)
        {
            for (int i = 0; i < m_gridList.Count; ++i)
            {
                if (m_gridList[i].gridPosition.x == x && m_gridList[i].gridPosition.y == y)
                {
                    return m_gridList[i];
                }
            }
            return null;
        }

        public void Enter()
        {
            for (int i = 0; i < m_entityInitDataList.Count; ++i)
            {
                BattleFactory.CreateEntity(m_battleInstance, m_entityInitDataList[i].Value, m_entityInitDataList[i].Key);
            }
        }

        public void Start()
        {
            m_stateMachine.ChangeState((int)EStageState.Prepare);
        }

        public void Tick()
        {
            
        }
    }
}
