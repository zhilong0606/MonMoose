using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : LocalizationWidget
{
    private Text text;
    [SerializeField]
    protected bool autoNewLine = false;
    [SerializeField]
    protected EValueType curValueType = EValueType.Id;
    private object[] prms;
    private Dictionary<int, string> strMap = new Dictionary<int, string>(); 

    public string Text
    {
        get { return text.text; }
        set
        {
            curValueType = EValueType.Text;
            text.text = value; 
        }
    }
    public Color Color
    {
        get { return text.color; }
        set { text.color = value; }
    }

    protected override void OnInit(object param)
    {
        text = GetComponent<Text>();
        //m_window.AddLocalizationWidget(this);
    }

    public string LocalizationStr
    {
        get
        {
            string str;
            if (id == 0)
            {
                str = string.Empty;
            }
            else if (!strMap.TryGetValue(id, out str))
            {
                //str = StaticDataManager.instance.GetLocalizationText(id, (I18NType)language);
                strMap.Add(id, str);
            }
            return str; 
        }
    }

    public void Format(int textId, params object[] values)
    {
        id = textId;
        prms = values;
        curValueType = EValueType.Format;
        text.text = string.Format(LocalizationStr, values);
    }

    public void Format(string textStr, params object[] values)
    {
        Text = string.Format(textStr, values);
    }

    public void SetString(string str) {
        Text = str;
    }

    public override void OnIdUpdate()
    {
        curValueType = EValueType.Id;
        UpdateWidget();
    }

    public override void OnLanguageUpdate()
    {
        strMap.Clear();
        if (curValueType == EValueType.Id)
        {
            UpdateWidget();
        }
        else if (curValueType == EValueType.Format)
        {
            text.text = string.Format(LocalizationStr, prms);
        }
    }

    private void UpdateWidget()
    {
        string textStr = LocalizationStr;
        if (autoNewLine)
        {
            textStr = textStr.Replace("\\n", "\n");
        }
        if (text != null && text.text != null)
        {
            text.text = textStr;
        }        
    }

    public enum EValueType
    {
        Text,
        Format,
        Id,
    }
}
