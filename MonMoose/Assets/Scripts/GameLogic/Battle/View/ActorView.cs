using System.Collections;
using System.Collections.Generic;
using MonMoose.Logic.Battle;
using UnityEngine;

namespace MonMoose.Logic
{
    public class ActorView : EntityObjView
    {
        protected override string prefabPath
        {
            get
            {
                ActorInfoComponent infoComponent = m_entity.GetComponent<ActorInfoComponent>();
                return infoComponent.actorStaticInfo.PrefabPath;
            }
        }
    }
}
