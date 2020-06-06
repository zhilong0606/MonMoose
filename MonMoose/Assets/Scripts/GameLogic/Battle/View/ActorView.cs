using System.Collections;
using System.Collections.Generic;
using MonMoose.Logic.Battle;
using UnityEngine;

namespace MonMoose.Logic
{
    public class ActorView : EntityObjView
    {
        private GameObject m_rotateRoot;

        protected override GameObject rotateRoot
        {
            get { return m_rotateRoot; }
        }

        protected override string prefabPath
        {
            get
            {
                ActorInfoComponent infoComponent = m_entity.GetComponent<ActorInfoComponent>();
                return infoComponent.actorStaticInfo.PrefabPath;
            }
        }

        public override void CreateView()
        {
            base.CreateView();
            m_rotateRoot = m_obj.transform.Find("Rotate").gameObject;
        }
    }
}
