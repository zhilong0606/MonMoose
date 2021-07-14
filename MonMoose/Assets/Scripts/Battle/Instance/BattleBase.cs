using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;

namespace MonMoose.Battle
{
    public partial class BattleBase
    {
        private List<Team> m_teamList = new List<Team>();
        private List<Entity> m_entityList = new List<Entity>();
        private List<Player> m_playerList = new List<Player>();
        private Func<EBattleViewControllerType, BattleViewController> m_funcOnCreateCtrl;
        private IBattleEventListener m_eventListener;
        
        private DebugModule m_debugModule = new DebugModule();
        private ObjIdModule m_objIdModule = new ObjIdModule();
        private SceneModule m_sceneModule = new SceneModule();
        private FrameSyncModule m_frameSyncModule = new FrameSyncModule();
        private MovePathFindModule m_pathFindModule = new MovePathFindModule();
        private FormationModule m_formationModule = new FormationModule();

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
            m_funcOnCreateCtrl = battleInitData.funcOnCreateCtrl;
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
            m_moduleList.Add(m_formationModule);

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
            StartScene();
            m_tickProcess.Start();
        }

        public void AddEntity(Entity entity)
        {
            m_entityList.Add(entity);
        }

        public void RemoveEntity(int uid)
        {
            Entity entity = GetEntity(uid);
            if (entity != null)
            {
                m_entityList.Remove(entity);
            }
        }

        public Entity CreateEntity(EntityInitData initData, EBattleObjType objType, GridPosition gridPosition)
        {
            return CreateEntity(initData, CreateObjId(objType), gridPosition);
        }

        public Entity CreateEntity(EntityInitData initData, int uid, GridPosition gridPosition)
        {
            Entity entity = null;
            EntityStaticInfo info = StaticDataManager.instance.GetEntity(initData.rid);
            switch (info.EntityType)
            {
                case EEntityType.Actor:
                    entity = FetchPoolObj<Actor>(typeof(BattleFactory));
                    break;
            }
            if (entity != null)
            {
                entity.Init(uid, initData);
                entity.GetComponent<LocationComponent>().SetPosition(gridPosition, DcmVec2.zero, true);
                AddEntity(entity);
            }
            return entity;
        }

        public void DestroyEntity(int uid)
        {
            Entity entity = GetEntity(uid);
            if (entity != null)
            {
                entity.UnInit();
                entity.Release();
            }
            RemoveEntity(uid);
        }

        public BattleViewController GetViewController(EBattleViewControllerType type)
        {
            if (m_funcOnCreateCtrl != null)
            {
                return m_funcOnCreateCtrl(type);
            }
            return null;
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

        public void Tick(float deltaTime)
        {
            m_tickProcess.Tick(deltaTime);
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
