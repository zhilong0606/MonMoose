using System;
using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;

namespace MonMoose.Battle
{
    public class FormationModule : Module
    {
        private List<FormationEntityInfo> m_list = new List<FormationEntityInfo>();

        public IEnumerable<FormationEntityInfo> entityIter
        {
            get { return m_list; }
        }

        public void AddFormationEntity(EntityInitData initData, int count)
        {
            FormationEntityInfo entityInfo = m_battleInstance.FetchPoolObj<FormationEntityInfo>(this);
            entityInfo.data = initData;
            entityInfo.count = count;
            m_list.Add(entityInfo);
        }

        public void RemoveFormationEntity(int rid)
        {
            FormationEntityInfo entityInfo = GetFormationEntity(rid);
            if (entityInfo != null)
            {
                m_list.Remove(entityInfo);
            }
        }

        public FormationEntityInfo GetFormationEntity(int rid)
        {
            for (int i = 0; i < m_list.Count; ++i)
            {
                if (m_list[i].data.rid == rid)
                {
                    return m_list[i];
                }
            }
            return null;
        }

        public Entity SetupEntity(int rid, GridPosition gridPosition)
        {
            FormationEntityInfo entityInfo = GetFormationEntity(rid);
            if (entityInfo != null && entityInfo.canSetup)
            {
                Entity entity = m_battleInstance.CreateEntity(entityInfo.data, EBattleObjType.DynamicEntity, gridPosition);
                entityInfo.Setup(entity.uid, gridPosition);
                return entity;
            }
            return null;
        }

        public void AddFormationActor(EntityInitData initData)
        {
            //m_battleInstance.CreateEntity(initData, EBattleObjType.DynamicEntity);
        }
        
        public void RemoveFormationActor(int uid)
        {
            m_battleInstance.RemoveEntity(uid);
        }

        public void ExchangeFormationActor()
        {

        }
    }
}
