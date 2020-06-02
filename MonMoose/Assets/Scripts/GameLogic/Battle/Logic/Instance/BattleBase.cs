﻿using System;
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
        private FrameSyncSender m_frameSyncSender = new FrameSyncSender();
        private FrameSyncManager m_frameSyncManager = new FrameSyncManager();

        private List<Module> m_moduleList = new List<Module>();

        public DebugModule debugModule
        {
            get { return m_debugModule; }
        }

        public void Init(BattleInitData battleInitData)
        {
            m_funcOnGetView = battleInitData.funcOnGetView;
            m_frameSyncManager.Init(this, FrameTick);
            InitModuleList(battleInitData);
            InitTeamList(battleInitData);
        }

        private void InitModuleList(BattleInitData battleInitData)
        {
            m_moduleList.Add(m_debugModule);
            m_moduleList.Add(m_poolModule);
            m_moduleList.Add(m_objIdModule);
            m_moduleList.Add(m_sceneModule);

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
        }

        public void Start()
        {

        }

        public FrameSyncSender GetSender()
        {
            if (m_frameSyncSender == null)
            {
                m_frameSyncSender = new FrameSyncSender();
            }
            return m_frameSyncSender;
        }

        public EntityView GetEntityView(int entityId)
        {
            if (m_funcOnGetView != null)
            {
                return m_funcOnGetView(entityId);
            }
            return FetchPoolObj<EmptyView>();
        }

        public Grid GetGrid(int x, int y)
        {
            return m_sceneModule.GetGrid(x, y);
        }

        public Grid GetGrid(GridPosition gridPos)
        {
            return m_sceneModule.GetGrid(gridPos);
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

        public void Tick(float deltaTime)
        {
            m_frameSyncManager.Tick(deltaTime);
        }

        internal void FrameTick()
        {
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
