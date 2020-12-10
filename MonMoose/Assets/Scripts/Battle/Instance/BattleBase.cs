using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.Battle
{
    public class BattleBase
    {

        private List<Team> m_teamList = new List<Team>();
        private List<Entity> m_entityList = new List<Entity>();
        private List<Player> m_playerList = new List<Player>();
        private Func<int, EntityView> m_funcOnGetView;
        private IBattleEventListener m_eventListener;

        private DebugModule m_debugModule = new DebugModule();
        private ObjIdModule m_objIdModule = new ObjIdModule();

        private SceneModule m_sceneModule = new SceneModule();

        private FrameSyncModule m_frameSyncModule = new FrameSyncModule();
        private MovePathFindModule m_pathFindModule = new MovePathFindModule();

        private List<Module> m_moduleList = new List<Module>();
        private TickProcess m_tickProcess = new TickProcess();

        public List<Entity> entityList
        {
            get { return m_entityList; }
        }

        public IBattleEventListener eventListener
        {
            get { return m_eventListener; }
        }

        public FrameSyncSender sender
        {
            get { return m_frameSyncModule.sender; }
        }

        public void Init(BattleInitData battleInitData)
        {
            m_eventListener = battleInitData.eventListener;
            m_funcOnGetView = battleInitData.funcOnGetView;
            InitModuleList(battleInitData);
            InitTeamList(battleInitData);
            m_tickProcess.Init(FrameSyncDefine.TimeInterval);
            m_tickProcess.RegisterListener(OnFrameTick);
        }

        private void InitModuleList(BattleInitData battleInitData)
        {
            m_moduleList.Add(m_debugModule);
            m_moduleList.Add(m_objIdModule);
            m_moduleList.Add(m_sceneModule);
            m_moduleList.Add(m_frameSyncModule);
            m_moduleList.Add(m_pathFindModule);

            for (int i = 0; i < m_moduleList.Count; ++i)
            {
                m_moduleList[i].Init(this, battleInitData);
            }
        }

        public T FetchPoolObj<T>(object causer) where T : class
        {
            T obj = ClassPoolManager.instance.Fetch<T>(causer);
            BattleObj battleObj = obj as BattleObj;
            if (battleObj != null)
            {
                battleObj.SetBattleInstance(this);
            }
            return obj;
        }

        private void InitTeamList(BattleInitData battleInitData)
        {
            battleInitData.teamList.Sort(TeamInitData.Sort);
            for (int i = 0; i < battleInitData.teamList.Count; ++i)
            {
                Team team = FetchPoolObj<Team>(this);
                team.Init(battleInitData.teamList[i]);
                m_teamList.Add(team);
            }

            m_entityList.Sort(Entity.Sort);
        }

        public void Start()
        {
            m_sceneModule.Start();
            m_tickProcess.Start();
        }

        public void AddEntity(Entity entity)
        {
            m_entityList.Add(entity);
        }

        public EntityView GetEntityView(int entityId)
        {
            if (m_funcOnGetView != null)
            {
                return m_funcOnGetView(entityId);
            }

            return FetchPoolObj<EmptyView>(this);
        }

        public Player GetPlayer(int playerId)
        {
            for (int i = 0; i < m_playerList.Count; ++i)
            {
                if (m_playerList[i].id == playerId)
                {
                    return m_playerList[i];
                }
            }
            return null;
        }

        public Entity GetEntity(int uid)
        {
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                if (m_entityList[i].uid == uid)
                {
                    return m_entityList[i];
                }
            }

            return null;
        }

        public bool CheckActiveController(Controller controller)
        {
            return true;
        }

        public void GetEntityListByTeamId(int teamId, List<Entity> entityList)
        {
            entityList.Clear();
            Team team = GetTeam(teamId);
            if (team != null)
            {
                entityList.AddRange(team.entityList);
            }
        }

        public Team GetTeam(int teamId)
        {
            for (int i = 0; i < m_teamList.Count; ++i)
            {
                if (m_teamList[i].id == teamId)
                {
                    return m_teamList[i];
                }
            }

            return null;
        }

        public void GetEntitysByGrid(GridPosition gridPos, List<Entity> outList)
        {
            outList.Clear();
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                if (m_entityList[i].GetComponent<LocationComponent>().IsOccupyGridPosition(gridPos))
                {
                    outList.Add(m_entityList[i]);
                }
            }
        }

        public bool IsGridEmpty(GridPosition pos)
        {
            BattleGrid grid = GetGrid(pos);
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                if (m_entityList[i].GetComponent<LocationComponent>().locateGrid == grid)
                {
                    return false;
                }
            }

            return true;
        }

        public BattleGrid GetGrid(int x, int y)
        {
            return m_sceneModule.GetGrid(x, y);
        }

        public BattleGrid GetGrid(GridPosition gridPos)
        {
            return m_sceneModule.GetGrid(gridPos);
        }

        public void StartFrameSync()
        {
            m_frameSyncModule.Start();
        }

        public int CreateObjId(EBattleObjType type)
        {
            return m_objIdModule.CreateObjId(type);
        }

        public bool FindPath(Entity entity, BattleGrid startGrid, DcmVec2 offset, BattleGrid targetGrid, List<BattleGrid> gridList)
        {
            return m_pathFindModule.FindPath(entity, startGrid, offset, targetGrid, gridList);
        }

        public void Log(int level, string str)
        {
            m_debugModule.Log(level, str);
        }

        public void Tick(float deltaTime)
        {
            m_tickProcess.Tick(deltaTime);
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                m_entityList[i].view.Tick(deltaTime);
            }
        }

        private void OnFrameTick(TickProcess process)
        {
            for (int i = 0; i < m_moduleList.Count; ++i)
            {
                m_moduleList[i].Tick();
            }

            for (int i = 0; i < m_teamList.Count; ++i)
            {
                m_teamList[i].Tick();
            }

            for (int i = 0; i < m_entityList.Count; ++i)
            {
                m_entityList[i].Tick();
            }
        }
    }
}
