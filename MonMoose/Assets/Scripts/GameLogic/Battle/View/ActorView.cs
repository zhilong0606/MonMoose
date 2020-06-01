using System.Collections;
using System.Collections.Generic;
using MonMoose.Logic.Battle;
using UnityEngine;

namespace MonMoose.Logic
{
    public class ActorView : EntityView
    {
        private GameObject m_obj;

        public void Init(GameObject obj)
        {
            m_obj = obj;
        }

        public override void SetPosition(FixVec2 pos)
        {
            m_obj.transform.position = new Vector3((float)pos.x, 0f, (float)pos.y);
        }

        public override void CreateView()
        {
            base.CreateView();
        }
    }
}
