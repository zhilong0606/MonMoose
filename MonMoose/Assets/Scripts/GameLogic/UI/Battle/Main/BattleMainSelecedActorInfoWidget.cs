using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonMoose.Core;
using MonMoose.StaticData;

namespace MonMoose.GameLogic.UI
{
    public class BattleMainSelecedActorInfoWidget : UIComponent
    {
        private Image m_iconImage;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_iconImage = GetInventory().GetComponent<Image>((int)EWidget.IconImage);
        }

        protected override void RegisterListener()
        {
            base.RegisterListener();
            EventManager.instance.RegisterListener<int>((int)EventID.Battle_ActorSelected, OnActorSelected);
        }

        protected override void UnRegisterListener()
        {
            base.UnRegisterListener();
            EventManager.instance.UnRegisterListener<int>((int)EventID.Battle_ActorSelected, OnActorSelected);
        }

        private void OnActorSelected(int actorId)
        {
            SetActor(actorId);
        }

        public void SetActor(int actorId)
        {
            CollectableActorStaticInfo staticInfo = StaticDataManager.instance.GetCollectableActor(actorId);
            m_iconImage.sprite = ResourceManager.instance.GetSprite(staticInfo.HeadIcon);
        }

        private enum EWidget
        {
            IconImage,
        }
    }
}
