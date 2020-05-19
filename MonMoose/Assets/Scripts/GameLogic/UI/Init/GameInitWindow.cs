using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.UI;

public class GameInitWindow : UIWindow
{
    private Initializer m_initializer;
    private Image m_fillImage;
    private Text m_percentageText;

    protected override void OnInit(object param)
    {
        base.OnInit(param);
        m_initializer = param as Initializer;
        m_fillImage = GetInventory().GetComponent<Image>((int)EWidget.FillImage);
        m_percentageText = GetInventory().GetComponent<Text>((int)EWidget.PercentageText);
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
    }
}
