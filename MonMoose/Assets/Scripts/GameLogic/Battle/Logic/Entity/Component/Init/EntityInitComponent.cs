﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Logic.Battle
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
            m_entity.view.CreateView();
        }
    }
}
