using System;
using MonMoose.Core;
using UnityEngine.UI;

namespace MonMoose.Logic.UI
{
    public class GameInitWindow : UIWindow
    {
        private Image m_fillImage;
        private Text m_percentageText;
        private Text m_continueText;
        private Button m_continueBtn;

        private Initializer m_initializer;
        private Action m_actionOnEnd;

        protected override void OnInit(object param)
        {
            base.OnInit(param);
            GameInitParam initParam = param as GameInitParam;
            m_initializer = initParam.initializer;
            m_actionOnEnd = initParam.actionOnInitEnd;
            m_fillImage = GetInventory().GetComponent<Image>((int)EWidget.FillImage);
            m_percentageText = GetInventory().GetComponent<Text>((int)EWidget.PercentageText);
            m_continueText = GetInventory().GetComponent<Text>((int)EWidget.ContinueText);
            m_continueBtn = GetInventory().GetComponent<Button>((int)EWidget.ContinueBtn);
            m_continueBtn.interactable = false;

            m_continueBtn.onClick.AddListener(OnContinueBtnClicked);
            m_initializer.StartAsync(OnInitCompleted);
        }

        private void OnInitCompleted()
        {
            m_continueBtn.interactable = true;
            m_continueText.SetActiveSafely(true);
        }

        private void OnContinueBtnClicked()
        {
            if (m_actionOnEnd != null)
            {
                Action temp = m_actionOnEnd;
                m_actionOnEnd = null;
                temp();
            }
        }

        protected override void Update()
        {
            base.Update();
            float value = m_initializer.processRate;
            m_fillImage.fillAmount = value;
            m_percentageText.text = NumericStringManager.instance.GetNumber((int)(value * 100));
        }

        private enum EWidget
        {
            FillImage,
            PercentageText,
            ContinueBtn,
            ContinueText,
        }
    }
}
