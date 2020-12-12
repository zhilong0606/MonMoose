﻿using System.Collections;
using System.Collections.Generic;

namespace MonMoose.Battle
{
    public class EntityInitComponent : EntityComponent
    {
        public override EEntityComponentType type
        {
            get { return EEntityComponentType.Init; }
        }

        protected override void OnInit(EntityInitData entityInitData)
        {
            base.OnInit(entityInitData);
            m_entity.ctrl.InitView();
        }
    }
}
