using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.StaticData;

namespace MonMoose.Logic.Battle
{
    public class BattleBase
    {
        private List<Team> m_teamList = new List<Team>();
        private List<Entity> m_entityList = new List<Entity>();
        private Func<int, EntityView> m_funcOnGetView;

        private DebugModule m_debugModule = new DebugModule();
        private PoolModule m_poolModule = new PoolModule();
        private ObjIdModule m_objIdModule = new ObjIdModule();
        private SceneModule m_sceneModule = new SceneModule();
        private FrameSyncModule m_frameSyncModule = new FrameSyncModule();
        private MovePathFindModule m_pathFindModule = new MovePathFindModule();

        private List<Module> m_moduleList = new List<Module>();

        public List<Entity> entityList
        {
            get { return m_entityList; }
        }

        public FrameSyncSender sender
        {
            get { return m_frameSyncModule.sender; }
        }

        public void Init(BattleInitData battleInitData)
        {
            m_funcOnGetView = battleInitData.funcOnGetView;
            InitModuleList(battleInitData);
            InitTeamList(battleInitData);
        }

        private void InitModuleList(BattleInitData battleInitData)
        {
            m_moduleList.Add(m_debugModule);
            m_moduleList.Add(m_poolModule);
            m_moduleList.Add(m_objIdModule);
            m_moduleList.Add(m_sceneModule);
            m_moduleList.Add(m_frameSyncModule);
            m_moduleList.Add(m_pathFindModule);

            for (int i = 0; i < m_moduleList.Count; ++i)
            {
                m_moduleList[i].Init(this, battleInitData);
            }
        }

        private void InitTeamList(BattleInitData battleInitData)
        {
            battleInitData.teamList.Sort(TeamInitData.Sort);
            for (int i = 0; i < battleInitData.teamList.Count; ++i)
            {
                Team team = FetchPoolObj<Team>();
                team.Init(battleInitData.teamList[i]);
                m_teamList.Add(team);
            }
            m_entityList.Sort(Entity.Sort);
        }

        public void Start()
        {
            m_sceneModule.Start();
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
            return FetchPoolObj<EmptyView>();
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
            Grid grid = GetGrid(pos);
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                if (m_entityList[i].GetComponent<LocationComponent>().locateGrid == grid)
                {
                    return false;
                }
            }
            return true;
        }

        public Grid GetGrid(int x, int y)
        {
            return m_sceneModule.GetGrid(x, y);
        }

        public Grid GetGrid(GridPosition gridPos)
        {
            return m_sceneModule.GetGrid(gridPos);
        }

        public void WaitFrameCommand(EFrameCommandType cmdType)
        {
            m_frameSyncModule.WaitFrameCommand(cmdType);
        }

        public int CreateObjId(EBattleObjType type)
        {
            return m_objIdModule.CreateObjId(type);
        }

        public T FetchPoolObj<T>() where T : class
        {
            T obj = m_poolModule.Fetch<T>();
            BattleObj battleObj = obj as BattleObj;
            if (battleObj != null)
            {
                battleObj.SetBattleInstance(this);
            }
            return obj;
        }

        public bool FindPath(Entity entity, Grid startGrid, DcmVec2 offset, Grid targetGrid, List<Grid> gridList)
        {
            return m_pathFindModule.FindPath(entity, startGrid, offset, targetGrid, gridList);
        }

        public void Log(int level, string str)
        {
            m_debugModule.Log(level, str);
        }

        public void Tick(float deltaTime)
        {
            m_frameSyncModule.Tick(deltaTime);
            for (int i = 0; i < m_entityList.Count; ++i)
            {
                m_entityList[i].view.Tick(deltaTime);
            }
        }

        internal void FrameTick()
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
