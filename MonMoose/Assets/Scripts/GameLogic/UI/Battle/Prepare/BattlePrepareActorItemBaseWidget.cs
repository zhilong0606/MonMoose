using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonMoose.Logic.UI
{
    public class BattlePrepareActorItemBaseWidget : UIComponent
    {
        private Text m_text;

        protected int m_actorId;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            m_text = GetInventory().GetComponent<Text>((int)EWidget.Text);
        }

        public void SetActor(int actorId)
        {
            m_actorId = actorId;
            m_text.text = actorId.ToString();
        }

        private enum EWidget
        {
            Text,
        }
    }
}
