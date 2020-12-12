using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using MonMoose.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace MonMoose.GameLogic.UI
{
    public class BattlePrepareActorItemBaseWidget : UIComponent
    {
        private Image m_iconImage;
        private Text m_nameText;

        protected int m_actorId;

        public int actorId
        {
            get { return m_actorId; }
        }

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_nameText = GetInventory().GetComponent<Text>((int)EWidget.NameText);
            m_iconImage = GetInventory().GetComponent<Image>((int)EWidget.IconImage);
        }

        public void SetActor(int actorId)
        {
            m_actorId = actorId;
            CollectableActorStaticInfo staticInfo = StaticDataManager.instance.GetCollectableActor(actorId);
            m_nameText.text = staticInfo.Name;
            m_iconImage.sprite = ResourceManager.instance.GetSprite(staticInfo.HeadIcon);
        }

        private enum EWidget
        {
            NameText,
            IconImage,
        }
    }
}
