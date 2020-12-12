using System.Collections;
using System.Collections.Generic;
using MonMoose.Battle;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public class ActorView : EntityView
    {
        private GameObject m_rotateRoot;

        protected override GameObject rotateRoot
        {
            get { return m_rotateRoot; }
        }

        protected override void OnInit()
        {
            m_rotateRoot = transform.Find("Rotate").gameObject;
        }

        protected override void OnTick(float deltaTime)
        {
            if (posLerp.isStart)
            {
                posLerp.Tick(deltaTime);
                transform.position = posLerp.curValue;
            }
        }
    }
}
