using System.Collections;
using System.Collections.Generic;
using MonMoose.BattleLogic;
using MonMoose.Core;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class BattlePrepareActorManager : Singleton<BattlePrepareActorManager>
    {
        private List<PrepareActor> m_actorList = new List<PrepareActor>();

        public void AddActor(int actorId, GameObject actorObj, GridPosition position)
        {
            PrepareActor actor = new PrepareActor();
            actor.id = actorId;
            actor.obj = actorObj;
            actor.gridPos = position;
            m_actorList.Add(actor);
        }

        public void RemoveActor(int actorId)
        {
            for (int i = 0; i < m_actorList.Count; ++i)
            {
                if (m_actorList[i].id == actorId)
                {
                    m_actorList.RemoveAt(i);
                    break;
                }
            }
        }

        public bool TryGetActor(GridPosition position, out int actorId, out GameObject actorObj)
        {
            for (int i = 0; i < m_actorList.Count; ++i)
            {
                if (m_actorList[i].gridPos == position)
                {
                    actorObj = m_actorList[i].obj;
                    actorId = m_actorList[i].id;
                    return true;
                }
            }
            actorObj = null;
            actorId = 0;
            return false;
        }

        private struct PrepareActor
        {
            public int id;
            public GameObject obj;
            public GridPosition gridPos;
        }
    }
}
